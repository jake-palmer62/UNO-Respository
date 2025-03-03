using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UNOProjectCO3.Game_Connection_Algorithms;
using UNOProjectCO3.Games;


namespace UNOProjectCO3
{
    public class GameStateChangedArgs : EventArgs
    {
        #region Variables
        public readonly GameHost Host;
        public readonly GameState OldState;
        public readonly GameState NewState;
        #endregion

        public GameStateChangedArgs(GameHost H, GameState oldState, GameState newState) // Uses events to display current game states.
        {
            Host = H;
            OldState = oldState;
            NewState = newState;
        }
        
    }
    public abstract class GameHost : HostBackEnd
    {
        
        #region Game Properties
        public static GameHost theGameHostInstance { get; private set; }
        public static bool IsCurrentlyHosting { get { return theGameHostInstance != null; } }
        public abstract string GameTitle { get; }
        public readonly long ID = IDGenerator.GenerateID();
        public virtual byte MinPlayers { get { return 2; } set { } }
        public virtual byte MaxPlayers { get { return byte.MaxValue; } set { } }
        List<Player> players = new List<Player>();

        public int PlayerCount
        {
            get
            {
                return players.Count;
            }
        }

        public IEnumerable<Player> Players
        {
            get { return players; }
        }

        public Player GetPlayer(long id) // Assigns players with their own ID for validation.
        {
            lock (players)
                foreach (var p in players)
                    if (p.ID == id)
                        return p;
            return null;
        }

        public Player GetPlayer(string playerName)
        {
            lock (players)
                foreach (var p in players)
                    if (p.Name == playerName)
                        return p;
            return null;
        }

        public int GetPlayerIndex(Player p)
        {
            return players.IndexOf(p);
        }

        public Player GetPlayerByIndex(int i)
        {
            return players[i];
        }

        GameState state;
        public GameState State
        {
            get { return state; }
            private set
            {
                var args = new GameStateChangedArgs(this, state, value);
                state = value;

                if (GameStateChanged != null)
                    GameStateChanged(this, args);
                if (AnyGameStateChanged != null)
                    AnyGameStateChanged(this, args);
            }
        }
        #region Events
        public static EventHandler<GameStateChangedArgs> AnyGameStateChanged;
        public event EventHandler<GameStateChangedArgs> GameStateChanged;
        #endregion

        public virtual bool ReadyToPlay // Used in the ready button in the GameLobby to specify whether players are ready to play or not.
        {
            get
            {
                if (PlayerCount < MinPlayers || PlayerCount > MaxPlayers)
                    return false;
                lock (players)
                    foreach (var p in players)
                        if (!p.ReadyToPlay)
                            return false;
                return true;
            }
        }

        public event Action<bool> GameStartabilityChanged;
        #endregion

        public GameHost() : base(myPort)
        {
        }

        public static GameHost HostGame(IGameHostCreation Creator) // Creates the game host and ensures that only one instance of the game can be ran
        {
            if (IsCurrentlyHosting)
                throw new InvalidOperationException("Shut down other host first!");
            var host = Creator.Create();
            theGameHostInstance = host;
            host.State = GameState.Starting;
            host.State = GameState.WaitingForPlayers;
            return host;
        }


        #region Exitgame
        public static bool ShutDown()
        {
            if (theGameHostInstance == null)
                return false;
            theGameHostInstance.Shutdown();
            theGameHostInstance = null;
            return true;
        }

        public virtual void Shutdown()
        {
            State = GameState.ShuttingDown;
            SendToAllPlayers(new[] { (byte)MessagesforClient.ServerShutdown });
            Dispose();
        }
        #endregion

