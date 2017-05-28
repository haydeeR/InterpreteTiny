namespace Compi
{
    partial class NuevaGramatica
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
            this.textNombreGramatica = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCrearGramatica = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textNombreGramatica
            // 
            this.textNombreGramatica.Location = new System.Drawing.Point(161, 42);
            this.textNombreGramatica.Name = "textNombreGramatica";
            this.textNombreGramatica.Size = new System.Drawing.Size(196, 20);
            this.textNombreGramatica.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre de la gramatica:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(15, 87);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCrearGramatica
            // 
            this.btnCrearGramatica.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCrearGramatica.Location = new System.Drawing.Point(282, 87);
            this.btnCrearGramatica.Name = "btnCrearGramatica";
            this.btnCrearGramatica.Size = new System.Drawing.Size(75, 23);
            this.btnCrearGramatica.TabIndex = 3;
            this.btnCrearGramatica.Text = "Crear";
            this.btnCrearGramatica.UseVisualStyleBackColor = true;
            // 
            // NuevaGramatica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(369, 122);
            this.Controls.Add(this.btnCrearGramatica);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textNombreGramatica);
            this.Name = "NuevaGramatica";
            this.Text = "Crear nueva gramatica";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textNombreGramatica;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCrearGramatica;
    }
}