using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Threading;


namespace UNOProjectCO3.Game_Connection_Algorithms
{
    public partial class GameLobby : Form
    {
        public GameLobby()
        {
            InitializeComponent();
        }

        ~GameLobby()
        {
            GameHost.ShutDown();
        }

        gameConnection Connection;
        public readonly ManualResetEvent ConnectedEvent = new ManualResetEvent(false);
        static bool IsAdmin { get { return GameHost.IsHosting; } }
        bool GameClosed;

        public static GameLobby CreateGame(string thePlayerName, IGameHostCreation theGameHost)
        {
            if (GameHost.IsHosting)
            {
                throw new InvalidOperationException("Cannot create another game while already hosting one!");
            }
            var host = GameHost.HostGame(theGameHost);
            var lobby = TryToJoin(IPAddress.None, host.ID, thePlayerName, theGameHost);

            if (!GameHost.IsHosting)
            {
                return null;
            }

            if (lobby == null)
            {
                GameHost.ShutDown();
            }
            return lobby;                        
        }

        public static GameLobby TryToJoin(IPAddress IP, long hostID, string playerName, IGameHostCreation theGameHost)
        {
            var lobby = new GameLobby();
            var Connection = gameConnection.Create(IP, hostID, theGameHost);
            lobby.Connection = Connection;
            lobby.Init();
            Connection.Initialize(playerName);
            return lobby;
        }

        void OtherPlayerLeft(string Name)
        {
            Program.MainForm.BeginInvoke(new MethodInvoker(() => playerList.Items.Remove(new PlayerInfo { name = Name })));
        }

        void ChatMessageReceived(string name, string message)
        {
            chat_TextBox.BeginInvoke(new MethodInvoker(() => chat_TextBox.AppendText (name + ": " + message + Environment.NewLine)));
        }

        void Init()
        {
            Connection.Connected += Connected;
            Connection.Disconnected += Disconnected;
            Connection.GeneralPlayerInfoReceived += PlayerInfoReceived;
            Connection.OtherPlayerLeft += OtherPlayerLeft;
            Connection.ChatArrived += ChatMessageReceived;
            Connection.ReadyStateChanged += ReadyStateChanged;
            Connection.GameStarted += GameStarted;
            Connection.GameFinished += GameFinished;
        }

        void GameStarted()
        {
        }

        void GameFinished(bool obj)
        {
            Show();
        }

        void ReadyStateChanged(bool ready)
        {
            BeginInvoke(new MethodInvoker(() => button_Ready.Enabled = ready));
        }

        void Connected()
        {
            ConnectedEvent.Set();
            Program.MainForm.BeginInvoke(new MethodInvoker(Show));
            Connection.AcquireGeneralPlayerInfo();
        }

        void Disconnected(ClientMessages reason, string message)
        {
            ConnectedEvent.Reset();
            Connection.Dispose();
            Connection = null;

            if (!GameClosed)
                Invoke(new MethodInvoker(Close));

            string title;
            switch (reason)
            {
                default:
                    title = "Disconnected";
                    break;
                case ClientMessages.JoinDenied:
                    title = "Join denied";
                    break;
                case ClientMessages.Kicked:
                    title = "Kicked";
                    break;
                case ClientMessages.Timeout:
                    title = "Timeout";
                    break;
                case ClientMessages.ServerShutdown:
                    title = "Shutdown";
                    message = "Server was shut down";
                    break;
            }
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        Font boldFont;

        private void GameLobby_Load(object sender, EventArgs e)
        {
            if (!IsAdmin)
            {
                panel_Admin.Visible = false;
            }
            else
            {
                GameHost.theInstance.GameStartabilityChanged += ReadyStateChanged;
            }
        }
        void DrawPlayerItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index < 0)
                return;
            var pi = playerList.Items[e.Index] as PlayerInfo;
            var font = e.Font;
            if (pi.isReady)
            {
                if (boldFont == null)
                    boldFont = new Font(e.Font, FontStyle.Bold);

                font = boldFont;
            }
            e.Graphics.DrawString(pi.name, font, e.State == DrawItemState.Selected ? Brushes.White : Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
        }

        class PlayerInfo
        {
            public string name;
            public bool isReady;

            public override bool Equals(object obj)
            {
                return obj is PlayerInfo && (obj as PlayerInfo).name == name;
            }

            public override int GetHashCode()
            {
                return name.GetHashCode();
            }
        }

        void PlayerInfoReceived(string name, bool isReady, object furtherInfo)
        {
            Program.MainForm.BeginInvoke(new MethodInvoker(() =>
            {
                var pi = new PlayerInfo { name = name, isReady = isReady };
                var i = playerList.Items.IndexOf(name);

                if (i < 0)
                    playerList.Items.Add(name);
                else
                {
                    playerList.Items.RemoveAt(i);
                    playerList.Items.Insert(i, pi);
                }
            }));
        }

        private void playerWait_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Say_Button_Click(object sender, EventArgs e)
        {
            var t = text_ChatMessage.Text;
            if (string.IsNullOrEmpty(t))
                return;
            Connection.SendChat(t);
            text_ChatMessage.Clear();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            GameHost.ShutDown();
        }

        private void ready_Button_Click(object sender, EventArgs e)
        {
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            GameHost.theInstance.StartGame();
        }

        void text_ChatMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                Say_Button_Click(sender, e);
        }

        private void button_Ready_CheckedChanged(object sender, EventArgs e)
        {
            Connection.IsPlayerReady = button_Ready.Checked;
        }

        private void Return_to_Lobby_Click(object sender, EventArgs e)
        {
            Connection.Disconnect();
            GameClosed = true;
            Close();
        }

        private void chat_TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_StartGame_Click(object sender, EventArgs e)
        {
            GameHost.theInstance.StartGame();
        }

        private void button_KickPlayers_Click(object sender, EventArgs e)
        {
            foreach (PlayerInfo pi in playerList.SelectedItems)
            {
                var p = GameHost.theInstance.GetPlayer(pi.name);
                if (p == null)
                {
                    continue;
                }
                GameHost.theInstance.DisconnectPlayer(p, ClientMessages.Kicked);
            }
        }

        private void panel_Admin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void playerList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void text_ChatMessage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
