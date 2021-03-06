﻿namespace Client_v0._1._0
{
    partial class DecSettings
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
            this.lBYourDeck = new System.Windows.Forms.ListBox();
            this.lBAllCard = new System.Windows.Forms.ListBox();
            this.lAllDeck = new System.Windows.Forms.Label();
            this.bBack = new System.Windows.Forms.Button();
            this.bSaveDeck = new System.Windows.Forms.Button();
            this.cBDecks = new System.Windows.Forms.ComboBox();
            this.lMyDeck = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lBYourDeck
            // 
            this.lBYourDeck.AllowDrop = true;
            this.lBYourDeck.FormattingEnabled = true;
            this.lBYourDeck.Location = new System.Drawing.Point(278, 50);
            this.lBYourDeck.Name = "lBYourDeck";
            this.lBYourDeck.Size = new System.Drawing.Size(231, 342);
            this.lBYourDeck.TabIndex = 0;
            this.lBYourDeck.DragDrop += new System.Windows.Forms.DragEventHandler(this.lBYourDeck_DragDrop);
            this.lBYourDeck.DragEnter += new System.Windows.Forms.DragEventHandler(this.lBYourDeck_DragEnter);
            this.lBYourDeck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lBYourDeck_MouseDown);
            // 
            // lBAllCard
            // 
            this.lBAllCard.AllowDrop = true;
            this.lBAllCard.FormattingEnabled = true;
            this.lBAllCard.Location = new System.Drawing.Point(12, 50);
            this.lBAllCard.Name = "lBAllCard";
            this.lBAllCard.Size = new System.Drawing.Size(239, 342);
            this.lBAllCard.TabIndex = 2;
            this.lBAllCard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lBAllCard_MouseDown);
            // 
            // lAllDeck
            // 
            this.lAllDeck.AutoSize = true;
            this.lAllDeck.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lAllDeck.Location = new System.Drawing.Point(5, 8);
            this.lAllDeck.Name = "lAllDeck";
            this.lAllDeck.Size = new System.Drawing.Size(151, 39);
            this.lAllDeck.TabIndex = 3;
            this.lAllDeck.Text = "All crads";
            // 
            // bBack
            // 
            this.bBack.Location = new System.Drawing.Point(12, 398);
            this.bBack.Name = "bBack";
            this.bBack.Size = new System.Drawing.Size(239, 40);
            this.bBack.TabIndex = 4;
            this.bBack.Text = "Back to main menu";
            this.bBack.UseVisualStyleBackColor = true;
            this.bBack.Click += new System.EventHandler(this.bBack_Click);
            // 
            // bSaveDeck
            // 
            this.bSaveDeck.Location = new System.Drawing.Point(278, 398);
            this.bSaveDeck.Name = "bSaveDeck";
            this.bSaveDeck.Size = new System.Drawing.Size(231, 40);
            this.bSaveDeck.TabIndex = 6;
            this.bSaveDeck.Text = "Save deck";
            this.bSaveDeck.UseVisualStyleBackColor = true;
            this.bSaveDeck.Click += new System.EventHandler(this.bSaveDeck_Click);
            // 
            // cBDecks
            // 
            this.cBDecks.FormattingEnabled = true;
            this.cBDecks.Location = new System.Drawing.Point(515, 50);
            this.cBDecks.Name = "cBDecks";
            this.cBDecks.Size = new System.Drawing.Size(121, 21);
            this.cBDecks.TabIndex = 7;
            this.cBDecks.SelectedIndexChanged += new System.EventHandler(this.cBDecks_SelectedIndexChanged);
            // 
            // lMyDeck
            // 
            this.lMyDeck.AutoSize = true;
            this.lMyDeck.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lMyDeck.Location = new System.Drawing.Point(271, 8);
            this.lMyDeck.Name = "lMyDeck";
            this.lMyDeck.Size = new System.Drawing.Size(148, 39);
            this.lMyDeck.TabIndex = 8;
            this.lMyDeck.Text = "Cards: 0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(508, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 39);
            this.label1.TabIndex = 9;
            this.label1.Text = "Decks";
            // 
            // DecSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 444);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lMyDeck);
            this.Controls.Add(this.cBDecks);
            this.Controls.Add(this.bSaveDeck);
            this.Controls.Add(this.bBack);
            this.Controls.Add(this.lAllDeck);
            this.Controls.Add(this.lBAllCard);
            this.Controls.Add(this.lBYourDeck);
            this.Name = "DecSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DecSettings";
            this.Load += new System.EventHandler(this.DecSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lBYourDeck;
        private System.Windows.Forms.ListBox lBAllCard;
        private System.Windows.Forms.Label lAllDeck;
        private System.Windows.Forms.Button bBack;
        private System.Windows.Forms.Button bSaveDeck;
        private System.Windows.Forms.ComboBox cBDecks;
        private System.Windows.Forms.Label lMyDeck;
        private System.Windows.Forms.Label label1;
    }
}