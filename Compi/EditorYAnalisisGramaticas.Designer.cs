namespace Compi
{
    partial class editorGramatica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(editorGramatica));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAbrirGramatica = new System.Windows.Forms.ToolStripButton();
            this.btnNuevaGramatica = new System.Windows.Forms.ToolStripButton();
            this.btnNuevaProduccion = new System.Windows.Forms.ToolStripButton();
            this.btnGuarda = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.colProducciones = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gramatica = new System.Windows.Forms.Label();
            this.tabla = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFirstAndNext = new System.Windows.Forms.ToolStripButton();
            this.listViewPrimero = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSiguiente = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Plum;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAbrirGramatica,
            this.btnNuevaGramatica,
            this.btnNuevaProduccion,
            this.btnGuarda,
            this.btnSalir,
            this.btnFirstAndNext,
            this.btnSiguiente});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(927, 55);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAbrirGramatica
            // 
            this.btnAbrirGramatica.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrirGramatica.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrirGramatica.Image")));
            this.btnAbrirGramatica.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAbrirGramatica.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrirGramatica.Name = "btnAbrirGramatica";
            this.btnAbrirGramatica.Size = new System.Drawing.Size(52, 52);
            this.btnAbrirGramatica.Text = "Abrir una gramatica";
            this.btnAbrirGramatica.ToolTipText = "Abrir una gramatica existente";
            this.btnAbrirGramatica.Click += new System.EventHandler(this.btnAbrirGramatica_Click);
            // 
            // btnNuevaGramatica
            // 
            this.btnNuevaGramatica.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevaGramatica.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevaGramatica.Image")));
            this.btnNuevaGramatica.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevaGramatica.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevaGramatica.Name = "btnNuevaGramatica";
            this.btnNuevaGramatica.Size = new System.Drawing.Size(52, 52);
            this.btnNuevaGramatica.Text = "Crear nueva gramatica";
            this.btnNuevaGramatica.Click += new System.EventHandler(this.btnNuevaGramatica_Click);
            // 
            // btnNuevaProduccion
            // 
            this.btnNuevaProduccion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevaProduccion.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevaProduccion.Image")));
            this.btnNuevaProduccion.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevaProduccion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevaProduccion.Name = "btnNuevaProduccion";
            this.btnNuevaProduccion.Size = new System.Drawing.Size(52, 52);
            this.btnNuevaProduccion.Text = "Agrega una produccion";
            this.btnNuevaProduccion.Click += new System.EventHandler(this.btnNuevaProduccion_Click);
            // 
            // btnGuarda
            // 
            this.btnGuarda.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuarda.Image = ((System.Drawing.Image)(resources.GetObject("btnGuarda.Image")));
            this.btnGuarda.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnGuarda.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuarda.Name = "btnGuarda";
            this.btnGuarda.Size = new System.Drawing.Size(52, 52);
            this.btnGuarda.Text = "Guardar Gramatica";
            this.btnGuarda.Click += new System.EventHandler(this.btnGuarda_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(52, 52);
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colProducciones});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 104);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(196, 274);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // colProducciones
            // 
            this.colProducciones.Text = "Producciones";
            this.colProducciones.Width = 190;
            // 
            // gramatica
            // 
            this.gramatica.AutoSize = true;
            this.gramatica.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gramatica.Location = new System.Drawing.Point(8, 76);
            this.gramatica.Name = "gramatica";
            this.gramatica.Size = new System.Drawing.Size(86, 19);
            this.gramatica.TabIndex = 3;
            this.gramatica.Text = "Gramatica:";
            // 
            // tabla
            // 
            this.tabla.AutoSize = true;
            this.tabla.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabla.Location = new System.Drawing.Point(229, 76);
            this.tabla.Name = "tabla";
            this.tabla.Size = new System.Drawing.Size(135, 19);
            this.tabla.TabIndex = 4;
            this.tabla.Text = "Conjunto primero";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(455, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Conjunto siguiente";
            // 
            // btnFirstAndNext
            // 
            this.btnFirstAndNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFirstAndNext.Image = ((System.Drawing.Image)(resources.GetObject("btnFirstAndNext.Image")));
            this.btnFirstAndNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFirstAndNext.Name = "btnFirstAndNext";
            this.btnFirstAndNext.Size = new System.Drawing.Size(23, 52);
            this.btnFirstAndNext.Text = "tablaPrimeroYsiguiente";
            this.btnFirstAndNext.Click += new System.EventHandler(this.btnFirstAndNext_Click);
            // 
            // listViewPrimero
            // 
            this.listViewPrimero.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewPrimero.GridLines = true;
            this.listViewPrimero.Location = new System.Drawing.Point(233, 104);
            this.listViewPrimero.Name = "listViewPrimero";
            this.listViewPrimero.Size = new System.Drawing.Size(196, 274);
            this.listViewPrimero.TabIndex = 6;
            this.listViewPrimero.UseCompatibleStateImageBehavior = false;
            this.listViewPrimero.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Conjunto Primero";
            this.columnHeader1.Width = 190;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView2.GridLines = true;
            this.listView2.Location = new System.Drawing.Point(459, 104);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(196, 274);
            this.listView2.TabIndex = 7;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Conjunto Siguiente";
            this.columnHeader2.Width = 190;
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSiguiente.Image = ((System.Drawing.Image)(resources.GetObject("btnSiguiente.Image")));
            this.btnSiguiente.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(23, 52);
            this.btnSiguiente.Text = "conjuntoSiguiente";
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // editorGramatica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(927, 429);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listViewPrimero);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabla);
            this.Controls.Add(this.gramatica);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "editorGramatica";
            this.Text = "Editor y Analizador de gramaticas";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNuevaGramatica;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colProducciones;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStripButton btnAbrirGramatica;
        private System.Windows.Forms.ToolStripButton btnNuevaProduccion;
        private System.Windows.Forms.ToolStripButton btnGuarda;
        private System.Windows.Forms.Label gramatica;
        private System.Windows.Forms.Label tabla;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton btnFirstAndNext;
        private System.Windows.Forms.ListView listViewPrimero;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripButton btnSiguiente;
    }
}