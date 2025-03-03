using System.Collections.Generic;

namespace UNOProjectCO3
{
    public class UNOPlayer : Player
    {
        public new UNOHost Host { get { return base.Host as UNOHost;  } }
        readonly List<Card> Hand = new List<Card>();
        public bool UNOButtonPressed;
        public int CardCount { get { return Hand.Count; } }
        public IEnumerable<Card> Cards
        { get { return Hand; } }

        public UNOPlayer (UNOHost Host, string PlayerName) : base(Host, PlayerName)
        {
        }
        
        public void ReleaseHand()
        {
            Host.AvailableCards.Place(Hand);
            Hand.Clear();
        }

        public void ResetDeck() // Resets the card deck.
        {
            Hand.Clear();
            this.Hand.AddRange(Host.AvailableCards.DealBegginingHand());
        }

        public bool PlaceCard(Card c) // Game rule for whether players can place cards or whether they must draw.
        {
            if (Hand.Contains(c))
            {
                return false;
            }
            else
            {
                Hand.Add(c);
                return true;                
            }            
        }

        public bool RemoveCard(Card c) // Removes the card after it has been played.
        {
            return Hand.Remove(c);
        }
        
    }
}
