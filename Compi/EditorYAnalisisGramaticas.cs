using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Compi
{
    public partial class editorGramatica : Form
    {
        /// <summary>
        /// Variable para la gramatica a agregar
        /// </summary>
        private Gramatica g = null;
        /// <summary>
        /// Indice de la tab de la gramatica que se esta usando
        /// </summary>
        private int tabIndex = 0;
        /// <summary>
        /// Variable para los headers de los listview del LR1
        /// </summary>
        private List<String> cabeceras = null;
        /// <summary>
        /// Contenido de la tabla de analisis sintactico
        /// </summary>
        private List<List<String>> tablaSint = null;
        /// <summary>
        /// Esta variable es solo para visualizar la barra en en la introduccón
        /// </summary>
        private ini formini = null;

        private ListViewItem lvAux = null;

        private ListViewItem.ListViewSubItem lvSubItem = null;

        private string fullFileName;

        private TablaDesplazamientos tablaDesplazamientos = null;

        public editorGramatica(ini i)
        {
            this.formini = i;
            InitializeComponent();
        }

        /// <summary>
        /// Metodo que crea un archivo con extension .gram y una nueva tab
        /// en el edito de gramaticas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNuevaGramatica_Click(object sender, EventArgs e)
        {
            NuevaGramatica newG = new NuevaGramatica();
            if (newG.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((newG.getNombre().Trim()) != "")
                    {
                        Stream stream = new FileStream(newG.getNombre() + ".txt", FileMode.Create, FileAccess.Write, FileShare.None);
                        g = new Gramatica(newG.getNombre());
                    }
                    else
                        MessageBox.Show("No se puede crear una gramatica sin nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: No se puede leer el archivo del disco, Error Original:" + ex.Message);
                }
            }
        }
        /// <summary>
        /// Metodo para terminar la aplicacion 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.formini.Close();
            this.Close();
        }

        /// <summary>
        /// Metodo que abre un archivo e intancia una gramatica a partir de el
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbrirGramatica_Click(object sender, EventArgs e)
        {
            int band = 0;
            string pathFile = "";
            string txtOfProgram = "";
            OpenFileDialog fnew = new OpenFileDialog();

            if (fnew.ShowDialog() == DialogResult.OK && fnew.FileName != "")
            {
                StreamReader sr = new StreamReader(fnew.FileName);
                pathFile = fnew.FileName;
                var fileName = Path.GetFileName(fnew.FileName);
                //Si tiene ya un archivo el tabControl agrega una pestaña mas 
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                    if (fileName == tabControl1.TabPages[i].Text)
                        band = 1;
                if (band == 0)
                {
                    //Se carga el contenido del programa para ponerlo linea por linea en el tabControl
                    txtOfProgram = sr.ReadToEnd();
                    tabControl1.TabPages.Add(fileName);
                    tabIndex = tabControl1.TabPages.Count - 1;
                    tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(CreaText(txtOfProgram, Color.White, Color.Black, tabControl1));
                    tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(imprimeCodigo(pathFile));
                    tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(imprimeCodigo(""));
                    tabIndex = tabControl1.TabPages.Count - 1;
                }
                //Se cierra el archivo que se abrio
                sr.Close();
                analizaCodigo.Enabled = true;
                this.g = new Gramatica(fileName);
            }
        }

        /// <summary>
        /// Método que analiza lexicamente la gramatica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void analizaCodigo_Click(object sender, EventArgs e)
        {
            string aLine = "", nullLine = "";
            string rutaError = rutaModificada(tabControl1.TabPages[tabIndex].Controls[1].Text);
            if (File.Exists(rutaError))
                File.Delete(rutaError);

            //Se analiza con el analizador lexico
            StringReader strReader = new StringReader(this.tabControl1.TabPages[this.tabIndex].Controls[0].Text);
            while ((aLine = strReader.ReadLine()) != null)
            {
                if (aLine != null)
                {
                    nullLine = aLine.Trim();
                    if (nullLine != "")
                        if (this.g.evaluaProduccion(aLine) == false)
                        {
                            this.textBox1.BackColor = Color.Red;
                            this.textBox1.ForeColor = Color.White;
                            this.textBox1.Text = "Existe un error en la linea: " + aLine;
                        }
                }
                else
                    break;
            }
            this.btnSiguienteYPrim.Enabled = true;
            this.toolStripDropDownButton3.Enabled = true;
            this.textBox1.BackColor = Color.Green;
            this.textBox1.ForeColor = Color.White;
            this.textBox1.Text = "Gramatica escrita correctamente";
            /*
            abrirArchError(actual2);
            if (!File.Exists(actual2))
                textBox1.Text = "No se encontraron errores";
            */
        }

        public void abrirArchError(string rutaArch)
        {
            if (File.Exists(rutaArch))
            {
                StreamReader sr = new StreamReader(rutaArch);
                //Contiene el texto del codigo fuente
                var errorTxt = sr.ReadToEnd();
                sr.Close();
                textBox1.Text = errorTxt;
            }
            else
                textBox1.Text = "No se encontraron errores";

        }

        public string rutaModificada(string ruta)
        {
            return Path.GetDirectoryName(ruta) + "\\" + "errores" + Path.GetFileName(ruta);
        }

        public RichTextBox CreaText(string txt, Color backColor, Color textColor, TabControl forWho)
        {
            RichTextBox text = new RichTextBox();
            text.Text = txt;
            text.Multiline = true;
            text.Width = forWho.Width - 10;
            text.Height = forWho.Height - 30;
            text.Enabled = true;
            text.AcceptsTab = true;
            text.TextChanged += MyTB_TextChanged;
            text.BackColor = backColor;
            text.ForeColor = textColor;
            return text;
        }

        /// <summary>
        /// Referencia de funcion que muestra cual es la tab modificada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTB_TextChanged(object sender, EventArgs e)
        {
            if (tabIndex == -1)
                tabIndex = 0;
            tabControl1.TabPages[tabIndex].Tag = "modif";
        }

        /// <summary>
        /// Submetodo de abrir archivo este archivo se encarga de imprimir 
        /// </summary>
        /// <param name="rutaArchivo"></param>
        /// <returns></returns>
        public Label imprimeCodigo(string rutaArchivo)
        {
            Label label = new Label();
            label.Text = rutaArchivo;
            return label;

        }
        /**
         * Metodo que guarda una produccion y va serializando la gramatica completa
         * con extension .txt
         */
        private void btnGuarda_Click(object sender, EventArgs e)
        {
            if (tabIndex != -1 && tabControl1.TabPages[tabIndex].Tag.ToString() == "nomodif")
            {
                StreamWriter myStream;
                myStream = new System.IO.StreamWriter(tabControl1.TabPages[tabIndex].Controls[1].Text);
                myStream.Write(tabControl1.TabPages[tabIndex].Controls[0].Text);
                myStream.Close();
                tabControl1.TabPages[tabIndex].Tag = "nomodif";
            }
            else
                guardarComo(null, null);
        }

        private void guardarComo(object sender, EventArgs e)
        {
            StreamWriter myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            if (tabIndex != -1)
            {
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = tabControl1.TabPages[tabIndex].Text;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    myStream = new System.IO.StreamWriter(saveFileDialog1.FileName);
                    myStream.Write(tabControl1.TabPages[tabIndex].Controls[0].Text);
                    myStream.Close();
                    tabControl1.TabPages[tabIndex].Controls[1].Text = saveFileDialog1.FileName;
                    tabControl1.TabPages[tabIndex].Tag = "nomodif";
                }
            }
        }

        public void reducePila(int ind, List<string> pila)
        {
            for (int i = pila.Count() - 1; i >= ind; i--)
                pila.Remove(pila[i]);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void lR1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (g != null && g.getNTerminal() != null)
            {
                g.constructorLR1(this);
                g.llenarTablaLR1();
                this.dibujaAFD();
            }
            else
            {
                MessageBox.Show("Esta gramatica  no tiene terminales");
            }
        }

        /// <summary>
        /// Metodo que dibuja el afd en el treeview
        /// </summary>
        public void dibujaAFD()
        {
            List<EdoLR1> listEdos = g.getListaEdos();
            EdoLR1 edoBase = listEdos[0];

            // Add a root TreeNode for each Edo LR1 object in the ArrayList.
            foreach (EdoLR1 edo in listEdos)
            {
                treeView1.Nodes.Add(new TreeNode(edo.getTokenDeLlegada()));
                // Add a child treenode for each Order object in the current EdoLR1 object.
                foreach (Produccion p1 in edo.getProducciones())
                {
                    treeView1.Nodes[listEdos.IndexOf(edo)].Nodes.Add(
                      new TreeNode(p1.getTokensAsString(p1.getTokens().IndexOf(edo.getTokenDeLlegada()))) + " | " + p1.getTokenBusqueda());
                }
            }
            //  this.treeView1.Nodes.Add(nodoBase);
        }


        public void muestraTermYNTerm()
        {
            List<string> nTerminales = g.getNTerminal();
            List<string> terminales = g.getTerminales();
            this.cabeceras = new List<string>();
            this.tablaDesplazamientos = new TablaDesplazamientos();

            tablaLr1.Clear();
            tablaLr1.Columns.Add("Estados");
            foreach (string term in terminales)
            {
                tablaLr1.Columns.Add(term);
                this.cabeceras.Add(term);
                this.tablaDesplazamientos.agregaColumna(term);
            }
            tablaLr1.Columns.Add("$");
            this.cabeceras.Add("$");
            this.tablaDesplazamientos.agregaColumna("$");
            foreach (string nterm in nTerminales)
            {
                tablaLr1.Columns.Add(nterm);
                this.cabeceras.Add(nterm);
                this.tablaDesplazamientos.agregaColumna(nterm);
            }
            this.llenarTablaLR1();
        }

        public void llenarTablaLR1()
        {
            List<EdoLR1> listEdos = g.getListaEdos();
            ListViewItem lvAux = null;
            ListViewItem.ListViewSubItem lvSubItem = null;
            String[] arrayReduccion = null;
            string tokenDeBusqueda = "";

            //TablaDesplazamientos tabDesp = new TablaDesplazamientos(listEdos);
            this.tablaDesplazamientos.Estados = listEdos;
            foreach (EdoLR1 e in listEdos)
            {
                lvAux = tablaLr1.Items.Add(e.getId().ToString());
                foreach (string cabecera in this.cabeceras)
                {
                    //tabDesp.agregaColumna(cabecera);
                    this.tablaDesplazamientos.agregaValor(cabecera, " -- ");
                    lvSubItem = lvAux.SubItems.Add(" -- ");
                    if (e.getListArista().Count > 0)
                    {
                        AristaLR1 a = e.getListArista().FirstOrDefault(ar => ar.getEdoDestino().getTokenDeLlegada() == cabecera);

                        if (a != null)
                        {
                            lvSubItem.Text = a.getAccion();
                            this.tablaDesplazamientos.cambiaValor(listEdos.IndexOf(e), cabecera, a.getAccion());
                        }

                        //foreach (AristaLR1 a in e.getListArista())
                        //{
                        //    if (a.getEdoDestino().getTokenDeLlegada() == cabecera)
                        //    {
                        //        lvSubItem.Text = a.getAccion();
                        //    }
                        //}
                    }
                    if (cabecera.Length - 1 == '\'')
                    {
                        lvSubItem.Text = e.getAccion();
                    }
                    if (e.listReducciones.Count > 0)
                    {
                        var reduccion = e.listReducciones.FirstOrDefault(x => x.Split('#')[0] != "<" && x.Split('#')[0] != ">" ? x.Split('#')[0].Contains(cabecera) : (@"\" + x.Split('#')[0]).Contains(cabecera));

                        if (reduccion != null)
                        {
                            lvSubItem.Text = reduccion.Split('#')[1];
                            this.tablaDesplazamientos.cambiaValor(listEdos.IndexOf(e), cabecera, reduccion.Split('#')[1]);
                        }
                        /*
                        foreach (string reduccion in e.listReducciones)
                        {
                            arrayReduccion = reduccion.Split('#');
                            tokenDeBusqueda = arrayReduccion[0];
                            if (tokenDeBusqueda == ">" || tokenDeBusqueda == "<")
                                tokenDeBusqueda = @"\" + tokenDeBusqueda;

                            if (tokenDeBusqueda == cabecera)
                            {
                                lvSubItem.Text = arrayReduccion[1];
                                this.tablaDesplazamientos.cambiaValor(listEdos.IndexOf(e), cabecera, arrayReduccion[1]);
                            }
                        }*/
                    }
                }
            }
        }

        private void tablaAcciones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PagCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonCadenaEntrada_Click(object sender, EventArgs e)
        {
            List<List<DslToken>> listasDeTokens = null;
            List<DslSentence> listasDeSentencias = null;
            AnalizadorLexico analizer = new AnalizadorLexico();
            analizer.tokeniza(this.fullFileName);
            listasDeTokens = analizer.TokenDefinitions;
            listasDeSentencias = analizer.SentenceDefinitions;

            this.llenarTablaAcciones(listasDeSentencias);
            //this.llenarTablaAcciones(listasDeTokens);
        }


        private void tokenizar()
        {

        }


        private void llenarTablaAcciones(List<DslSentence> cadenas)
        {
            List<EdoLR1> estadosAux = g != null ? g.getListaEdos() : null;
            if (estadosAux != null && estadosAux.Count > 0)
            {
                TablaDeAcciones tablaDeAcciones = new TablaDeAcciones(this.tablaDesplazamientos, g.getListaEdos()[0]);

                foreach (DslSentence cadLinea in cadenas)
                {
                    for (int ind = 0; ind < cadLinea.value.Length; ind++)
                    {
                        tablaDeAcciones.agregaCaracter(cadLinea.value[ind].ToString(), estadosAux, cadLinea.value.Substring(ind));
                    }
                }
            }
            //foreach (List<DslToken> listaTokens in cadena)
            //{
            //    foreach (DslToken token in listaTokens)
            //    {
            //        for (int ind = 0; ind < token.Value.Length; ind++)
            //        {
            //            tablaDeAcciones.agregaCaracter(token.Value[ind].ToString(), estadosAux, "");
            //        }
            //    }
            //}
        }





        public void insertaRegistro(string pila, string cadena)
        {
            this.lvAux = this.tablaAcciones.Items.Add(pila);
            this.lvSubItem = this.lvAux.SubItems.Add(cadena);

        }

        public void insertaAccion(string accion)
        {
            this.lvSubItem = this.lvAux.SubItems.Add(accion);
        }

        public void insertaAccionEnTabla(List<String> accion)
        {
            string a = "";
            a = accion[accion.Count() - 1];
            this.insertaAccion(a);
        }

        public void insertaTabla(List<String> pila, String arrCade, int indCadena)
        {
            string p = "", c = "";
            for (int i = 0; i < pila.Count(); i++)
            {
                p += pila[i];
            }
            for (int i = indCadena; i < arrCade.Count(); i++)
            {
                c += arrCade[i];
            }
            this.insertaRegistro(p, c);
        }

        private void btn_getEnterString_Click(object sender, EventArgs e)
        {
            int band = 0;
            string pathFile = "";
            string txtOfProgram = "";
            OpenFileDialog fnew = new OpenFileDialog();

            if (fnew.ShowDialog() == DialogResult.OK && fnew.FileName != "")
            {
                StreamReader sr = new StreamReader(fnew.FileName);
                pathFile = fnew.FileName;
                var fileName = Path.GetFileName(fnew.FileName);
                if (band == 0)
                {
                    txtOfProgram = sr.ReadToEnd();
                    tabControl2.TabPages[0].Tag = fileName;
                    // tabIndex = tabControl2.TabPages.Count - 1;
                    tabControl2.TabPages[0].Controls.Add(CreaText(txtOfProgram, Color.Black, Color.White, tabControl2));
                    tabControl2.TabPages[0].Controls.Add(imprimeCodigo(pathFile));
                    tabControl2.TabPages[0].Controls.Add(imprimeCodigo(""));
                    // tabIndex = tabControl2.TabPages.Count - 1;
                    //Se carga el programa
                    txtOfProgram = sr.ReadToEnd();
                }
                //Se cierra el archivo que se abrio
                sr.Close();
                buttonCadenaEntrada.Enabled = true;
                this.g = new Gramatica(fileName);
                this.fullFileName = pathFile;
            }
        }
        /// <summary>
        /// Opcion del menú para visualizar la ventana modal de los 
        /// conjuntos primero y siguiente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void conjuntoPrimeroYSiguienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> conjuntoPrimero;
            List<string> conjuntoSiguiente;
            if (this.g != null)
            {
                conjuntoPrimero = g.getConjuntoPrimero();
                conjuntoSiguiente = g.getConjuntoSiguiente();
                PrimeroYSiguienteModal FirstAndNextModal = new PrimeroYSiguienteModal();
                FirstAndNextModal.muestraConjunto(conjuntoPrimero, conjuntoSiguiente);
                FirstAndNextModal.Show();
            }
            else
                MessageBox.Show("Antes de solicitar el conjunto primero debe crear o abrir una gramatica");
        }

        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {

        }

        private List<ArbolAS> arboles = new List<ArbolAS>();
        private ArbolAS raiz;

        private void creaArbol()
        {
            arboles = new List<ArbolAS>();

            ArbolAS label_002 = new ArbolAS(new DslToken(TokenType.KeyWord, ":="));
            ArbolAS id_001 = new ArbolAS(new DslToken(TokenType.Id, "_Id001"));
            ArbolAS opMult_001 = new ArbolAS(new DslToken(TokenType.OperadorMult, "*"));
            ArbolAS opMult_001_iz = new ArbolAS(new DslToken(TokenType.Numero, "3"));
            ArbolAS opMult_001_der = new ArbolAS(new DslToken(TokenType.Numero, "4"));
            opMult_001.setNodoIzquierdo(opMult_001_iz);
            opMult_001.setNodoDerecho(opMult_001_der);
            label_002.setNodoIzquierdo(id_001);
            label_002.setNodoDerecho(opMult_001);

            ArbolAS label_001 = new ArbolAS(new DslToken(TokenType.KeyWord, ":="));
            ArbolAS id_002 = new ArbolAS(new DslToken(TokenType.Id, "_Id001"));
            ArbolAS opMult_002 = new ArbolAS(new DslToken(TokenType.OperadorMult, "*"));
            ArbolAS opMult_002_iz = new ArbolAS(new DslToken(TokenType.Numero, "6"));
            ArbolAS opMult_002_der = new ArbolAS(new DslToken(TokenType.Numero, "8"));
            opMult_002.setNodoIzquierdo(opMult_002_iz);
            opMult_002.setNodoDerecho(opMult_002_der);
            label_001.setNodoIzquierdo(id_002);
            label_001.setNodoDerecho(opMult_002);

            ArbolAS else_001 = new ArbolAS(new DslToken(TokenType.KeyWord, "else"));
            else_001.setNodoIzquierdo(label_001);
            else_001.setNodoDerecho(label_002);

            ArbolAS exp_001 = new ArbolAS(new DslToken(TokenType.OperadorComp, "<"));
            ArbolAS operandoComp_001_der = new ArbolAS(new DslToken(TokenType.Numero, "10"));
            ArbolAS exp_002 = new ArbolAS(new DslToken(TokenType.OperadorSuma, "+"));
            ArbolAS id_003 = new ArbolAS(new DslToken(TokenType.Id, "_Id003"));
            ArbolAS opSum_001 = new ArbolAS(new DslToken(TokenType.OperadorMult, "+"));
            ArbolAS opSum_001_iz = new ArbolAS(new DslToken(TokenType.Numero, "6"));
            ArbolAS opSum_001_der = new ArbolAS(new DslToken(TokenType.Numero, "8"));
            opSum_001.setNodoIzquierdo(opSum_001_iz);
            opSum_001.setNodoDerecho(opSum_001_der);
            exp_002.setNodoIzquierdo(id_003);
            exp_002.setNodoDerecho(opSum_001);
            exp_001.setNodoDerecho(operandoComp_001_der);
            exp_001.setNodoIzquierdo(exp_002);

            ArbolAS if_001 = new ArbolAS(new DslToken(TokenType.KeyWord, "if"));
            if_001.setNodoIzquierdo(exp_001);
            if_001.setNodoDerecho(else_001);


            ArbolAS id_004 = new ArbolAS(new DslToken(TokenType.Id, "_Id004"));
            ArbolAS write_001 = new ArbolAS(new DslToken(TokenType.KeyWord, "write"));
            write_001.setNodoIzquierdo(id_004);

            ArbolAS id_005 = new ArbolAS(new DslToken(TokenType.Id, "_Id004"));
            ArbolAS read_001 = new ArbolAS(new DslToken(TokenType.KeyWord, "read"));
            read_001.setNodoIzquierdo(id_005);

            ArbolAS num_001 = new ArbolAS(new DslToken(TokenType.Numero, "7"));
            ArbolAS num_002 = new ArbolAS(new DslToken(TokenType.Numero, "2"));
            ArbolAS opSum_002 = new ArbolAS(new DslToken(TokenType.OperadorSuma, "+"));
            opSum_002.setNodoIzquierdo(num_002);
            opSum_002.setNodoDerecho(num_001);
            ArbolAS id_006 = new ArbolAS(new DslToken(TokenType.Id, "_Id006"));
            ArbolAS assign_001 = new ArbolAS(new DslToken(TokenType.KeyWord, ":="));
            assign_001.setNodoIzquierdo(id_006);
            assign_001.setNodoDerecho(opSum_002);

            arboles.Add(assign_001);
            arboles.Add(read_001);
            arboles.Add(write_001);
            arboles.Add(if_001);
        }
    }
}
