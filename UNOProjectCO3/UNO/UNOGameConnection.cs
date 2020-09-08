using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UNOProjectCO3.Game_Connection_Algorithms;
using System.IO;

namespace UNOProjectCO3.UNO
{
    public enum UNOProperty
    {
        OwnHand, OtherPlayerHand, OtherGameStates, NextPlayer,
    }

    public class UNOGameConnection : gameConnection
    {
        public readonly Dictionary<string, int> OtherPlayerHand = new Dictionary<string, int>(); // Uses a known list for the other players' hand
        public readonly List<Card> OwnHand = new List<Card>();
        public readonly List<Card> ReccomendedCards = new List<Card>();
        public Card TopCard;
        public Card.CardColor ColourSelection;
        public int CardsOnStack;
        public bool ClockwiseGameDirection; // Specifies that the current game direction is clockwise
        public string CurrentPlayer;

        public bool IsAtGameTurn // Returns the player name so that the user knows who's turn it is
        {
            get { return CurrentPlayer == PlayerName; }
        }

        public gameScreen theGameScreen; // Creates the game screen

        public event Action<UNOProperty> PropertyChanged;

        void NotifyPropChanged(UNOProperty prop) // Specifies what property has eben changed (e.g Card being played etc).
        {
            if (PropertyChanged != null)
            {
                Program.MainForm.BeginInvoke(PropertyChanged, prop);
            }
        }

        void CloseGUI() // Closes the game screen
        {
            if (theGameScreen == null)
                return;

            theGameScreen.Close();
            theGameScreen.Dispose();
            theGameScreen = null;
        }

        public override void Initialize(string name)
        {
            base.Initialize(name);
        }

        protected override void OnGameFinished(bool aborted) // Closes the game when a user has shutdown the client.
        {
            theGameScreen.BeginInvoke(new MethodInvoker(CloseGUI));
            base.OnGameFinished(aborted);
        }

        protected override void OnGameStarted() // Opens the game screen to begin the game
        {
            Program.MainForm.BeginInvoke(new MethodInvoker(() => {
                theGameScreen = new gameScreen(this);
                theGameScreen.Show();
            }));
        }

        protected override void OnOtherPlayerLeft(string name) // Removes other players when they have left the game
        {
            OtherPlayerHand.Remove(name);
            if (PropertyChanged != null)
                PropertyChanged(UNOProperty.OtherPlayerHand);

            base.OnOtherPlayerLeft(name);
        }

        protected override void gameDisconnected(ClientMessages msg, string reason) // Displayes a reason why the game was disconected and closes the solution
        {
            Program.MainForm.BeginInvoke(new MethodInvoker(CloseGUI));
            base.gameDisconnected(msg, reason);
        }

        protected override void OnGeneralPlayerInfoReceived(string Name, bool isReady, BinaryReader r)
        {
            base.OnGeneralPlayerInfoReceived(Name, isReady, r);
            OtherPlayerHand[Name] = (int)r.ReadByte();
            NotifyPropChanged(UNOProperty.OtherPlayerHand);
        }

        protected override void OnPlayerInfoReceived(BinaryReader r) // Clears and reshuffles card lsits, notifys that cards are being dealt.
        {
            OwnHand.Clear();
            ReccomendedCards.Clear();
            var num = (int)r.ReadByte();
            while (num-- != 0)
            {
                OwnHand.Add(Card.FromHash(r.ReadUInt16()));
                ReccomendedCards.Add(Card.FromHash(r.ReadUInt16()));
            }
            NotifyPropChanged(UNOProperty.OwnHand);
            base.OnPlayerInfoReceived(r);
        }

        protected override void OnGameDataReceived(BinaryReader r) // Specifies what message is to be sent to the player, based on game rules.
        {
            var message = (UNOMessage)r.ReadByte();

            switch (message)
            {
                case UNOMessage.ActionNotAllowed:
                    var a = r.ReadString();
                    theGameScreen.BeginInvoke(new MethodInvoker(() =>
                    MessageBox.Show(a, "Action not Allowed!")));
                    break;
                case UNOMessage.GameStates:
                    CardsOnStack = (int)r.ReadByte();
                    TopCard = Card.FromHash(r.ReadUInt16());
                    ColourSelection = (Card.CardColor)r.ReadByte();
                    ClockwiseGameDirection = r.ReadBoolean();
                    CurrentPlayer = r.ReadString();
                    NotifyPropChanged(UNOProperty.OtherGameStates);
                    break;
                case UNOMessage.YouAreNext:
                    NotifyPropChanged(UNOProperty.NextPlayer);
                    break;
                case UNOMessage.GameFinished:
                    a = r.ReadString();
                    theGameScreen.BeginInvoke(new MethodInvoker(()=>
                    MessageBox.Show(a + " has won the game!", "Game Over")));
                    break;
            }
        }

        public void PressUNOButton()
        {
            SendGameData(new[] { (byte)UNOMessage.PressedUNO });
        }

        public void PutCardOnStack(Card c, Card.CardColor color) // Places the cards on the game stack.
        {
            var a = new byte[1 + 2 + (c.Color == Card.CardColor.Black ? 1 : 0)];
            a[0] = (byte)UNOMessage.PlaceCard;
            BitConverter.GetBytes(c.ToHash()).CopyTo(a, 1);

            if (c.Color == Card.CardColor.Black)
            {
                a[3] = (byte)color;
            }
            SendGameData(a);
        }

        public void DrawCard()
        {
            SendGameData(new[] { (byte)UNOMessage.DrawCardFromStack });
        }

        public void SkipTurn()
        {
            SendGameData(new[] { (byte)UNOMessage.SkipTurn });
        }
    }
}
