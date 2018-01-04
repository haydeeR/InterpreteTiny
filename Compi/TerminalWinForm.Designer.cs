namespace Compi
{
    partial class TerminalWinForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerminalWinForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtOutputTinyProgram = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumLinea = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.txtNumCuadruplo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEjecutaSiguiente = new System.Windows.Forms.Button();
            this.toolTipPrompt = new System.Windows.Forms.ToolTip(this.components);
            this.txtLineaEjecucion = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtOutputTinyProgram, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1008, 729);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtOutputTinyProgram
            // 
            this.txtOutputTinyProgram.BackColor = System.Drawing.Color.Black;
            this.txtOutputTinyProgram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutputTinyProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputTinyProgram.ForeColor = System.Drawing.Color.Lime;
            this.txtOutputTinyProgram.Location = new System.Drawing.Point(3, 54);
            this.txtOutputTinyProgram.Multiline = true;
            this.txtOutputTinyProgram.Name = "txtOutputTinyProgram";
            this.txtOutputTinyProgram.ReadOnly = true;
            this.txtOutputTinyProgram.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutputTinyProgram.Size = new System.Drawing.Size(1002, 672);
            this.txtOutputTinyProgram.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnEjecutaSiguiente, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtLineaEjecucion, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1002, 45);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtNumLinea, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(194, 39);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "# Línea:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNumLinea
            // 
            this.txtNumLinea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNumLinea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumLinea.Location = new System.Drawing.Point(100, 3);
            this.txtNumLinea.Multiline = true;
            this.txtNumLinea.Name = "txtNumLinea";
            this.txtNumLinea.Size = new System.Drawing.Size(91, 33);
            this.txtNumLinea.TabIndex = 1;
            this.txtNumLinea.Text = "123456";
            this.txtNumLinea.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Controls.Add(this.txtNumCuadruplo, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(203, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(194, 39);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // txtNumCuadruplo
            // 
            this.txtNumCuadruplo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNumCuadruplo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumCuadruplo.Location = new System.Drawing.Point(148, 3);
            this.txtNumCuadruplo.Multiline = true;
            this.txtNumCuadruplo.Name = "txtNumCuadruplo";
            this.txtNumCuadruplo.Size = new System.Drawing.Size(43, 33);
            this.txtNumCuadruplo.TabIndex = 2;
            this.txtNumCuadruplo.Text = "1234";
            this.txtNumCuadruplo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 39);
            this.label2.TabIndex = 1;
            this.label2.Text = "# Cuadruplo:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnEjecutaSiguiente
            // 
            this.btnEjecutaSiguiente.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEjecutaSiguiente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEjecutaSiguiente.Image = ((System.Drawing.Image)(resources.GetObject("btnEjecutaSiguiente.Image")));
            this.btnEjecutaSiguiente.Location = new System.Drawing.Point(403, 3);
            this.btnEjecutaSiguiente.Name = "btnEjecutaSiguiente";
            this.btnEjecutaSiguiente.Size = new System.Drawing.Size(194, 39);
            this.btnEjecutaSiguiente.TabIndex = 4;
            this.btnEjecutaSiguiente.Text = "Ejecuta Siguiente";
            this.btnEjecutaSiguiente.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnEjecutaSiguiente.UseVisualStyleBackColor = false;
            this.btnEjecutaSiguiente.Click += new System.EventHandler(this.btnEjecutaSiguiente_Click);
            // 
            // toolTipPrompt
            // 
            this.toolTipPrompt.BackColor = System.Drawing.Color.Black;
            this.toolTipPrompt.ForeColor = System.Drawing.Color.Lime;
            // 
            // txtLineaEjecucion
            // 
            this.txtLineaEjecucion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLineaEjecucion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLineaEjecucion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLineaEjecucion.ForeColor = System.Drawing.Color.DarkRed;
            this.txtLineaEjecucion.Location = new System.Drawing.Point(603, 3);
            this.txtLineaEjecucion.Multiline = true;
            this.txtLineaEjecucion.Name = "txtLineaEjecucion";
            this.txtLineaEjecucion.ReadOnly = true;
            this.txtLineaEjecucion.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtLineaEjecucion.Size = new System.Drawing.Size(396, 39);
            this.txtLineaEjecucion.TabIndex = 7;
            this.txtLineaEjecucion.Text = "Linea Ejecución";
            // 
            // TerminalWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "TerminalWinForm";
            this.Text = "Tiny Terminal";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtOutputTinyProgram;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumLinea;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TextBox txtNumCuadruplo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEjecutaSiguiente;
        private System.Windows.Forms.ToolTip toolTipPrompt;
        private System.Windows.Forms.TextBox txtLineaEjecucion;
    }
}