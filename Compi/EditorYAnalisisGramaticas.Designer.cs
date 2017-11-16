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
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.abrirGramaticaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crearGramaticaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarGramaticaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analizaCodigo = new System.Windows.Forms.ToolStripButton();
            this.btnSiguienteYPrim = new System.Windows.Forms.ToolStripDropDownButton();
            this.conjuntoPrimeroYSiguienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.lR1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.gramatica = new System.Windows.Forms.Label();
            this.tabla = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tablaLr1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tablaAcciones = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCadenaEntrada = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btn_getEnterString = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Plum;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.analizaCodigo,
            this.btnSiguienteYPrim,
            this.toolStripDropDownButton3,
            this.btnSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1151, 55);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirGramaticaToolStripMenuItem,
            this.crearGramaticaToolStripMenuItem,
            this.guardarGramaticaToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(61, 52);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // abrirGramaticaToolStripMenuItem
            // 
            this.abrirGramaticaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("abrirGramaticaToolStripMenuItem.Image")));
            this.abrirGramaticaToolStripMenuItem.Name = "abrirGramaticaToolStripMenuItem";
            this.abrirGramaticaToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.abrirGramaticaToolStripMenuItem.Text = "&Abrir gramatica";
            this.abrirGramaticaToolStripMenuItem.Click += new System.EventHandler(this.btnAbrirGramatica_Click);
            // 
            // crearGramaticaToolStripMenuItem
            // 
            this.crearGramaticaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("crearGramaticaToolStripMenuItem.Image")));
            this.crearGramaticaToolStripMenuItem.Name = "crearGramaticaToolStripMenuItem";
            this.crearGramaticaToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.crearGramaticaToolStripMenuItem.Text = "&Crear gramatica";
            this.crearGramaticaToolStripMenuItem.Click += new System.EventHandler(this.btnNuevaGramatica_Click);
            // 
            // guardarGramaticaToolStripMenuItem
            // 
            this.guardarGramaticaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("guardarGramaticaToolStripMenuItem.Image")));
            this.guardarGramaticaToolStripMenuItem.Name = "guardarGramaticaToolStripMenuItem";
            this.guardarGramaticaToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.guardarGramaticaToolStripMenuItem.Text = "&Guardar gramatica";
            this.guardarGramaticaToolStripMenuItem.Click += new System.EventHandler(this.btnGuarda_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("salirToolStripMenuItem.Image")));
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.salirToolStripMenuItem.Text = "&Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // analizaCodigo
            // 
            this.analizaCodigo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.analizaCodigo.Enabled = false;
            this.analizaCodigo.Image = ((System.Drawing.Image)(resources.GetObject("analizaCodigo.Image")));
            this.analizaCodigo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.analizaCodigo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.analizaCodigo.Name = "analizaCodigo";
            this.analizaCodigo.Size = new System.Drawing.Size(52, 52);
            this.analizaCodigo.Text = "Analiza codigo";
            this.analizaCodigo.Click += new System.EventHandler(this.analizaCodigo_Click);
            // 
            // btnSiguienteYPrim
            // 
            this.btnSiguienteYPrim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSiguienteYPrim.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conjuntoPrimeroYSiguienteToolStripMenuItem});
            this.btnSiguienteYPrim.Enabled = false;
            this.btnSiguienteYPrim.Image = ((System.Drawing.Image)(resources.GetObject("btnSiguienteYPrim.Image")));
            this.btnSiguienteYPrim.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSiguienteYPrim.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSiguienteYPrim.Name = "btnSiguienteYPrim";
            this.btnSiguienteYPrim.Size = new System.Drawing.Size(61, 52);
            this.btnSiguienteYPrim.Text = "Conjunto Primero Y Siguiente";
            // 
            // conjuntoPrimeroYSiguienteToolStripMenuItem
            // 
            this.conjuntoPrimeroYSiguienteToolStripMenuItem.Name = "conjuntoPrimeroYSiguienteToolStripMenuItem";
            this.conjuntoPrimeroYSiguienteToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.conjuntoPrimeroYSiguienteToolStripMenuItem.Text = "Conjunto primero y siguiente";
            this.conjuntoPrimeroYSiguienteToolStripMenuItem.Click += new System.EventHandler(this.conjuntoPrimeroYSiguienteToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lR1ToolStripMenuItem});
            this.toolStripDropDownButton3.Enabled = false;
            this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
            this.toolStripDropDownButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(45, 52);
            this.toolStripDropDownButton3.Text = "LR1";
            this.toolStripDropDownButton3.Click += new System.EventHandler(this.toolStripDropDownButton3_Click);
            // 
            // lR1ToolStripMenuItem
            // 
            this.lR1ToolStripMenuItem.Name = "lR1ToolStripMenuItem";
            this.lR1ToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.lR1ToolStripMenuItem.Text = "LR1";
            this.lR1ToolStripMenuItem.Click += new System.EventHandler(this.lR1ToolStripMenuItem_Click);
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
            this.tabla.Location = new System.Drawing.Point(477, 76);
            this.tabla.Name = "tabla";
            this.tabla.Size = new System.Drawing.Size(0, 19);
            this.tabla.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 104);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(427, 182);
            this.tabControl1.TabIndex = 9;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 304);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(427, 82);
            this.textBox1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(452, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tabla de analisis sintáctico LR1:";
            // 
            // tablaLr1
            // 
            this.tablaLr1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.tablaLr1.GridLines = true;
            this.tablaLr1.Location = new System.Drawing.Point(456, 104);
            this.tablaLr1.Name = "tablaLr1";
            this.tablaLr1.Size = new System.Drawing.Size(320, 282);
            this.tablaLr1.TabIndex = 11;
            this.tablaLr1.UseCompatibleStateImageBehavior = false;
            this.tablaLr1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Estados";
            this.columnHeader1.Width = 190;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl2.Location = new System.Drawing.Point(456, 433);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(683, 220);
            this.tabControl2.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(675, 189);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "<#Codigo";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.richTextBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(675, 189);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Consola";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.SlateGray;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.PanWest;
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(6, 6);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(894, 408);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = ">>>";
            // 
            // tablaAcciones
            // 
            this.tablaAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablaAcciones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.tablaAcciones.GridLines = true;
            this.tablaAcciones.Location = new System.Drawing.Point(782, 104);
            this.tablaAcciones.Name = "tablaAcciones";
            this.tablaAcciones.Size = new System.Drawing.Size(357, 282);
            this.tablaAcciones.TabIndex = 14;
            this.tablaAcciones.UseCompatibleStateImageBehavior = false;
            this.tablaAcciones.View = System.Windows.Forms.View.Details;
            this.tablaAcciones.SelectedIndexChanged += new System.EventHandler(this.tablaAcciones_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Pila";
            this.columnHeader2.Width = 123;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Cadena";
            this.columnHeader3.Width = 102;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Accion";
            this.columnHeader4.Width = 177;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(778, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 19);
            this.label2.TabIndex = 15;
            this.label2.Text = "Tabla de Acciones";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // buttonCadenaEntrada
            // 
            this.buttonCadenaEntrada.Enabled = false;
            this.buttonCadenaEntrada.Location = new System.Drawing.Point(908, 392);
            this.buttonCadenaEntrada.Name = "buttonCadenaEntrada";
            this.buttonCadenaEntrada.Size = new System.Drawing.Size(192, 38);
            this.buttonCadenaEntrada.TabIndex = 16;
            this.buttonCadenaEntrada.Text = "Analiza cadena de entrada";
            this.buttonCadenaEntrada.UseVisualStyleBackColor = true;
            this.buttonCadenaEntrada.Click += new System.EventHandler(this.buttonCadenaEntrada_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(13, 405);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(426, 248);
            this.treeView1.TabIndex = 17;
            // 
            // btn_getEnterString
            // 
            this.btn_getEnterString.Location = new System.Drawing.Point(640, 392);
            this.btn_getEnterString.Name = "btn_getEnterString";
            this.btn_getEnterString.Size = new System.Drawing.Size(192, 38);
            this.btn_getEnterString.TabIndex = 18;
            this.btn_getEnterString.Text = "Carga cadena de entrada";
            this.btn_getEnterString.UseVisualStyleBackColor = true;
            this.btn_getEnterString.Click += new System.EventHandler(this.btn_getEnterString_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Thistle;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(24, 701);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(1111, 13);
            this.textBox2.TabIndex = 19;
            // 
            // editorGramatica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(1151, 741);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btn_getEnterString);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.buttonCadenaEntrada);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tablaAcciones);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tablaLr1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tabla);
            this.Controls.Add(this.gramatica);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "editorGramatica";
            this.Text = "Editor y Analizador de gramaticas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.editorGramatica_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.Label gramatica;
        private System.Windows.Forms.Label tabla;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripButton analizaCodigo;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem abrirGramaticaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crearGramaticaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarGramaticaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton btnSiguienteYPrim;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem lR1ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView tablaLr1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ListView tablaAcciones;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCadenaEntrada;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btn_getEnterString;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ToolStripMenuItem conjuntoPrimeroYSiguienteToolStripMenuItem;
    }
}