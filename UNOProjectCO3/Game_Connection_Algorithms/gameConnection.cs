using System;
using System.IO;
using System.Net;
using UNOProjectCO3.Game_Connection_Algorithms;
using UNOProjectCO3.Games;
using System.Net.Sockets;
using UNOProjectCO3.UNO;

namespace UNOProjectCO3
{
    public abstract class gameConnection : HostBackEnd
    {
        public IPEndPoint HostIPEndPointAddress { get; private set; }
        public long HostId { get; private set; }
        readonly long ConnectionId = IDGenerator.GenerateID();
        public long PlayerId { get; private set; }
        public string PlayerName;
        bool connected;

        public bool IsConnected
        {
            get { return connected; }
        }

        bool isPlayerReady;

        public bool IsPlayerReady
        {
            get
            {
                return isPlayerReady;
            }
            set
            {
                SetPlayerReadyState(value);
            }
        }
        public bool IsGameStartable { get; private set; }

        #region Game hosting events
        public delegate void NoArgDelegate();
        public event NoArgDelegate Connected;
        public delegate void DisconnectedHandler(MessagesforClient msg, string reason);
        public event DisconnectedHandler Disconnected;
        public delegate void ChatHandler(string Name, string message);
        public event ChatHandler ChatArrived;
        public event NoArgDelegate GameStarted;
        public event Action<bool> GameFinished;
        public event Action<bool> ReadyStateChanged;
        public event Action<string> OtherPlayerLeft;
        public delegate void GeneralPlayerInfoHandler(string nick, bool isReady, object furtherInfo);
        public event GeneralPlayerInfoHandler GeneralPlayerInfoReceived;
        #endregion

        public static gameConnection Create(IPAddress ip, long hostId, IGameHostCreation theGameHost)
        {
            var Connection = theGameHost.CreateConnection();
            Connection.HostId = hostId;
            Connection.HostIPEndPointAddress = new IPEndPoint(ip, myPort);
            return Connection;
        }

        public virtual void Initialize(string Name)
        {
            SendConnectionRequest(Name);
        }

        void SendConnectionRequest(string Name)
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(HostId);
                w.Write((byte)HostMessage.Connect);
                w.Write(ConnectionId);
                w.Write(Name);
                Send(ms, HostIPEndPointAddress);
            }
        }

        protected virtual void GameConnected()
        {
            connected = true;
            if (Connected != null)
                Connected();
        }

        protected virtual void GameDisconnected(MessagesforClient msg, string reason)
        {
            connected = false;
            if (Disconnected != null)
                Disconnected(msg, reason);
        }

        public void DisconnectGame()
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(HostId);
                w.Write((byte)HostMessage.Disconnect);
                w.Write(PlayerId);
                Send(ms, HostIPEndPointAddress);
            }
        }

        protected override void DataRecieved(IPEndPoint ep, BinaryReader r)
        {
            var id = r.ReadInt64();
            if (PlayerId != 0)
            {
                if (id != PlayerId)
                    return;
            }
            else if (ConnectionId != id)
                return;
            var msg = (MessagesforClient)r.ReadByte();
            string Name;

            switch (msg)
            {
                case MessagesforClient.JoinAllowed:

                    PlayerId = r.ReadInt64();
                    PlayerName = r.ReadString();

                    GameConnected();
                    break;
                case MessagesforClient.Kicked:
                case MessagesforClient.Disconnected:
                case MessagesforClient.Timeout:
                case MessagesforClient.JoinDenied:
                    GameDisconnected(msg, r.ReadString());
                    break;
                case MessagesforClient.ServerShutdown:
                    GameDisconnected(msg, string.Empty);
                    break;
                case MessagesforClient.OtherPlayerLeft:
                    PlayerName = r.ReadString();
                    OnOtherPlayerLeft(PlayerName);
                    if (OtherPlayerLeft != null)
                        OtherPlayerLeft(PlayerName);
                    break;
                case MessagesforClient.IsReady:
                    isPlayerReady = r.ReadBoolean();
                    if (ReadyStateChanged != null)
                        ReadyStateChanged(isPlayerReady);
                    break;
                case MessagesforClient.PlayerInfo:
                    OnGamePlayerInfoReceived(r);
                    break;
                case MessagesforClient.ChatMessage:
                    Name = r.ReadString();
                    var chat = r.ReadString();
                    if (ChatArrived != null)
                        ChatArrived(Name, chat);
                    break;
                case MessagesforClient.GameStarted:
                    OnGameStarted();
                    if (GameStarted != null)
                        GameStarted();
                    break;
                case MessagesforClient.GameFinished:
                    var aborted = r.ReadBoolean();
                    OnGameFinished(aborted);
                    if (GameFinished != null)
                        GameFinished(aborted);
                    break;
                case MessagesforClient.GameData:
                    OnGameDataReceived(r);
                    break;
                case MessagesforClient.GeneralPlayersInfo:
                    var count = r.ReadByte();
                    while (count-- > 0)
                    {
                        Name = r.ReadString();
                        var isReady = r.ReadBoolean();
                        OnGeneralPlayerInfoReceived(Name, isReady, r);
                    }
                    break;
            }
        }
        protected virtual void OnGamePlayerInfoReceived(BinaryReader r) { }
        protected virtual void OnOtherPlayerLeft(string nick) { }

        public void AcquirePlayerInfo()
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(HostId);
                w.Write((byte)HostMessage.GetPlayerInfo);
                w.Write(PlayerId);
                Send(ms, HostIPEndPointAddress);
            }
        }

        protected void SendGeneralPlayerInfoReceivedEvent(string Name, bool isReady, object furtherInfo = null)
        {
            if (GeneralPlayerInfoReceived != null)
                GeneralPlayerInfoReceived(Name, isReady, furtherInfo);
        }

        protected virtual void OnGeneralPlayerInfoReceived(string Name, bool isReady, BinaryReader r)
        {
            SendGeneralPlayerInfoReceivedEvent(Name, isReady, null);
        }

        public void GetGeneralPlayerInfo()
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(HostId);
                w.Write((byte)HostMessage.GetPlayersInfo);
                w.Write(PlayerId);
                Send(ms, HostIPEndPointAddress);
            }
        }

        void SetPlayerReadyState(bool ready)
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(PlayerId);
                w.Write((byte)HostMessage.SetReadyState);
                w.Write(PlayerId);
                w.Write(ready);
                Send(ms, HostIPEndPointAddress);
            }
        }

        public void SendChatMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
                using (var ms = new MemoryStream())
                using (var w = new BinaryWriter(ms))
                {
                    w.Write(HostId);
                    w.Write((byte)HostMessage.ChatMessage);
                    w.Write(PlayerId);
                    w.Write(message);
                    Send(ms, HostIPEndPointAddress);
                }
        }

        protected virtual void OnGameDataReceived(BinaryReader r) { }
        protected virtual void OnGameStarted() { }
        protected virtual void OnGameFinished(bool aborted) { }

        public void SendGameData(MemoryStream ms)
        {
            SendGameData(ms.ToArray());
        }

        public void SendGameData(byte[] data)
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(HostId);
                w.Write((byte)HostMessage.GameData);
                w.Write(PlayerId);
                w.Write(data);

                Send(ms, HostIPEndPointAddress);
            }
        }
    }
}
