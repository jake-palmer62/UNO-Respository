namespace UNOProjectCO3.Games
{
    partial class Servers
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Refresh_Button = new System.Windows.Forms.Button();
            this.text_PlayerName = new System.Windows.Forms.TextBox();
            this.list_Servers = new System.Windows.Forms.ListBox();
            this.Create_Button = new System.Windows.Forms.Button();
            this.Join_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(483, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select Games";
            // 
            // Refresh_Button
            // 
            this.Refresh_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Refresh_Button.Location = new System.Drawing.Point(197, 459);
            this.Refresh_Button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Refresh_Button.Name = "Refresh_Button";
            this.Refresh_Button.Size = new System.Drawing.Size(133, 46);
            this.Refresh_Button.TabIndex = 2;
            this.Refresh_Button.Text = "Refresh";
            this.Refresh_Button.UseVisualStyleBackColor = true;
            this.Refresh_Button.Click += new System.EventHandler(this.Refresh_Button_Click);
            // 
            // text_PlayerName
            // 
            this.text_PlayerName.Location = new System.Drawing.Point(23, 64);
            this.text_PlayerName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.text_PlayerName.Name = "text_PlayerName";
            this.text_PlayerName.Size = new System.Drawing.Size(420, 22);
            this.text_PlayerName.TabIndex = 3;
            this.text_PlayerName.TextChanged += new System.EventHandler(this.text_PlayerName_TextChanged);
            // 
            // list_Servers
            // 
            this.list_Servers.FormattingEnabled = true;
            this.list_Servers.ItemHeight = 16;
            this.list_Servers.Location = new System.Drawing.Point(489, 64);
            this.list_Servers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.list_Servers.Name = "list_Servers";
            this.list_Servers.Size = new System.Drawing.Size(505, 356);
            this.list_Servers.TabIndex = 4;
            // 
            // Create_Button
            // 
            this.Create_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Create_Button.Location = new System.Drawing.Point(655, 459);
            this.Create_Button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Create_Button.Name = "Create_Button";
            this.Create_Button.Size = new System.Drawing.Size(157, 46);
            this.Create_Button.TabIndex = 5;
            this.Create_Button.Text = "Create Game";
            this.Create_Button.UseVisualStyleBackColor = true;
            this.Create_Button.Click += new System.EventHandler(this.Create_Button_Click);
            // 
            // Join_Button
            // 
            this.Join_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Join_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Join_Button.Location = new System.Drawing.Point(397, 459);
            this.Join_Button.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Join_Button.Name = "Join_Button";
            this.Join_Button.Size = new System.Drawing.Size(201, 46);
            this.Join_Button.TabIndex = 7;
            this.Join_Button.Text = "Join Game";
            this.Join_Button.UseVisualStyleBackColor = true;
            this.Join_Button.Click += new System.EventHandler(this.Join_Button_Click);
            // 
            // Servers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.Join_Button);
            this.Controls.Add(this.Create_Button);
            this.Controls.Add(this.list_Servers);
            this.Controls.Add(this.text_PlayerName);
            this.Controls.Add(this.Refresh_Button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Servers";
            this.Text = "Servers";
            this.Load += new System.EventHandler(this.Servers_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Refresh_Button;
        private System.Windows.Forms.TextBox text_PlayerName;
        private System.Windows.Forms.ListBox list_Servers;
        private System.Windows.Forms.Button Create_Button;
        private System.Windows.Forms.Button Join_Button;
    }
}