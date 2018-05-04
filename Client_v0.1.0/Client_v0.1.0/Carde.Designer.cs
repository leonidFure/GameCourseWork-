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
            this.lHealth = new System.Windows.Forms.Label();
            this.lDamage = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lHealth
            // 
            this.lHealth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lHealth.ForeColor = System.Drawing.Color.White;
            this.lHealth.Location = new System.Drawing.Point(64, 139);
            this.lHealth.Name = "lHealth";
            this.lHealth.Size = new System.Drawing.Size(46, 31);
            this.lHealth.TabIndex = 1;
            this.lHealth.Text = "11";
            this.lHealth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lDamage
            // 
            this.lDamage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lDamage.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lDamage.ForeColor = System.Drawing.Color.White;
            this.lDamage.Location = new System.Drawing.Point(3, 139);
            this.lDamage.Name = "lDamage";
            this.lDamage.Size = new System.Drawing.Size(46, 31);
            this.lDamage.TabIndex = 2;
            this.lDamage.Text = "11";
            this.lDamage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lName
            // 
            this.lName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lName.Font = new System.Drawing.Font("Wingdings 3", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lName.Location = new System.Drawing.Point(0, 110);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(114, 29);
            this.lName.TabIndex = 3;
            this.lName.Text = "dsdsdsads";
            this.lName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Carde
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.lName);
            this.Controls.Add(this.lDamage);
            this.Controls.Add(this.lHealth);
            this.Name = "Carde";
            this.Size = new System.Drawing.Size(114, 173);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lHealth;
        private System.Windows.Forms.Label lDamage;
        private System.Windows.Forms.Label lName;
    }
}
