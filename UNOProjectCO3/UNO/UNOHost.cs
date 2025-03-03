using System;
using System.Collections.Generic;
using System.IO;
using UNOProjectCO3.Game_Connection_Algorithms;
using UNOProjectCO3.Games;
using UNOProjectCO3.UNO;

namespace UNOProjectCO3
{
    public class UNOHost : GameHost
    {
        public readonly CardDeck AvailableCards = new CardDeck();
        public long NextPlayerId;
        public long PreviousPlayerId;

        public UNOPlayer NextPlayer
        {
            get { return GetPlayer(NextPlayerId) as UNOPlayer; }
            set
            {
                PreviousPlayerId = NextPlayerId;
                NextPlayerId = value != null ? value.ID : 0;
            }
        }

        public readonly Stack<Card> CardStack = new Stack<Card>();
        public bool ClockwiseDirection;
        public Card.CardColor CurrentColor;
        bool PlayerDrewCard = false;

        #region LowLevel

        public override string GameTitle
        {
            get
            {
                return "UNO";
            }
        }

        public const byte MinUnoPlayers = 2;
        public const byte MaxUnoPlayers = 10;
        byte maxPlayers = MaxUnoPlayers;

        public override byte MaxPlayers
        {
            get { return maxPlayers; }
            set
            {
                if (State == GameState.Playing)
                    return;
                if (value < MinPlayers)
                    throw new InvalidOperationException("MayPlayer value cannot be smaller than MinPlayer count");
                maxPlayers = Math.Max(MaxUnoPlayers, value);
            }
        }

        byte minPlayers = MinUnoPlayers;

        public override byte MinPlayers
        {
            get
            {
                return minPlayers;
            }
            set
            {
                if (value > MaxPlayers)
                    throw new InvalidOperationException("MinPlayer value cannot be larger than MaxPlayer count");
                minPlayers = Math.Max(value, MinUnoPlayers);
            }
        }

        #endregion
        #region Card logic

        void SkipToNextPlayer()
        {
            PlayerDrewCard = false;
            var i = GetPlayerIndex(NextPlayer);
            if (ClockwiseDirection)
            {
                if (i == -1 || i + 1 >= PlayerCount)
                {
                    NextPlayer = GetPlayerByIndex(0) as UNOPlayer;
                    return;
                }

                NextPlayer = GetPlayerByIndex(i + 1) as UNOPlayer;

            }
            else
            {
                if (i == -1 || i - 1 < 0)
                {
                    NextPlayer = GetPlayerByIndex(PlayerCount - 1) as UNOPlayer;
                    return;
                }

                NextPlayer = GetPlayerByIndex(i - 1) as UNOPlayer;
            }
        }

        void InformNewPlayer()
        {
            SendGameDataToPlayer(NextPlayer, new[] { (byte)UNOMessage.YouAreNext });
        }

        public bool IsCardCompatibleToStack(Card c)
        {
            if (c.Color == Card.CardColor.Black || CardStack.Count == 0)
                return true;

            if (CurrentColor == c.Color || CardStack.Peek().Caption == c.Caption) // Gilt das letztere nur für Nummern oder auch für Event-Karten?
                return true;

            return false;
        }

        public bool IsHandCompatibleToStack(UNOPlayer p)
        {
            foreach (var card in p.Cards)
                if (IsCardCompatibleToStack(card))
                    return true;
            return false;
        }

        public bool TryPutOnStack(UNOPlayer p, Card c, Card.CardColor colorSelection, out string errorMsg)
        {
            errorMsg = null;

            if (p != NextPlayer)
            {
                errorMsg = "Wait for your round!";
                return false;
            }

            if (!IsCardCompatibleToStack(c) && (p.CardCount > 1 || c.Caption == Card.CardCaption.ChooseColour))
            {
                errorMsg = "Card not allowed to be put on the stack!";
                return false;
            }

            p.RemoveCard(c);
            CardStack.Push(c);

            DistributeSpecificPlayerUpdate(p);
            CurrentColor = colorSelection;

            if (p.CardCount == 0)
            {
                if (p.UNOButtonPressed)
                {
                    using (var ms = new MemoryStream())
                    using (var w = new BinaryWriter(ms))
                    {
                        w.Write((byte)MessagesforClient.GameFinished);
                        w.Write(p.Name);
                    }

                    NoticeGameFinished(false);
                    return true;
                }
                else
                {
                    DrawCard(p);
                    return true;
                }
            }

            switch (c.Caption)
            {
                case Card.CardCaption.Plus2:
                    SkipToNextPlayer();
                    var next = NextPlayer;
                    next.PlaceCard(AvailableCards.GiveCard());
                    next.PlaceCard (AvailableCards.GiveCard());
                    DistributeSpecificPlayerUpdate(next);
                    break;
                case Card.CardCaption.ChangeDirection:
                    ClockwiseDirection = !ClockwiseDirection;
                    break;
                case Card.CardCaption.SkipPlayer:
                    SkipToNextPlayer();
                    break;
                case Card.CardCaption.ChooseColour:
                    break;
                case Card.CardCaption.Pick4:
                    SkipToNextPlayer();
                    next = NextPlayer;
                    for (int i = 4; i > 0; i--)
                        next.PlaceCard(AvailableCards.GiveCard());
                    DistributeSpecificPlayerUpdate(next);
                    break;
                default:
                    break;
            }

            if (State != GameState.StartGame)
                SkipToNextPlayer();
            InformNewPlayer();
            DistributeGeneralPlayerUpdate();
            DistributeSpecificPlayerUpdate(NextPlayer);
            DistributeGameStates();
            return true;
        }

