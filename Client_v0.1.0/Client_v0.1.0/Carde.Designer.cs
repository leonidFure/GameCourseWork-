namespace Client_v0._1._0
{
    partial class Carde
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Carde));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lHealth = new System.Windows.Forms.Label();
            this.lDamage = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(107, 103);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lHealth
            // 
            this.lHealth.AutoSize = true;
            this.lHealth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lHealth.ForeColor = System.Drawing.Color.White;
            this.lHealth.Location = new System.Drawing.Point(64, 139);
            this.lHealth.Name = "lHealth";
            this.lHealth.Size = new System.Drawing.Size(46, 31);
            this.lHealth.TabIndex = 1;
            this.lHealth.Text = "11";
            // 
            // lDamage
            // 
            this.lDamage.AutoSize = true;
            this.lDamage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lDamage.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lDamage.ForeColor = System.Drawing.Color.White;
            this.lDamage.Location = new System.Drawing.Point(3, 139);
            this.lDamage.Name = "lDamage";
            this.lDamage.Size = new System.Drawing.Size(46, 31);
            this.lDamage.TabIndex = 2;
            this.lDamage.Text = "11";
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lName.Location = new System.Drawing.Point(19, 109);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(0, 29);
            this.lName.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(45, 112);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // Carde
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lName);
            this.Controls.Add(this.lDamage);
            this.Controls.Add(this.lHealth);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Carde";
            this.Size = new System.Drawing.Size(114, 173);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lHealth;
        private System.Windows.Forms.Label lDamage;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
