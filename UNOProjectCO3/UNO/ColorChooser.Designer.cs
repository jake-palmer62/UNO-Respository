namespace UNOProjectCO3.UNO
{
    partial class ColorChooser
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
            this.Blue_Button = new System.Windows.Forms.Button();
            this.Green_Button = new System.Windows.Forms.Button();
            this.Red_Button = new System.Windows.Forms.Button();
            this.Yellow_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Blue_Button
            // 
            this.Blue_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Blue_Button.Location = new System.Drawing.Point(221, 61);
            this.Blue_Button.Name = "Blue_Button";
            this.Blue_Button.Size = new System.Drawing.Size(103, 47);
            this.Blue_Button.TabIndex = 0;
            this.Blue_Button.Text = "Blue";
            this.Blue_Button.UseVisualStyleBackColor = true;
            this.Blue_Button.Click += new System.EventHandler(this.Blue_Button_Click);
            // 
            // Green_Button
            // 
            this.Green_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Green_Button.Location = new System.Drawing.Point(64, 61);
            this.Green_Button.Name = "Green_Button";
            this.Green_Button.Size = new System.Drawing.Size(103, 47);
            this.Green_Button.TabIndex = 2;
            this.Green_Button.Text = "Green";
            this.Green_Button.UseVisualStyleBackColor = true;
            this.Green_Button.Click += new System.EventHandler(this.Green_Button_Click);
            // 
            // Red_Button
            // 
            this.Red_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Red_Button.Location = new System.Drawing.Point(64, 174);
            this.Red_Button.Name = "Red_Button";
            this.Red_Button.Size = new System.Drawing.Size(103, 47);
            this.Red_Button.TabIndex = 3;
            this.Red_Button.Text = "Red";
            this.Red_Button.UseVisualStyleBackColor = true;
            this.Red_Button.Click += new System.EventHandler(this.Red_Button_Click);
            // 
            // Yellow_Button
            // 
            this.Yellow_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Yellow_Button.Location = new System.Drawing.Point(221, 174);
            this.Yellow_Button.Name = "Yellow_Button";
            this.Yellow_Button.Size = new System.Drawing.Size(103, 47);
            this.Yellow_Button.TabIndex = 4;
            this.Yellow_Button.Text = "Yellow";
            this.Yellow_Button.UseVisualStyleBackColor = true;
            this.Yellow_Button.Click += new System.EventHandler(this.Yellow_Button_Click);
            // 
            // ColorChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 288);
            this.Controls.Add(this.Yellow_Button);
            this.Controls.Add(this.Red_Button);
            this.Controls.Add(this.Green_Button);
            this.Controls.Add(this.Blue_Button);
            this.Name = "ColorChooser";
            this.Text = "ColorChooser";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Blue_Button;
        private System.Windows.Forms.Button Green_Button;
        private System.Windows.Forms.Button Red_Button;
        private System.Windows.Forms.Button Yellow_Button;
    }
}