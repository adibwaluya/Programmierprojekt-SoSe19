namespace OpenGlUserControl
{
    partial class WinFormsControl
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.WinformsControlPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // WinformsControlPanel
            // 
            this.WinformsControlPanel.AutoSize = true;
            this.WinformsControlPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.WinformsControlPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.WinformsControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WinformsControlPanel.Location = new System.Drawing.Point(0, 0);
            this.WinformsControlPanel.Name = "WinformsControlPanel";
            this.WinformsControlPanel.Size = new System.Drawing.Size(800, 450);
            this.WinformsControlPanel.TabIndex = 0;
            // 
            // WinFormsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CausesValidation = false;
            this.Controls.Add(this.WinformsControlPanel);
            this.Name = "WinFormsControl";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.WinFormsControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel WinformsControlPanel;
    }
}
