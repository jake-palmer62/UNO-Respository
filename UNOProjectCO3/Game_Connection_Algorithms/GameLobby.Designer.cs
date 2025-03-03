namespace UNOProjectCO3.Game_Connection_Algorithms
{
    partial class GameLobby
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameLobby));
            this.label1 = new System.Windows.Forms.Label();
            this.chat_TextBox = new System.Windows.Forms.TextBox();
            this.text_ChatMessage = new System.Windows.Forms.TextBox();
            this.Say_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.playerList = new System.Windows.Forms.ListBox();
            this.button_Ready = new System.Windows.Forms.CheckBox();
            this.Return_to_Lobby = new System.Windows.Forms.Button();
            this.panel_Admin = new System.Windows.Forms.Panel();
            this.Admin_Label = new System.Windows.Forms.Label();
            this.button_KickPlayers = new System.Windows.Forms.Button();
            this.button_StartGame = new System.Windows.Forms.Button();
            this.panel_Admin.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Waiting for Players:";
            // 
            // chat_TextBox
            // 
            this.chat_TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chat_TextBox.Location = new System.Drawing.Point(420, 38);
            this.chat_TextBox.Multiline = true;
            this.chat_TextBox.Name = "chat_TextBox";
            this.chat_TextBox.Size = new System.Drawing.Size(429, 562);
            this.chat_TextBox.TabIndex = 3;
            this.chat_TextBox.TextChanged += new System.EventHandler(this.chat_TextBox_TextChanged);
            // 
            // text_ChatMessage
            // 
            this.text_ChatMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_ChatMessage.Location = new System.Drawing.Point(421, 606);
            this.text_ChatMessage.Name = "text_ChatMessage";
            this.text_ChatMessage.Size = new System.Drawing.Size(310, 29);
            this.text_ChatMessage.TabIndex = 4;
            this.text_ChatMessage.TextChanged += new System.EventHandler(this.text_ChatMessage_TextChanged);
            // 
            // Say_Button
            // 
            this.Say_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Say_Button.Location = new System.Drawing.Point(737, 606);
            this.Say_Button.Name = "Say_Button";
            this.Say_Button.Size = new System.Drawing.Size(112, 45);
            this.Say_Button.TabIndex = 5;
            this.Say_Button.Text = "Say";
            this.Say_Button.UseVisualStyleBackColor = true;
            this.Say_Button.Click += new System.EventHandler(this.Say_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(422, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Game Chat:";
            // 
            // playerList
            // 
            this.playerList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerList.FormattingEnabled = true;
            this.playerList.ItemHeight = 18;
            this.playerList.Location = new System.Drawing.Point(14, 38);
            this.playerList.Name = "playerList";
            this.playerList.Size = new System.Drawing.Size(386, 562);
            this.playerList.TabIndex = 8;
            this.playerList.SelectedIndexChanged += new System.EventHandler(this.playerList_SelectedIndexChanged);
            // 
            // button_Ready
            // 
            this.button_Ready.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button_Ready.Appearance = System.Windows.Forms.Appearance.Button;
            this.button_Ready.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Ready.Location = new System.Drawing.Point(862, 599);
            this.button_Ready.Name = "button_Ready";
            this.button_Ready.Size = new System.Drawing.Size(340, 52);
            this.button_Ready.TabIndex = 9;
            this.button_Ready.Text = "Ready";
            this.button_Ready.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button_Ready.CheckedChanged += new System.EventHandler(this.button_Ready_CheckedChanged);
            this.button_Ready.Click += new System.EventHandler(this.ready_Button_Click);
            // 
            // Return_to_Lobby
            // 
            this.Return_to_Lobby.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Return_to_Lobby.AutoSize = true;
            this.Return_to_Lobby.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Return_to_Lobby.Location = new System.Drawing.Point(862, 527);
            this.Return_to_Lobby.Name = "Return_to_Lobby";
            this.Return_to_Lobby.Size = new System.Drawing.Size(340, 65);
            this.Return_to_Lobby.TabIndex = 10;
            this.Return_to_Lobby.Text = "Return To Lobby";
            this.Return_to_Lobby.UseVisualStyleBackColor = true;
            this.Return_to_Lobby.Click += new System.EventHandler(this.Return_to_Lobby_Click);
            // 
            // panel_Admin
            // 
            this.panel_Admin.Controls.Add(this.Admin_Label);
            this.panel_Admin.Controls.Add(this.button_KickPlayers);
            this.panel_Admin.Controls.Add(this.button_StartGame);
            this.panel_Admin.Location = new System.Drawing.Point(992, 11);
            this.panel_Admin.Name = "panel_Admin";
            this.panel_Admin.Size = new System.Drawing.Size(191, 116);
            this.panel_Admin.TabIndex = 11;
            this.panel_Admin.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Admin_Paint);
            // 
            // Admin_Label
            // 
            this.Admin_Label.AutoSize = true;
            this.Admin_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Admin_Label.Location = new System.Drawing.Point(46, 3);
            this.Admin_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Admin_Label.Name = "Admin_Label";
            this.Admin_Label.Size = new System.Drawing.Size(109, 20);
            this.Admin_Label.TabIndex = 9;
            this.Admin_Label.Text = "Admin Panel";
            // 
            // button_KickPlayers
            // 
            this.button_KickPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_KickPlayers.Location = new System.Drawing.Point(71, 61);
            this.button_KickPlayers.Name = "button_KickPlayers";
            this.button_KickPlayers.Size = new System.Drawing.Size(120, 23);
            this.button_KickPlayers.TabIndex = 8;
            this.button_KickPlayers.Text = "Kick Player";
            this.button_KickPlayers.Click += new System.EventHandler(this.button_KickPlayers_Click);
            // 
            // button_StartGame
            // 
            this.button_StartGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_StartGame.Enabled = false;
            this.button_StartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_StartGame.Location = new System.Drawing.Point(74, 90);
            this.button_StartGame.Name = "button_StartGame";
            this.button_StartGame.Size = new System.Drawing.Size(117, 23);
            this.button_StartGame.TabIndex = 1;
            this.button_StartGame.Text = "Start Game";
            this.button_StartGame.Click += new System.EventHandler(this.Start_button_Click);
            // 
            // GameLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 663);
            this.Controls.Add(this.panel_Admin);
            this.Controls.Add(this.Return_to_Lobby);
            this.Controls.Add(this.button_Ready);
            this.Controls.Add(this.playerList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Say_Button);
            this.Controls.Add(this.text_ChatMessage);
            this.Controls.Add(this.chat_TextBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GameLobby";
            this.Text = "GameLobby";
            this.Load += new System.EventHandler(this.GameLobby_Load);
            this.panel_Admin.ResumeLayout(false);
            this.panel_Admin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox chat_TextBox;
        private System.Windows.Forms.TextBox text_ChatMessage;
        private System.Windows.Forms.Button Say_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox playerList;
        private System.Windows.Forms.CheckBox button_Ready;
        private System.Windows.Forms.Button Return_to_Lobby;
        private System.Windows.Forms.Panel panel_Admin;
        private System.Windows.Forms.Button button_KickPlayers;
        private System.Windows.Forms.Button button_StartGame;
        private System.Windows.Forms.Label Admin_Label;
    }
}