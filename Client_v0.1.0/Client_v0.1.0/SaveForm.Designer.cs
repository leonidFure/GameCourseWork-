namespace Client_v0._1._0
{
    partial class SaveForm
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
            this.bOk = new System.Windows.Forms.Button();
            this.bCansel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(12, 51);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(100, 23);
            this.bOk.TabIndex = 0;
            this.bOk.Text = "Save";
            this.bOk.UseVisualStyleBackColor = true;
            // 
            // bCansel
            // 
            this.bCansel.Location = new System.Drawing.Point(129, 51);
            this.bCansel.Name = "bCansel";
            this.bCansel.Size = new System.Drawing.Size(100, 23);
            this.bCansel.TabIndex = 1;
            this.bCansel.Text = "Cansel";
            this.bCansel.UseVisualStyleBackColor = true;
            this.bCansel.Click += new System.EventHandler(this.bCansel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please, write the name of the deck:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(176, 20);
            this.textBox1.TabIndex = 3;
            // 
            // SaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 89);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bCansel);
            this.Controls.Add(this.bOk);
            this.Name = "SaveForm";
            this.Text = "SaveForm";
            this.Load += new System.EventHandler(this.SaveForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Button bCansel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}