        #region Messaging between hosts
        protected override void DataRecieved(IPEndPoint ep, BinaryReader r) // Messages and game states for slaves to the game.
        {
            if (r.ReadInt64() != ID)
                return;
            var message = (HostMessage)r.ReadByte();
            var playerId = r.ReadInt64();
            MemoryStream answer;
            BinaryWriter w;
            Player player;
            answer = new MemoryStream();
            w = new BinaryWriter(answer);
            switch (message)
            {
                case HostMessage.Connect:
                    var requestedName = r.ReadString();
                    w.Write(playerId); 

                    if (State != Games.GameState.WaitingForPlayers)
                    {
                        w.Write((byte)MessagesforClient.JoinDenied);
                        w.Write("Server is not awaiting new players!");
                    }
                    else if (players.Count < MaxPlayers)
                    {
                        player = CreatePlayer(GetValidPlayerName(requestedName));

                        player.Address = ep;
                        w.Write((byte)MessagesforClient.JoinAllowed);
                        w.Write(player.ID);
                        w.Write(player.Name);

                        players.Add(player);
                        OnPlayerAdded(player);
                    }
                    else
                    {
                        w.Write((byte)MessagesforClient.JoinDenied);
                        w.Write("Player limit reached!");
                    }
                    break;

                case HostMessage.Disconnect:
                    player = GetPlayer(playerId);

                    if (player != null)
                        DisconnectPlayer(w, player, MessagesforClient.Disconnected, "Disconnected by user", ep);
                    break;

                case HostMessage.GetReadyState:
                    player = GetPlayer(playerId);

                    if (player != null)
                    {
                        w.Write(playerId);
                        w.Write((byte)MessagesforClient.IsReady);
                        w.Write(player.ReadyToPlay);
                    }
                    break;

                case HostMessage.SetReadyState:
                    if (State != Games.GameState.WaitingForPlayers)
                        break;

                    player = GetPlayer(playerId);

                    if (player != null)
                    {
                        player.ReadyToPlay = r.ReadBoolean();

                        // Send update to clients
                        DistributeGeneralPlayerUpdate(player);
                        CheckGameStartable();
                    }
                    break;

                case HostMessage.ChatMessage:
                    player = GetPlayer(playerId);

                    if (player == null)
                        break;

                    var chat = r.ReadString();
                    if (!string.IsNullOrEmpty(chat))
                    {
                        using (var ms2 = new MemoryStream())
                        using (var w2 = new BinaryWriter(ms2))
                        {
                            w2.Write((byte)MessagesforClient.ChatMessage);
                            w2.Write(player.Name);
                            w2.Write(chat);
                            SendToAllPlayers(ms2.ToArray());
                        }
                    }
                    break;

                case HostMessage.GameData:
                    w.Write(playerId);
                    var off = answer.Length;
                    OnGameDataReceived(GetPlayer(playerId), r, w);

                    if (answer.Length == off)
                    {
                        answer.SetLength(0);
                    }
                    break;


                case HostMessage.GetPlayerInfo:
                    player = GetPlayer(playerId);

                    if (player == null)
                        break;

                    w.Write(player.ID);
                    ComposeSpecificPlayerInfoBytes(player, w);
                    break;

                case HostMessage.GetPlayersInfo:
                    w.Write(playerId);
                    ComposeGeneralPlayerInfoBytes(w);
                    break;
            }

            if (answer.Length > 0)
                Send(answer, ep);

            w.Close();
            answer.Dispose();
        }
        #endregion

        #region Player
        protected abstract Player CreatePlayer(string playerName);

        protected virtual void OnPlayerAdded(Player player)
        {
            DistributeGeneralPlayerUpdate(player);
            CheckGameStartable();
        }
        protected virtual void OnPlayerDisconnecting(Player p, MessagesforClient reason) { }

        protected virtual void OnPlayerDisconnected(Player p, MessagesforClient reason)
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write((byte)MessagesforClient.OtherPlayerLeft);
                w.Write(p.Name);

