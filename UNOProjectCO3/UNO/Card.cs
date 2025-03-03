using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using UNOProjectCO3.Properties;

namespace UNOProjectCO3
{
    public struct Card
    {
        public readonly CardColor Color;
        public readonly CardCaption Caption;

        public Card(CardColor Color, CardCaption Caption)
        {
            this.Color = Color;
            this.Caption = Caption;
        }

        public Bitmap GetImage()
        {
            return GetImage(Caption, Color);
        }

        public static Bitmap GetImage(CardCaption caption, CardColor color)
        {
            var StringBuilder1 = new StringBuilder();

            switch (caption)
            {
                case CardCaption.Zero:
                    StringBuilder1.Append("ZERO");
                    break;
                case CardCaption.One:
                    StringBuilder1.Append("ONE");
                    break;
                case CardCaption.Two:
                    StringBuilder1.Append("TWO");
                    break;
                case CardCaption.Three:
                    StringBuilder1.Append("THREE");
                    break;
                case CardCaption.Four:
                    StringBuilder1.Append("FOUR");
                    break;
                case CardCaption.Five:
                    StringBuilder1.Append("FIVE");
                    break;
                case CardCaption.Six:
                    StringBuilder1.Append("SIX");
                    break;
                case CardCaption.Seven:
                    StringBuilder1.Append("SEVEN");
                    break;
                case CardCaption.Eight:
                    StringBuilder1.Append("EIGHT");
                    break;
                case CardCaption.Nine:
                    StringBuilder1.Append("NINE");
                    break;

                case CardCaption.ChooseColour:
                    StringBuilder1.Append("WILD");
                    break;

                case CardCaption.Pick4:
                    StringBuilder1.Append("FOUR_WILD");
                    break;

                case CardCaption.Plus2:
                    StringBuilder1.Append("DRAW_TWO");
                    break;

                case CardCaption.ChangeDirection:
                    StringBuilder1.Append("REVERSE");
                    break;

                case CardCaption.SkipPlayer:
                    StringBuilder1.Append("SKIP");
                    break;

                case CardCaption.None:
                    StringBuilder1.Append("BACKCARD");
                    break;
            }

            switch (color)
            {
                case CardColor.Red:
                    StringBuilder1.Append("_Red");
                    break;
                case CardColor.Green:
                    StringBuilder1.Append("_Green");
                    break;
                case CardColor.Yellow:
                    StringBuilder1.Append("_Yellow");
                    break;
                case CardColor.Blue:
                    StringBuilder1.Append("_Blue");
                    break;
            }
            return Resources.ResourceManager.GetObject(StringBuilder1.ToString()) as Bitmap;
        }

        public enum CardColor : byte
        {
            Red, Green, Blue, Yellow, Black, None
        }

        public enum CardCaption : byte
        {
            None, Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Plus2, ChangeDirection, SkipPlayer, Pick4, ChooseColour,
        }

        public ushort ToHash()
        {
            return (ushort)(((byte)Color << 8) + (byte)Caption);
        }

        public static Card FromHash(ushort hash)
        {
            return new Card((CardColor)(hash >> 8), (CardCaption)hash);
        }

        public override int GetHashCode()
        {
            return (int)ToHash();
        }

        public override bool Equals(object obj)
        {
            return obj is Card && ((Card)obj).ToHash() == ToHash();
        }
    }

}

