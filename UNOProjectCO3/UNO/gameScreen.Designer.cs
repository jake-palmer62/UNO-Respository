namespace UNOProjectCO3.Game_Connection_Algorithms
{
    partial class gameScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gameScreen));
            this.DrawCard_Button = new System.Windows.Forms.Button();
            this.DeclareUNO_Button = new System.Windows.Forms.Button();
            this.SkipTurn_Button = new System.Windows.Forms.Button();
            this.LeaveGame_Button = new System.Windows.Forms.Button();
            this.main_Panel = new System.Windows.Forms.Panel();
            this.hand_Panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // DrawCard_Button
            // 
            this.DrawCard_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawCard_Button.Location = new System.Drawing.Point(12, 415);
            this.DrawCard_Button.Name = "DrawCard_Button";
            this.DrawCard_Button.Size = new System.Drawing.Size(107, 30);
            this.DrawCard_Button.TabIndex = 0;
            this.DrawCard_Button.Text = "Draw Card!";
            this.DrawCard_Button.UseVisualStyleBackColor = true;
            this.DrawCard_Button.Click += new System.EventHandler(this.DrawCard_Button_Click);
            // 
            // DeclareUNO_Button
            // 
            this.DeclareUNO_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeclareUNO_Button.Location = new System.Drawing.Point(251, 415);
            this.DeclareUNO_Button.Name = "DeclareUNO_Button";
            this.DeclareUNO_Button.Size = new System.Drawing.Size(75, 30);
            this.DeclareUNO_Button.TabIndex = 1;
            this.DeclareUNO_Button.Text = "UNO!";
            this.DeclareUNO_Button.UseVisualStyleBackColor = true;
            this.DeclareUNO_Button.Click += new System.EventHandler(this.DeclareUNO_Button_Click);
            // 
            // SkipTurn_Button
            // 
            this.SkipTurn_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SkipTurn_Button.Location = new System.Drawing.Point(467, 415);
            this.SkipTurn_Button.Name = "SkipTurn_Button";
            this.SkipTurn_Button.Size = new System.Drawing.Size(109, 30);
            this.SkipTurn_Button.TabIndex = 2;
            this.SkipTurn_Button.Text = "Skip Turn";
            this.SkipTurn_Button.UseVisualStyleBackColor = true;
            this.SkipTurn_Button.Click += new System.EventHandler(this.SkipTurn_Button_Click);
            // 
            // LeaveGame_Button
            // 
            this.LeaveGame_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeaveGame_Button.Location = new System.Drawing.Point(670, 415);
            this.LeaveGame_Button.Name = "LeaveGame_Button";
            this.LeaveGame_Button.Size = new System.Drawing.Size(118, 30);
            this.LeaveGame_Button.TabIndex = 3;
            this.LeaveGame_Button.Text = "Leave Game";
            this.LeaveGame_Button.UseVisualStyleBackColor = true;
            this.LeaveGame_Button.Click += new System.EventHandler(this.LeaveGame_Button_Click);
            // 
            // main_Panel
            // 
            this.main_Panel.BackColor = System.Drawing.Color.White;
            this.main_Panel.Location = new System.Drawing.Point(12, 12);
            this.main_Panel.Name = "main_Panel";
            this.main_Panel.Size = new System.Drawing.Size(776, 232);
            this.main_Panel.TabIndex = 4;
            this.main_Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.main_Panel_Paint_1);
            // 
            // hand_Panel
            // 
            this.hand_Panel.BackColor = System.Drawing.Color.White;
            this.hand_Panel.Location = new System.Drawing.Point(12, 250);
            this.hand_Panel.Name = "hand_Panel";
            this.hand_Panel.Size = new System.Drawing.Size(776, 159);
            this.hand_Panel.TabIndex = 1;
            this.hand_Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.hand_Panel_Paint_1);
            this.hand_Panel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hand_Panel_MouseClick);
            // 
            // gameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.hand_Panel);
            this.Controls.Add(this.main_Panel);
            this.Controls.Add(this.LeaveGame_Button);
            this.Controls.Add(this.SkipTurn_Button);
            this.Controls.Add(this.DeclareUNO_Button);
            this.Controls.Add(this.DrawCard_Button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "gameScreen";
            this.Text = "UNO!";
            this.Load += new System.EventHandler(this.gameScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DrawCard_Button;
        private System.Windows.Forms.Button DeclareUNO_Button;
        private System.Windows.Forms.Button SkipTurn_Button;
        private System.Windows.Forms.Button LeaveGame_Button;
        private System.Windows.Forms.Panel main_Panel;
        private System.Windows.Forms.Panel hand_Panel;
    }
}