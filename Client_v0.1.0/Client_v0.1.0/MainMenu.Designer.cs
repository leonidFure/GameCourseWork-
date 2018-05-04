namespace Client_v0._1._0
{
    partial class MainMenu
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
            this.bPlay = new System.Windows.Forms.Button();
            this.bDeckSettings = new System.Windows.Forms.Button();
            this.bExit = new System.Windows.Forms.Button();
            this.lName = new System.Windows.Forms.Label();
            this.tName = new System.Windows.Forms.TextBox();
            this.lGreeting = new System.Windows.Forms.Label();
            this.cBSetdecks = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bPlay
            // 
            this.bPlay.Location = new System.Drawing.Point(225, 160);
            this.bPlay.Name = "bPlay";
            this.bPlay.Size = new System.Drawing.Size(250, 42);
            this.bPlay.TabIndex = 1;
            this.bPlay.Text = "Play";
            this.bPlay.UseVisualStyleBackColor = true;
            this.bPlay.Click += new System.EventHandler(this.bPlay_Click);
            // 
            // bDeckSettings
            // 
            this.bDeckSettings.Location = new System.Drawing.Point(225, 208);
            this.bDeckSettings.Name = "bDeckSettings";
            this.bDeckSettings.Size = new System.Drawing.Size(250, 42);
            this.bDeckSettings.TabIndex = 2;
            this.bDeckSettings.Text = "Deck settings";
            this.bDeckSettings.UseVisualStyleBackColor = true;
            this.bDeckSettings.Click += new System.EventHandler(this.bDeckSettings_Click);
            // 
            // bExit
            // 
            this.bExit.Location = new System.Drawing.Point(225, 256);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(250, 42);
            this.bExit.TabIndex = 3;
            this.bExit.Text = "Exit";
            this.bExit.UseVisualStyleBackColor = true;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lName.Location = new System.Drawing.Point(2, 406);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(155, 24);
            this.lName.TabIndex = 4;
            this.lName.Text = "Enter your name:";
            // 
            // tName
            // 
            this.tName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tName.Location = new System.Drawing.Point(163, 404);
            this.tName.Name = "tName";
            this.tName.Size = new System.Drawing.Size(147, 29);
            this.tName.TabIndex = 5;
            this.tName.Text = "User name";
            // 
            // lGreeting
            // 
            this.lGreeting.AutoSize = true;
            this.lGreeting.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lGreeting.Location = new System.Drawing.Point(209, 105);
            this.lGreeting.Name = "lGreeting";
            this.lGreeting.Size = new System.Drawing.Size(285, 39);
            this.lGreeting.TabIndex = 6;
            this.lGreeting.Text = "Hello, dear friend!";
            // 
            // cBSetdecks
            // 
            this.cBSetdecks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cBSetdecks.FormattingEnabled = true;
            this.cBSetdecks.Location = new System.Drawing.Point(532, 403);
            this.cBSetdecks.Name = "cBSetdecks";
            this.cBSetdecks.Size = new System.Drawing.Size(148, 32);
            this.cBSetdecks.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(468, 406);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "Deck:";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 439);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cBSetdecks);
            this.Controls.Add(this.lGreeting);
            this.Controls.Add(this.tName);
            this.Controls.Add(this.lName);
            this.Controls.Add(this.bExit);
            this.Controls.Add(this.bDeckSettings);
            this.Controls.Add(this.bPlay);
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bPlay;
        private System.Windows.Forms.Button bDeckSettings;
        private System.Windows.Forms.Button bExit;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.TextBox tName;
        private System.Windows.Forms.Label lGreeting;
        private System.Windows.Forms.ComboBox cBSetdecks;
        private System.Windows.Forms.Label label1;
    }
}