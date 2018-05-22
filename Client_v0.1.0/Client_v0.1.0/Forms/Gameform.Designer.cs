namespace Client_v0._1._0
{
    partial class Gameform
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.YourPanel = new System.Windows.Forms.Panel();
            this.lOffCard2 = new System.Windows.Forms.Label();
            this.lOffCard1 = new System.Windows.Forms.Label();
            this.lBCards2 = new System.Windows.Forms.ListBox();
            this.lBCrads1 = new System.Windows.Forms.ListBox();
            this.bStep = new System.Windows.Forms.Button();
            this.bExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // YourPanel
            // 
            this.YourPanel.AllowDrop = true;
            this.YourPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(187)))), ((int)(((byte)(206)))));
            this.YourPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.YourPanel.Location = new System.Drawing.Point(12, 65);
            this.YourPanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.YourPanel.Name = "YourPanel";
            this.YourPanel.Size = new System.Drawing.Size(886, 610);
            this.YourPanel.TabIndex = 0;
            this.YourPanel.Click += new System.EventHandler(this.YourPanel_Click);
            this.YourPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.YourPanel_DragDrop);
            this.YourPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.YourPanel_DragEnter);
            // 
            // lOffCard2
            // 
            this.lOffCard2.AutoSize = true;
            this.lOffCard2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lOffCard2.Location = new System.Drawing.Point(902, 22);
            this.lOffCard2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lOffCard2.Name = "lOffCard2";
            this.lOffCard2.Size = new System.Drawing.Size(0, 38);
            this.lOffCard2.TabIndex = 2;
            // 
            // lOffCard1
            // 
            this.lOffCard1.AutoSize = true;
            this.lOffCard1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lOffCard1.Location = new System.Drawing.Point(902, 374);
            this.lOffCard1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lOffCard1.Name = "lOffCard1";
            this.lOffCard1.Size = new System.Drawing.Size(0, 37);
            this.lOffCard1.TabIndex = 3;
            // 
            // lBCards2
            // 
            this.lBCards2.FormattingEnabled = true;
            this.lBCards2.Location = new System.Drawing.Point(901, 65);
            this.lBCards2.Name = "lBCards2";
            this.lBCards2.Size = new System.Drawing.Size(250, 251);
            this.lBCards2.TabIndex = 4;
            // 
            // lBCrads1
            // 
            this.lBCrads1.FormattingEnabled = true;
            this.lBCrads1.Location = new System.Drawing.Point(901, 414);
            this.lBCrads1.Name = "lBCrads1";
            this.lBCrads1.Size = new System.Drawing.Size(250, 251);
            this.lBCrads1.TabIndex = 5;
            this.lBCrads1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lBCrads1_MouseDown);
            // 
            // bStep
            // 
            this.bStep.Font = new System.Drawing.Font("Arial", 25.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bStep.Location = new System.Drawing.Point(901, 322);
            this.bStep.Name = "bStep";
            this.bStep.Size = new System.Drawing.Size(250, 49);
            this.bStep.TabIndex = 8;
            this.bStep.Text = "Your Turn";
            this.bStep.UseVisualStyleBackColor = true;
            this.bStep.Click += new System.EventHandler(this.bStep_Click);
            // 
            // bExit
            // 
            this.bExit.Enabled = false;
            this.bExit.Location = new System.Drawing.Point(13, 13);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(105, 46);
            this.bExit.TabIndex = 9;
            this.bExit.Text = "Exit";
            this.bExit.UseVisualStyleBackColor = true;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 40.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(300, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(596, 63);
            this.label1.TabIndex = 10;
            this.label1.Tag = "11";
            this.label1.Text = "Waiting second player.";
            // 
            // Gameform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 733);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bStep);
            this.Controls.Add(this.bExit);
            this.Controls.Add(this.lBCrads1);
            this.Controls.Add(this.lBCards2);
            this.Controls.Add(this.lOffCard1);
            this.Controls.Add(this.lOffCard2);
            this.Controls.Add(this.YourPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Gameform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game";
            this.Load += new System.EventHandler(this.Gameform_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel YourPanel;
        private System.Windows.Forms.Label lOffCard2;
        private System.Windows.Forms.Label lOffCard1;
        private System.Windows.Forms.ListBox lBCards2;
        private System.Windows.Forms.ListBox lBCrads1;
        private System.Windows.Forms.Button bStep;
        private System.Windows.Forms.Button bExit;
        private System.Windows.Forms.Label label1;
    }
}

