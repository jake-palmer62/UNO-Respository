using System;
using System.Drawing;
using System.Windows.Forms;

namespace UNOProjectCO3.UNO
{
    public partial class ColorChooser : Form
    {
        public Card.CardColor SelectedColor;
        public ColorChooser()
        {
            InitializeComponent();
        }

        private void ColorChooser_Load (object sender, EventArgs e)
        {
            Red_Button.BackColor = Color.Red;
            Yellow_Button.BackColor = Color.Yellow;
            Blue_Button.BackColor = Color.Blue;
            Green_Button.BackColor = Color.Green;
        }

        private void Green_Button_Click(object sender, EventArgs e)
        {
            SelectedColor = Card.CardColor.Green;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Blue_Button_Click(object sender, EventArgs e)
        {
            SelectedColor = Card.CardColor.Blue;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Yellow_Button_Click(object sender, EventArgs e)
        {
            SelectedColor = Card.CardColor.Yellow;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Red_Button_Click(object sender, EventArgs e)
        {
            SelectedColor = Card.CardColor.Red;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
