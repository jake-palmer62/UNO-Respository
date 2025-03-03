using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using UNOProjectCO3.UNO;

namespace UNOProjectCO3.Game_Connection_Algorithms
{
    public partial class gameScreen : Form
    {
        bool loaded;
        public readonly UNOGameConnection Connection;
        public float[,] ImageCoordinates;

        public gameScreen (UNOGameConnection connection)
        {
            this.Connection = connection;
            InitializeComponent();
            connection.PropertyChanged += thePropertyChanged;
        }

        ~gameScreen()
        {
            Connection.PropertyChanged -= thePropertyChanged;
        }

        private void gameScreen_Load (object sender, EventArgs e)
        {
            loaded = true;
        }

        void thePropertyChanged(UNOProperty property)
        {
            if (!loaded)
                return;
            BeginInvoke(new MethodInvoker(() => {
                switch (property)
                {
                    case UNOProperty.OwnHand:
                        hand_Panel.Invalidate();
                        break;
                    case UNOProperty.OtherGameStates:
                    case UNOProperty.OtherPlayerHand:
                        main_Panel.Invalidate();
                        break;
                    case UNOProperty.NextPlayer:
                        break;
                }
            }));
        }

        public const double dbl = 2 * Math.PI;
        Font fontforplayer = SystemFonts.CaptionFont;
        Bitmap blankCardImage = Card.GetImage(Card.CardCaption.None, Card.CardColor.Black);
        Font currentplayerfont;

        private void main_Panel_Paint (object sender, PaintEventArgs e)
        {
            if (currentplayerfont == null)
                currentplayerfont = new Font(fontforplayer, FontStyle.Bold);
            var graphics = e.Graphics;
            var width = (float)main_Panel.Width;
            var height = (float)main_Panel.Height;
            var middlepointX = width / 2f;
            var middlepointY = Height / 2f;
            var image = Connection.TopCard.GetImage();
            var doublewidth = 60f;
            var doublef = doublewidth / (float)image.Width;
            var doubleheight = doublef * image.Height; //  this arranges the players around a circle within the centre of the screen
            graphics.DrawImage(image, new RectangleF(middlepointX - doublewidth / 2f, (middlepointY + doubleheight) < height ? middlepointY : (height - doubleheight), doublewidth, doubleheight));
            var gamma = Math.Acos(1.0 - (Math.Pow(width, 2) / (2 * Math.Pow(middlepointY, 2) + 0.5 * Math.Pow(width, 2))));
            var numberofplayers = Connection.OtherPlayerHand.Count;
            var angleDistance = (dbl - gamma) / (double)numberofplayers;
            var radius = Math.Min(middlepointX, middlepointY);
            var phase = -Math.PI / 2 + gamma / 2;
            doublewidth = 60f;
            doublef = doublewidth / (float)image.Width;
            doubleheight = doublef * image.Height;

            var playerenum = Connection.OtherPlayerHand.GetEnumerator();
            for (int i = 1; i < numberofplayers; i++)
            {
                playerenum.MoveNext();
                if (playerenum.Current.Key == Connection.PlayerName)
                    playerenum.MoveNext();
                var current = playerenum.Current;
                var pointX = middlepointX + radius * (float)Math.Cos(angleDistance * i + phase);
                var pointY = middlepointY + radius * (float)Math.Sin(angleDistance * i + phase);
                var f = current.Key == Connection.CurrentPlayer ? currentplayerfont : fontforplayer;
                var size = graphics.MeasureString(current.Key, f);
                graphics.DrawString(current.Key, f, Brushes.Black, pointX - size.Width / 2, pointY);
                graphics.DrawImage(blankCardImage, pointX - doublewidth / 2, pointY += size.Height, doublewidth, doubleheight);
                var numString = current.Value.ToString();
                size = graphics.MeasureString(numString, f);
                graphics.DrawString(numString, f, Brushes.Black, pointX - size.Width / 2, pointY + doubleheight / 2 - size.Height / 2);
            }
        }

        public static Bitmap ResizeImage (Image theImage, double dblwidth)
        {
            double doubleF = dblwidth / theImage.Width;
            double doubleH = doubleF * theImage.Height;
            Bitmap resizedImage = new Bitmap((int)dblwidth, (int)doubleH);
            using (Graphics gNew = Graphics.FromImage(resizedImage))
            {
                gNew.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gNew.DrawImage(theImage, new Rectangle(0, 0, (int)dblwidth, (int)doubleH));
            }
            return resizedImage;
        }

        #region Hand Panel
        const float HandCardWidth = 80f;

        private void hand_Panel_Paint (object sender, PaintEventArgs e)
        {
            var WidthPerCard = HandCardWidth / 2;
            var num = Connection.OwnHand.Count;
            var g = e.Graphics;
            var movePosition = 0f;
            ImageCoordinates = new float[num, 2];
            var width = (float)hand_Panel.Width;
            var height = (float)hand_Panel.Height;
            var middlepointX = width / 4f;
            if (num * WidthPerCard > (width - middlepointX))
            {
                middlepointX = width - (num * WidthPerCard);
                if (middlepointX < 0)
                {
                    middlepointX = 0;
                    WidthPerCard = width / num;
                }
            }

            for (int i = 0; i < num; i++)
            {
                var c = Connection.OwnHand[i];
                var Image = c.GetImage();
                ImageCoordinates[i, 0] = middlepointX + movePosition;
                ImageCoordinates[i, 1] = (Connection.ReccomendedCards.Contains(c) ? 0f : 15f);
                movePosition += WidthPerCard;
                var dblF = HandCardWidth / (float)Image.Width;
                var dblHeight = dblF * Image.Height;
                g.DrawImage(Image, new RectangleF(ImageCoordinates[i, 0], ImageCoordinates[i, 1], HandCardWidth, dblHeight));
            }
        }

        private void hand_Panel_MouseClick (object sender, MouseEventArgs e)
        {
            int image = HitTestCard(e.Location);
            if (image != -1)
            {
                var c = Connection.OwnHand[image];
                var col = c.Color;

                if (c.Color == Card.CardColor.Black)
                {
                    var chooser = new ColorChooser();
                    chooser.ShowDialog(this);
                    col = chooser.SelectedColor;
                }
                Connection.PutCardOnStack(c, col);
            }
        }

        int HitTestCard(Point loc)
        {
            var x = loc.X;
            var y = loc.Y;

            int image = -1;
            for (int i = 0; i < Connection.OwnHand.Count; i++)
            {
                var img = Connection.OwnHand[i].GetImage();
                var dblFac = HandCardWidth / (float)img.Width;
                var dblHeight = dblFac * img.Height;
                float test2 = ImageCoordinates[i, 0] + HandCardWidth;     
                if (x >= ImageCoordinates[i, 0] && x <= ImageCoordinates[i, 0] + HandCardWidth && y >= ImageCoordinates[i, 1] && y <= ImageCoordinates[i, 1] + dblHeight)
                {
                    image = i;                    
                }
            }
            return image;
        }

        #endregion


        private void SkipTurn_Button_Click(object sender, EventArgs e)
        {
            Connection.SkipTurn();
        }

        private void DrawCard_Button_Click(object sender, EventArgs e)
        {
            Connection.DrawCard();
        }

        private void LeaveGame_Button_Click(object sender, EventArgs e)
        {
            Connection.DisconnectGame();
        }

        private void DeclareUNO_Button_Click(object sender, EventArgs e)
        {
            Connection.PressUNOButton();
        }

        private void main_Panel_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void hand_Panel_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