        public bool DrawCard(UNOPlayer p, bool sendupdate = true)
        {
            if (NextPlayer != p || PlayerDrewCard)
                return false;

            var c = AvailableCards.GiveCard();
            if (!p.PlaceCard(c))
            {
                AvailableCards.Place (c);
                return false;
            }

            PlayerDrewCard = true;

            if (sendupdate)
            {
                DistributeSpecificPlayerUpdate(p);
                DistributeGeneralPlayerUpdate();
                SkipToNextPlayer();
                InformNewPlayer();
            }

            return true;
        }

        public bool PressUnoButton(UNOPlayer p)
        {
            if (p.CardCount <= 1)
            {
                p.UNOButtonPressed = true;
                return true;
            }
            return false;
        }

        public bool SkipRound(UNOPlayer p)
        {
            if (NextPlayer != p || !PlayerDrewCard)
                return false;

            SkipToNextPlayer();
            return true;
        }

        #endregion

        protected override Player CreatePlayer(string nick)
        {
            return new UNOPlayer(this, nick);
        }

        protected override void OnPlayerDisconnected(Player p, MessagesforClient reason)
        {
            (p as UNOPlayer).ReleaseHand();

            if (NextPlayer == p)
            {
                SkipToNextPlayer();
                InformNewPlayer();
            }

            base.OnPlayerDisconnected(p, reason);
        }

        protected override bool StartGameInternal()
        {
            ClockwiseDirection = true;
            NextPlayerId = 0;

            AvailableCards.Reset();
            foreach (UNOPlayer p in Players)
            {
                p.ResetDeck();
            }
            CardStack.Clear();
            SkipToNextPlayer();
            var firstCard = AvailableCards.GiveCard();
            CurrentColor = firstCard.Color;
            if (CurrentColor == Card.CardColor.Black)
                CurrentColor = Card.CardColor.Yellow;
            NextPlayer.PlaceCard(firstCard);
            string errMsg;
            if (!TryPutOnStack(NextPlayer, firstCard, CurrentColor, out errMsg))
                return false;
            DistributeSpecificPlayerUpdate();
            DistributeGeneralPlayerUpdate();
            DistributeGameStates();
            return true;
        }

        protected override void OnComposePlayerInfo(Player p, BinaryWriter w)
        {
            var unop = p as UNOPlayer;

            w.Write((byte)unop.CardCount);
            var recommCards = new List<ushort>();

            foreach (var c in unop.Cards)
            {
                w.Write(c.ToHash());
                if (IsCardCompatibleToStack(c))
                    recommCards.Add(c.ToHash());
            }

            w.Write((byte)recommCards.Count);
            foreach (var hash in recommCards)
                w.Write(hash);

            base.OnComposePlayerInfo(p, w);
        }

        protected override void OnComposeGeneralPlayerInfo(Player p, BinaryWriter w)
        {
            base.OnComposeGeneralPlayerInfo(p, w);

            var unop = p as UNOPlayer;

            w.Write((byte)unop.CardCount);
        }

        protected override void OnGameDataReceived(Player playerOpt, BinaryReader r, BinaryWriter w)
        {
            string errorMsg = null;

            if (playerOpt == null)
                errorMsg = "Invalid player!";
            else
            {
                var message = (UNOMessage)r.ReadByte();

                switch (message)
                {
                    case UNOMessage.DrawCardFromStack:
                        if (!DrawCard(playerOpt as UNOPlayer))
                            errorMsg = "Can't draw card!";
                        break;
                    case UNOMessage.PressedUNO:
                        if (!PressUnoButton(playerOpt as UNOPlayer))
                            errorMsg = "Can't press UNO button";
                        break;
                    case UNOMessage.PlaceCard:

                        var c = Card.FromHash(r.ReadUInt16());
                        Card.CardColor col;

                        if (c.Color != Card.CardColor.Black)
                            col = c.Color;
                        else
                            col = (Card.CardColor)r.ReadByte();

                        TryPutOnStack(playerOpt as UNOPlayer, c, col, out errorMsg);
                        break;
                    case UNOMessage.SkipTurn:
                        if (!SkipRound(playerOpt as UNOPlayer))
                            errorMsg = "Can't skip turn!";
                        break;
                }
            }

            if (errorMsg != null)
            {
                w.Write((byte)MessagesforClient.GameData);
                w.Write((byte)UNOMessage.ActionNotAllowed);
                w.Write(errorMsg);
            }
        }

        public void DistributeGameStates()
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {

                w.Write((byte)UNOMessage.GameStates);
                w.Write((byte)CardStack.Count);
                w.Write(CardStack.Peek().ToHash());
                w.Write((byte)CurrentColor);
                w.Write(ClockwiseDirection);
                w.Write(NextPlayer.Name);

                SendGameDataToAllPlayers(ms.ToArray());
            }
        }

        protected override void NoticeGameFinished(bool aborted)
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {

                w.Write((byte)UNOMessage.GameFinished);
                w.Write(NextPlayer.Name);

                SendGameDataToAllPlayers(ms.ToArray());
            }
            base.NoticeGameFinished(aborted);
        }
    }
}