namespace importSTLTest
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.stlSelectBt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // stlSelectBt
            // 
            this.stlSelectBt.Location = new System.Drawing.Point(139, 327);
            this.stlSelectBt.Name = "stlSelectBt";
            this.stlSelectBt.Size = new System.Drawing.Size(75, 23);
            this.stlSelectBt.TabIndex = 0;
            this.stlSelectBt.Text = "Import STL Data";
            this.stlSelectBt.UseVisualStyleBackColor = true;
            this.stlSelectBt.Click += new System.EventHandler(this.stlSelectBt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.stlSelectBt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button stlSelectBt;
    }
}

