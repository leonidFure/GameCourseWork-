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
            this.lHeroHeath = new System.Windows.Forms.Label();
            this.lHeroEnergy = new System.Windows.Forms.Label();
            this.bStep = new System.Windows.Forms.Button();
            this.bExit = new System.Windows.Forms.Button();
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
            this.YourPanel.Size = new System.Drawing.Size(886, 611);
            this.YourPanel.TabIndex = 0;
            this.YourPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.YourPanel_DragDrop);
            this.YourPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.YourPanel_DragEnter);
            // 
            // lOffCard2
            // 
            this.lOffCard2.AutoSize = true;
            this.lOffCard2.Font = new System.Drawing.Font("Arial Narrow", 24.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lOffCard2.Location = new System.Drawing.Point(902, 22);
            this.lOffCard2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lOffCard2.Name = "lOffCard2";
            this.lOffCard2.Size = new System.Drawing.Size(89, 40);
            this.lOffCard2.TabIndex = 2;
            this.lOffCard2.Text = "label1";
            // 
            // lOffCard1
            // 
            this.lOffCard1.AutoSize = true;
            this.lOffCard1.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lOffCard1.Location = new System.Drawing.Point(902, 374);
            this.lOffCard1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lOffCard1.Name = "lOffCard1";
            this.lOffCard1.Size = new System.Drawing.Size(132, 37);
            this.lOffCard1.TabIndex = 3;
            this.lOffCard1.Text = "Cards: 22";
            // 
            // lBCards2
            // 
            this.lBCards2.FormattingEnabled = true;
            this.lBCards2.ItemHeight = 15;
            this.lBCards2.Location = new System.Drawing.Point(901, 65);
            this.lBCards2.Name = "lBCards2";
            this.lBCards2.Size = new System.Drawing.Size(184, 259);
            this.lBCards2.TabIndex = 4;
            // 
            // lBCrads1
            // 
            this.lBCrads1.FormattingEnabled = true;
            this.lBCrads1.ItemHeight = 15;
            this.lBCrads1.Location = new System.Drawing.Point(901, 414);
            this.lBCrads1.Name = "lBCrads1";
            this.lBCrads1.Size = new System.Drawing.Size(184, 259);
            this.lBCrads1.TabIndex = 5;
            this.lBCrads1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lBCrads1_MouseDown);
            // 
            // lHeroHeath
            // 
            this.lHeroHeath.AutoSize = true;
            this.lHeroHeath.Font = new System.Drawing.Font("Arial", 40.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lHeroHeath.Location = new System.Drawing.Point(-1, 704);
            this.lHeroHeath.Name = "lHeroHeath";
            this.lHeroHeath.Size = new System.Drawing.Size(273, 61);
            this.lHeroHeath.TabIndex = 6;
            this.lHeroHeath.Text = "Health: 30";
            // 
            // lHeroEnergy
            // 
            this.lHeroEnergy.AutoSize = true;
            this.lHeroEnergy.Font = new System.Drawing.Font("Arial", 40.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lHeroEnergy.Location = new System.Drawing.Point(424, 704);
            this.lHeroEnergy.Name = "lHeroEnergy";
            this.lHeroEnergy.Size = new System.Drawing.Size(257, 61);
            this.lHeroEnergy.TabIndex = 7;
            this.lHeroEnergy.Text = "Energy: 1";
            // 
            // bStep
            // 
            this.bStep.Font = new System.Drawing.Font("Arial", 25.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bStep.Location = new System.Drawing.Point(901, 691);
            this.bStep.Name = "bStep";
            this.bStep.Size = new System.Drawing.Size(184, 79);
            this.bStep.TabIndex = 8;
            this.bStep.Text = "Your Step";
            this.bStep.UseVisualStyleBackColor = true;
            this.bStep.Click += new System.EventHandler(this.bStep_Click);
            // 
            // bExit
            // 
            this.bExit.Location = new System.Drawing.Point(13, 13);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(105, 46);
            this.bExit.TabIndex = 9;
            this.bExit.Text = "Exit";
            this.bExit.UseVisualStyleBackColor = true;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // Gameform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 774);
            this.Controls.Add(this.bExit);
            this.Controls.Add(this.bStep);
            this.Controls.Add(this.lHeroEnergy);
            this.Controls.Add(this.lHeroHeath);
            this.Controls.Add(this.lBCrads1);
            this.Controls.Add(this.lBCards2);
            this.Controls.Add(this.lOffCard1);
            this.Controls.Add(this.lOffCard2);
            this.Controls.Add(this.YourPanel);
            this.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Gameform";
            this.Text = "Form1";
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
        private System.Windows.Forms.Label lHeroHeath;
        private System.Windows.Forms.Label lHeroEnergy;
        private System.Windows.Forms.Button bStep;
        private System.Windows.Forms.Button bExit;
    }
}