                SendToAllPlayers(ms.ToArray());
                CheckGameStartable();
            }
        }
        protected virtual void OnComposePlayerInfo(Player p, BinaryWriter w) { }
        protected virtual void OnComposeGeneralPlayerInfo(Player p, BinaryWriter w) { }

        public void DisconnectPlayer(Player player, MessagesforClient reason = MessagesforClient.Disconnected, string message = null)
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                DisconnectPlayer(w, player, reason, message);
                Send(ms, player.Address);
            }
        }

        public void DisconnectPlayer(BinaryWriter w, Player player, MessagesforClient reason = MessagesforClient.Disconnected, string message = null, IPEndPoint ep = null)
        {
            OnPlayerDisconnecting(player, reason);
            lock (players)
                players.Remove(player);
            w.Write(player.ID);
            w.Write((byte)reason);
            if (message == null)
                w.Write((byte)0);
            else
                w.Write(message);
            if (ep != null)
                player.Address = ep;
            OnPlayerDisconnected(player, reason);
        }

        string GetValidPlayerName(string originalName)
        {
            lock (players)
            {
                bool rep;
                string currentName = originalName;
                int i = 2;
                do
                {
                    rep = false;
                    foreach (var p in players)
                        if (p.Name == currentName)
                        {
                            currentName = originalName + (i++).ToString();
                            rep = true;
                            break;
                        }
                } while (rep);

                return currentName;
            }
        }

        protected void DistributeGeneralPlayerUpdate(long id)
        {
            var p = GetPlayer(id);
            if (p == null)
                throw new InvalidDataException("id");

            DistributeGeneralPlayerUpdate(p);
        }

        protected void DistributeGeneralPlayerUpdate(Player p)
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write((byte)MessagesforClient.GeneralPlayersInfo);
                w.Write((byte)1);

                w.Write(p.Name);
                w.Write(p.ReadyToPlay);
                OnComposeGeneralPlayerInfo(p, w);
                SendToAllPlayers(ms.ToArray());
            }
        }

        protected void DistributeGeneralPlayerUpdate()
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                ComposeGeneralPlayerInfoBytes(w);
                SendToAllPlayers(ms.ToArray());
            }
        }

        protected void ComposeGeneralPlayerInfoBytes(BinaryWriter w)
        {
            w.Write((byte)MessagesforClient.GeneralPlayersInfo);

            lock (players)
            {
                w.Write((byte)PlayerCount);
                foreach (var p in players)
                {
                    w.Write(p.Name);
                    w.Write(p.ReadyToPlay);
                    OnComposeGeneralPlayerInfo(p, w);
                }
            }
        }

        void ComposeSpecificPlayerInfoBytes(Player p, BinaryWriter w)
        {
            w.Write((byte)MessagesforClient.PlayerInfo);
            OnComposePlayerInfo(p, w);
        }

        protected void DistributeSpecificPlayerUpdate(long id)
        {
            var p = GetPlayer(id);
            if (p == null)
                throw new InvalidDataException("id");

            DistributeSpecificPlayerUpdate(p);
        }

        protected void DistributeSpecificPlayerUpdate(Player p)
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                ComposeSpecificPlayerInfoBytes(p, w);
                SendToPlayer(p, ms.ToArray());
            }
        }

        protected void DistributeSpecificPlayerUpdate()
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                lock (players)
                    foreach (var p in players)
                    {
                        ComposeSpecificPlayerInfoBytes(p, w);
                        SendToPlayer(p, ms.ToArray());
                        ms.SetLength(0);
                    }
            }
        }
        #endregion

        #region Game
        protected virtual void OnGameDataReceived(Player playerOpt, BinaryReader input, BinaryWriter response) { }

        protected void SendGameDataToPlayer(Player p, byte[] data)
        {
            var actData = new byte[data.Length + sizeof(long) + 1];

            BitConverter.GetBytes(p.ID).CopyTo(actData, 0);
            actData[sizeof(long)] = (byte)MessagesforClient.GameData;
            data.CopyTo(actData, sizeof(long) + 1);

            Send(actData, p.Address);
        }

        protected void SendToPlayer(Player p, byte[] data)
        {
            var actData = new byte[data.Length + sizeof(long)];

            BitConverter.GetBytes(p.ID).CopyTo(actData, 0);
            data.CopyTo(actData, sizeof(long));

            Send(actData, p.Address);
        }

        protected void SendToAllPlayers(byte[] data)
        {
            var actData = new byte[data.Length + sizeof(long)];

            data.CopyTo(actData, sizeof(long));

            lock (players)
                foreach (var p in players)
                {
                    BitConverter.GetBytes(p.ID).CopyTo(actData, 0);
                    Send(actData, p.Address);
                }
        }

        protected void SendGameDataToAllPlayers(byte[] data)
        {
            var actData = new byte[data.Length + sizeof(long) + 1];

            actData[sizeof(long)] = (byte)MessagesforClient.GameData;
            data.CopyTo(actData, sizeof(long) + 1);

            lock (players)
                foreach (var p in players)
                {
                    BitConverter.GetBytes(p.ID).CopyTo(actData, 0);
                    Send(actData, p.Address);
                }
        }

        public void CheckGameStartable()
        {
            if (GameStartabilityChanged != null)
                GameStartabilityChanged(ReadyToPlay);
        }

        public bool StartGame()
        {
            if (!ReadyToPlay)
                return false;

            State = Games.GameState.StartGame;

            if (!StartGameInternal())
            {
                State = Games.GameState.WaitingForPlayers;
                return false;
            }

            State = Games.GameState.Playing;

            SendToAllPlayers(new[] { (byte)MessagesforClient.GameStarted });

            return true;
        }

        protected abstract bool StartGameInternal();

        protected virtual void NoticeGameFinished(bool aborted = false)
        {
            State = Games.GameState.GameFinished;
            SendToAllPlayers(new[] { (byte)MessagesforClient.GameFinished, (byte)(aborted ? 1 : 0) });
            State = Games.GameState.WaitingForPlayers;
        }
        #endregion
    }
}