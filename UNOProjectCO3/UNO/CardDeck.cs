using System;
using System.Collections.Generic;

namespace UNOProjectCO3.UNO
{
    public class CardDeck
    {
        public const int BegginingCards = 7;
        public readonly List<Card> theCards = new List<Card>();


        public CardDeck()
        {
            Reset();
        }

        public void Reset()
        {
            theCards.Clear();

            theCards.Add(new Card(Card.CardColor.Red, Card.CardCaption.Zero));
            theCards.Add(new Card(Card.CardColor.Blue, Card.CardCaption.Zero));
            theCards.Add(new Card(Card.CardColor.Green, Card.CardCaption.Zero));
            theCards.Add(new Card(Card.CardColor.Yellow, Card.CardCaption.Zero));

            for (int i = 0; i < 4; i++)
            {
                theCards.Add(new Card(Card.CardColor.Black, Card.CardCaption.Pick4));
                theCards.Add(new Card(Card.CardColor.Black, Card.CardCaption.ChooseColour));
            }

            for (int i = 0; i < 2; i++)
            {
                theCards.Add(new Card(Card.CardColor.Red, Card.CardCaption.One)); //One
                theCards.Add(new Card(Card.CardColor.Blue, Card.CardCaption.One));
                theCards.Add(new Card(Card.CardColor.Green, Card.CardCaption.One));
                theCards.Add(new Card(Card.CardColor.Yellow, Card.CardCaption.One));
                theCards.Add(new Card(Card.CardColor.Red, Card.CardCaption.Two)); //Two
                theCards.Add(new Card(Card.CardColor.Blue, Card.CardCaption.Two));
                theCards.Add(new Card(Card.CardColor.Green, Card.CardCaption.Two));
                theCards.Add(new Card(Card.CardColor.Yellow, Card.CardCaption.Two));
                theCards.Add(new Card(Card.CardColor.Red, Card.CardCaption.Three)); //Three
                theCards.Add(new Card(Card.CardColor.Blue, Card.CardCaption.Three));
                theCards.Add(new Card(Card.CardColor.Green, Card.CardCaption.Three));
                theCards.Add(new Card(Card.CardColor.Yellow, Card.CardCaption.Three));
                theCards.Add(new Card(Card.CardColor.Red, Card.CardCaption.Four)); //Four
                theCards.Add(new Card(Card.CardColor.Blue, Card.CardCaption.Four));
                theCards.Add(new Card(Card.CardColor.Green, Card.CardCaption.Four));
                theCards.Add(new Card(Card.CardColor.Yellow, Card.CardCaption.Four));
                theCards.Add(new Card(Card.CardColor.Red, Card.CardCaption.Five)); //Five
                theCards.Add(new Card(Card.CardColor.Blue, Card.CardCaption.Five));
                theCards.Add(new Card(Card.CardColor.Green, Card.CardCaption.Five));
                theCards.Add(new Card(Card.CardColor.Yellow, Card.CardCaption.Five));
                theCards.Add(new Card(Card.CardColor.Red, Card.CardCaption.Six)); //Six
                theCards.Add(new Card(Card.CardColor.Blue, Card.CardCaption.Six));
                theCards.Add(new Card(Card.CardColor.Green, Card.CardCaption.Six));
                theCards.Add(new Card(Card.CardColor.Yellow, Card.CardCaption.Six));
                theCards.Add(new Card(Card.CardColor.Red, Card.CardCaption.Seven)); //Seven
                theCards.Add(new Card(Card.CardColor.Blue, Card.CardCaption.Seven));
                theCards.Add(new Card(Card.CardColor.Green, Card.CardCaption.Seven));
                theCards.Add(new Card(Card.CardColor.Yellow, Card.CardCaption.Seven));
                theCards.Add(new Card(Card.CardColor.Red, Card.CardCaption.Eight)); //Eight
                theCards.Add(new Card(Card.CardColor.Blue, Card.CardCaption.Eight));
                theCards.Add(new Card(Card.CardColor.Green, Card.CardCaption.Eight));
                theCards.Add(new Card(Card.CardColor.Yellow, Card.CardCaption.Eight));
                theCards.Add(new Card(Card.CardColor.Red, Card.CardCaption.Nine)); //Nine
                theCards.Add(new Card(Card.CardColor.Blue, Card.CardCaption.Nine));
                theCards.Add(new Card(Card.CardColor.Green, Card.CardCaption.Nine));
                theCards.Add(new Card(Card.CardColor.Yellow, Card.CardCaption.Nine));
                theCards.Add(new Card(Card.CardColor.Red, Card.CardCaption.Plus2)); //+2
                theCards.Add(new Card(Card.CardColor.Blue, Card.CardCaption.Plus2));
                theCards.Add(new Card(Card.CardColor.Green, Card.CardCaption.Plus2));
                theCards.Add(new Card(Card.CardColor.Yellow, Card.CardCaption.Plus2));
                theCards.Add(new Card(Card.CardColor.Red, Card.CardCaption.ChangeDirection)); //Reverse
                theCards.Add(new Card(Card.CardColor.Blue, Card.CardCaption.ChangeDirection));
                theCards.Add(new Card(Card.CardColor.Green, Card.CardCaption.ChangeDirection));
                theCards.Add(new Card(Card.CardColor.Yellow, Card.CardCaption.ChangeDirection));
                theCards.Add(new Card(Card.CardColor.Red, Card.CardCaption.SkipPlayer)); //Skip Next Player
                theCards.Add(new Card(Card.CardColor.Blue, Card.CardCaption.SkipPlayer));
                theCards.Add(new Card(Card.CardColor.Green, Card.CardCaption.SkipPlayer));
                theCards.Add(new Card(Card.CardColor.Yellow, Card.CardCaption.SkipPlayer));
            }
        }

        public bool Place(Card card)
        {
            if (theCards.Contains(card)) 
            return false;

            theCards.Add(card);
            return true;
        }

        public void Place(IEnumerable<Card> subDeck)
        {
            foreach (var cards in subDeck)
                Place(theCards);
        }

        public static void ShuffleCards<Card>(IList<Card> theCards) //IList for indexed list.
        {
            Random Rand = new Random();
            int c = theCards.Count;
            while (c > 1)
            {
                c--;
                int j = Rand.Next(c + 1);
                Card value = theCards[c];
                theCards[c] = theCards[j];
                theCards[j] = value;
            }
        }

        public List<Card> DealBegginingHand()
        {
            var PlayerHand = new List<Card>();
            var Rand = new Random();
            for (int i = 0; i < BegginingCards; i++)
            {
                int j = Rand.Next(theCards.Count);
                PlayerHand.Add(theCards[j]);
                theCards.RemoveAt(j);
            }
            return PlayerHand;
        }

        public Card GiveCard()
        {
            int n = (theCards.Count) - 1;
            Card card = theCards[n];
            theCards.RemoveAt(n);
            return card;
        }
    }
}  

