namespace Compi
{
    partial class VisorArbolAbstracto
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
            this.ctrlTreeViewer = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // ctrlTreeViewer
            // 
            this.ctrlTreeViewer.BackColor = System.Drawing.Color.Black;
            this.ctrlTreeViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlTreeViewer.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlTreeViewer.ForeColor = System.Drawing.Color.Lime;
            this.ctrlTreeViewer.Indent = 25;
            this.ctrlTreeViewer.ItemHeight = 30;
            this.ctrlTreeViewer.LineColor = System.Drawing.Color.Snow;
            this.ctrlTreeViewer.Location = new System.Drawing.Point(0, 0);
            this.ctrlTreeViewer.Margin = new System.Windows.Forms.Padding(10);
            this.ctrlTreeViewer.Name = "ctrlTreeViewer";
            this.ctrlTreeViewer.PathSeparator = "\\\\";
            this.ctrlTreeViewer.Size = new System.Drawing.Size(784, 561);
            this.ctrlTreeViewer.TabIndex = 0;
            // 
            // VisorArbolAbstracto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Plum;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.ctrlTreeViewer);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "VisorArbolAbstracto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visor de Árbol Abstracto";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VisorArbolAbstracto_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView ctrlTreeViewer;
    }
}