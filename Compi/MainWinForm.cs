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
using Tsimbolos;
using ManejoDeErrores;

namespace Compi
{
    public partial class MainWinForm : Form
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

        private TablaSimbolos tablaSimbolos = null;

        private ListViewItem lvAux = null;

        private ListViewItem.ListViewSubItem lvSubItem = null;

        private string fullFileName = @"Gramatica\Tiny_Final.txt";
        //        private string fullFileName = @"Gramatica\gram_S.txt";

        private string fullFileNamePrograma = string.Empty;

        private TablaDesplazamientos tablaDesplazamientos = null;

        private TablaDeAcciones tablaDeAcciones = null;

        private ArbolAS arbol = null;

        public VisorArbolAbstracto vAA = null;

        public MainWinForm(ini i)
        {
            InitializeComponent();
            this.formini = i;
            this.cargaGramatica();
            this.mainTabControlContent.SelectedTab = this.mainTabControlContent.TabPages[1];
            this.comBoxTipoEjecucion.SelectedIndex = 0;
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
                for (int i = 0; i < mainTabControlContent.TabPages.Count; i++)
                    if (fileName == mainTabControlContent.TabPages[i].Text)
                        band = 1;
                if (band == 0)
                {
                    //Se carga el contenido del programa para ponerlo linea por linea en el tabControl
                    txtOfProgram = sr.ReadToEnd();
                    mainTabControlContent.TabPages.Add(fileName);
                    tabIndex = mainTabControlContent.TabPages.Count - 1;
                    mainTabControlContent.TabPages[mainTabControlContent.TabPages.Count - 1].Controls.Add(CreaText(txtOfProgram, Color.White, Color.Black, mainTabControlContent));
                    mainTabControlContent.TabPages[mainTabControlContent.TabPages.Count - 1].Controls.Add(imprimeCodigo(pathFile));
                    mainTabControlContent.TabPages[mainTabControlContent.TabPages.Count - 1].Controls.Add(imprimeCodigo(""));
                    tabIndex = mainTabControlContent.TabPages.Count - 1;
                }
                //Se cierra el archivo que se abrio
                sr.Close();
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
            this.analizaCodigo_Gramatica();
        }


        private void analizaCodigo_Gramatica()
        {
            string aLine = "", nullLine = "";

            List<string> lineas = this.txtCtrlGramatica.Text.Replace("\r\n", "@").Split('@').ToList();
            for (int ind = 0; ind < lineas.Count; ind++)
            {
                aLine = lineas[ind];
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
            this.textBox1.BackColor = Color.Green;
            this.textBox1.ForeColor = Color.White;
            this.textBox1.Text = "Gramatica escrita correctamente";
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
            mainTabControlContent.TabPages[tabIndex].Tag = "modif";
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
            if (tabIndex != -1 && mainTabControlContent.TabPages[tabIndex].Tag.ToString() == "nomodif")
            {
                StreamWriter myStream;
                myStream = new System.IO.StreamWriter(mainTabControlContent.TabPages[tabIndex].Controls[1].Text);
                myStream.Write(mainTabControlContent.TabPages[tabIndex].Controls[0].Text);
                myStream.Close();
                mainTabControlContent.TabPages[tabIndex].Tag = "nomodif";
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
                saveFileDialog1.FileName = mainTabControlContent.TabPages[tabIndex].Text;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    myStream = new System.IO.StreamWriter(saveFileDialog1.FileName);
                    myStream.Write(mainTabControlContent.TabPages[tabIndex].Controls[0].Text);
                    myStream.Close();
                    mainTabControlContent.TabPages[tabIndex].Controls[1].Text = saveFileDialog1.FileName;
                    mainTabControlContent.TabPages[tabIndex].Tag = "nomodif";
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
                //g.constructorLR1(this);
                //g.llenarTablaLR1();
                //this.dibujaAFD();
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
                //tablaLr1.Columns.Add(term);
                this.cabeceras.Add(term);
                //this.tablaDesplazamientos.agregaColumna(term);
            }
            //tablaLr1.Columns.Add("$");
            this.cabeceras.Add("$");
            //this.tablaDesplazamientos.agregaColumna("$");
            foreach (string nterm in nTerminales)
            {
                //  tablaLr1.Columns.Add(nterm);
                this.cabeceras.Add(nterm);
                // this.tablaDesplazamientos.agregaColumna(nterm);
            }
            //this.llenarTablaLR1();
            this.llenartabla();
        }

        public void llenarTablaLR1()
        {
            List<EdoLR1> listEdos = g.getListaEdos();
            ListViewItem lvAux = null;
            ListViewItem.ListViewSubItem lvSubItem = null;

            //TablaDesplazamientos tabDesp = new TablaDesplazamientos(listEdos);
            //this.tablaDesplazamientos.Estados = listEdos;
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
                            this.tablaDesplazamientos.cambiaValor(e.getId(), cabecera, a.getAccion());
                        }
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
                    }
                }
            }
        }

        void llenartabla()
        {
            int counter = 0;
            string line;
            this.cabeceras = new List<string>();
            int col = 0;
            int numLinea = 0;
            List<EdoLR1> listEdos = g.getListaEdos();
            ListViewItem lvAux = null;
            ListViewItem.ListViewSubItem lvSubItem = null;

            System.IO.StreamReader file =
                new System.IO.StreamReader(@"Gramatica\Tiny_AnalizeTab.txt");
            if ((line = file.ReadLine()) != null)
            {
                string[] array = line.Split('@');
                foreach (string cab in array)
                {
                    tablaLr1.Columns.Add((cab == "" ? "$" : cab));
                    this.cabeceras.Add((cab == "" ? "$" : cab));
                    this.tablaDesplazamientos.agregaColumna((cab == "" ? "$" : cab));
                }
            }
            //this.tablaDesplazamientos.Estados = listEdos;
            while ((line = file.ReadLine()) != null)
            {
                string[] acciones = line.Split('@');
                lvAux = tablaLr1.Items.Add(counter.ToString());
                col = 0;

                foreach (string cabecera in this.cabeceras)
                {
                    this.tablaDesplazamientos.agregaValor(cabecera, " -- ");
                    lvSubItem = lvAux.SubItems.Add(" -- ");
                    lvSubItem.Text = acciones[col];
                    this.tablaDesplazamientos.cambiaValor(numLinea, cabecera, acciones[col]);
                    col++;
                }
                counter++;
                numLinea++;
            }
            file.Close();
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
            List<DslSentence> listaDeSentencias = null;
            List<string> lineasTiny = null;
            AnalizadorLexico analizer = new AnalizadorLexico();

            string codigoTiny = string.Empty;

            // Limpiamos las instancias y datagrids
            this.limpiaInstancias();

            codigoTiny = this.txtCtrlPrograma.Text.Replace("\t", "").Replace(" ", "");
            lineasTiny = codigoTiny.Replace("\r\n", "@").Split('@').ToList();

            if (lineasTiny != null && lineasTiny.Count > 0)
            {
                analizer.tokeniza(lineasTiny);
                listasDeTokens = analizer.TokenDefinitions;
                listaDeSentencias = analizer.SentenceDefinitions;

                //Llenamos la tabla de simbolos
                this.llenaTablaDeSimbolos(listaDeSentencias, listasDeTokens);
                //Mostramos en el grid la tabla de simbolos
                this.muestraTablaDeSimbolos();
                //Llena la tabla de acciones en base a las sentencias
                this.llenarTablaAcciones(lineasTiny);

                if (tablaDeAcciones.esCorrecta)
                {
                    //Llena la tabla de cuadruplos
                    this.generaCuadruplos();
                }
            }
        }


        private void limpiaInstancias()
        {
            Pilas.Stacks.limpiarInstancia();
            TablaSimbolos.TS.TablaMetaSimbolos.Clear();
            TablaErrores.InstanceTable.Errores.Clear();
            Cuadruplos.limpiaInstancia();

            this.limpiaDataGridViews();
        }



        private void limpiaDataGridViews()
        {
            //this.dataGridViewTablaAcciones.Rows.Clear();
            this.dataGridViewTablaAcciones.DataSource = null;
            this.dataGridViewTablaAcciones.Rows.Clear();
            this.dataGridViewTablaAcciones.Refresh();

            //this.dataGridViewTablaSimbolos.Rows.Clear();
            this.dataGridViewTablaSimbolos.DataSource = null;
            this.dataGridViewTablaSimbolos.Rows.Clear();
            this.dataGridViewTablaSimbolos.Refresh();

            //this.dataGridViewCuadruplos.Rows.Clear();
            this.dataGridViewCuadruplos.DataSource = null;
            this.dataGridViewCuadruplos.Rows.Clear();
            this.dataGridViewCuadruplos.Refresh();

            //this.dataGridViewErrores.Rows.Clear();
            this.dataGridViewErrores.DataSource = null;
            this.dataGridViewErrores.Rows.Clear();
            this.dataGridViewErrores.Refresh();
        }


        private void llenaTablaDeSimbolos(List<DslSentence> sentencias, List<List<DslToken>> tokens)
        {
            List<DslSentence> sentenciasDeclarativas = null;
            this.tablaSimbolos = TablaSimbolos.TS;

            if (sentencias != null && sentencias.Count > 0)
            {
                try
                {
                    //Obtenemos todas las lineas de sentencias declarativas que pudieran existir
                    sentenciasDeclarativas = sentencias.Where(sentencia => sentencia.sentenceType == SentenceType.SentenciaDeclara).ToList();

                    if (sentenciasDeclarativas != null && sentenciasDeclarativas.Count > 0)
                    {
                        //Inicializamos la tabla de simbolos
                        sentenciasDeclarativas.ForEach(sentenciaDeclarativa =>
                        {
                            int index = sentencias.IndexOf(sentenciaDeclarativa);
                            if (index >= 0 && index < tokens.Count)
                            {
                                //Obtenemos el token que define el tipo de dato de los simbolos
                                DslToken tipoDato = tokens[index].FirstOrDefault(tokenTipo => tokenTipo.TokenType == TokenType.TipoDato);

                                if (tipoDato != null)
                                {
                                    //Obtenemos los tokens que son definiciones de simbolos
                                    List<DslToken> tokensAux = tokens[index].Where(token =>
                                                                                token.TokenType == TokenType.Id
                                                                                ).ToList();
                                    //Agregamos cada simbolo definido de la línea actual
                                    tokensAux.ForEach(token =>
                                    {
                                        MetaSimbolo ms = new MetaSimbolo(
                                            token.Value, "0", index, sizeof(int),
                                            tipoDato.Value, tipoDato.Value);

                                        if (!this.tablaSimbolos.existeMetaSimbolo(ms))
                                            this.tablaSimbolos.addMetaSimbolo(ms);
                                        else
                                            TablaErrores.InstanceTable.agregaError(
                                                                                    new Error(sentenciaDeclarativa.noLinea,
                                                                                       "Se duplicó la declaración del símbolo: " + token.Value,
                                                                                       "Debe de cambiar el nombre del símbolo.",
                                                                                       "No se pueden definir dos símbolos con el mismo nombre."
                                                                                       ));
                                    });
                                }
                            }
                        });
                    }
                }
                catch (Exception excep)
                {
                    MessageBox.Show("Error al crear la tabla de símbolos.\r\n" + excep.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void muestraTablaDeSimbolos()
        {
            this.dataGridViewTablaSimbolos.AutoGenerateColumns = false;
            this.dataGridViewTablaSimbolos.DataSource = this.tablaSimbolos.metaSimbolos;

            this.dataGridViewTablaSimbolos.Columns[0].DataPropertyName = "id";
            this.dataGridViewTablaSimbolos.Columns[1].DataPropertyName = "tipo";
            this.dataGridViewTablaSimbolos.Columns[2].DataPropertyName = "simbolo";
            this.dataGridViewTablaSimbolos.Columns[3].DataPropertyName = "valor";
        }


        private void llenarTablaAcciones(List<string> lineasTiny)
        {
            List<EdoLR1> estadosAux = g != null ? g.getListaEdos() : null;
            string cadenaDeEntrada = string.Empty;
            string caracter = string.Empty;
            bool desplazo = false;
            bool operaExitosa = false, nuevaLinea = false;



            if (estadosAux != null && estadosAux.Count > 0)
            {
                this.tablaDeAcciones = new TablaDeAcciones(this.tablaDesplazamientos, g, 0);
                lineasTiny.ForEach(linea =>
                {
                    if (linea.Trim() != string.Empty)
                        cadenaDeEntrada += (linea.Trim() + "@");
                    else
                        cadenaDeEntrada += "@";
                });
                cadenaDeEntrada += "$";
                for (int ind = 0; ind < cadenaDeEntrada.Length; ind++)
                {
                    if (cadenaDeEntrada[ind] == '@')
                    {
                        if (nuevaLinea)
                            Pilas.Stacks.incrementeNoLinea();

                        nuevaLinea = true;
                        continue;
                    }
                    if (cadenaDeEntrada[ind] == '\\')
                    {
                        caracter = "\\";
                        continue;
                    }
                    else
                        caracter += cadenaDeEntrada[ind];

                    operaExitosa = this.tablaDeAcciones.agregaCaracter(caracter, cadenaDeEntrada.Substring(ind).Replace("@", ""), out desplazo);

                    if (!operaExitosa)
                    {
                        if (!tablaDeAcciones.esCorrecta)
                            TablaErrores.InstanceTable.agregaError(new Error(Pilas.Stacks.NumeroLinea,
                                                                             "La cadena de entrada no es válida, error de sintáxis.",
                                                                             "Verifique la cadena de entrada cercas de: " + cadenaDeEntrada.Substring(ind).Replace("@", ""),
                                                                             "Error de sintáxis."));
                        break;
                    }

                    ind = !desplazo ? (ind - 1) : ind;

                    if (desplazo && nuevaLinea)
                    {
                        nuevaLinea = false;
                        Pilas.Stacks.incrementeNoLinea();
                    }

                    if (!desplazo && caracter.Contains("\\e"))
                        caracter = caracter.Replace("e", "");
                    else
                        caracter = string.Empty;
                }
                this.muestraTablaAcciones(this.tablaDeAcciones);
            }
        }


        private void muestraTablaAcciones(TablaDeAcciones tablaDeAcciones)
        {
            if (tablaDeAcciones.esCorrecta)
                tablaDeAcciones.Acciones.Add(new Accion(tablaDeAcciones.Acciones.Last(), "$", "ACEPTAR", "ACEPTAR"));

            if (TablaErrores.InstanceTable.isEmpty() == false)
            {
                this.comBoxTipoEjecucion.Enabled = false;
                this.btnEjecutar.Enabled = false;
                this.btnVerArbolAbstracto.Enabled = false;
            }
            else
            {
                this.comBoxTipoEjecucion.Enabled = true;
                this.btnEjecutar.Enabled = true;
                this.btnVerArbolAbstracto.Enabled = true;
            }

            muestraErrores();

            this.dataGridViewTablaAcciones.AutoGenerateColumns = false;
            this.dataGridViewTablaAcciones.DataSource = tablaDeAcciones.Acciones;
            this.dataGridViewTablaAcciones.Columns[0].DataPropertyName = "Acciones";
            this.dataGridViewTablaAcciones.Columns[1].DataPropertyName = "CadenaEntrada";
            this.dataGridViewTablaAcciones.Columns[2].DataPropertyName = "AccionDespOReduc";
            this.dataGridViewTablaAcciones.Columns[3].DataPropertyName = "AccionSemantica";
        }

        private void muestraErrores()
        {
            this.dataGridViewErrores.AutoGenerateColumns = false;
            this.dataGridViewErrores.DataSource = TablaErrores.InstanceTable.Errores;

            this.dataGridViewErrores.Columns[0].DataPropertyName = "Value";
            this.dataGridViewErrores.Columns[1].DataPropertyName = "Solucion";
            this.dataGridViewErrores.Columns[2].DataPropertyName = "NumerodeLinea";
            this.dataGridViewErrores.Columns[3].DataPropertyName = "Descripcion";
        }


        private void generaCuadruplos()
        {
            if (this.tablaDeAcciones.Acciones.Last().AccionDespOReduc == "ACEPTAR" &&
                this.tablaDeAcciones.Acciones.Last().CadenaEntrada == "$")
            {
                Cuadruplos.Instance.recorreArbol(Pilas.Stacks.peekPAA());
                this.muestraCuadruplos();
            }
        }


        private void muestraCuadruplos()
        {
            this.dataGridViewCuadruplos.AutoGenerateColumns = false;
            /*
            this.dataGridViewCuadruplos.DataSource =
                (Cuadruplos.Instance.LstCuadruplos.Where(cua => cua.Operador.Value.Contains("end-") == false).ToList());
            */
            this.dataGridViewCuadruplos.DataSource = Cuadruplos.Instance.LstCuadruplos;
            //this.dataGridViewCuadruplos.DataSource = Cuadruplos.Instance.LstCuadruplos;
            this.dataGridViewCuadruplos.Columns[0].DataPropertyName = "strOperando1";
            this.dataGridViewCuadruplos.Columns[1].DataPropertyName = "strOperando2";
            this.dataGridViewCuadruplos.Columns[2].DataPropertyName = "strOperador";
            this.dataGridViewCuadruplos.Columns[3].DataPropertyName = "strTempVar";
            this.dataGridViewCuadruplos.Columns[4].DataPropertyName = "valueResult";
            this.dataGridViewCuadruplos.Columns[5].DataPropertyName = "Linea";
        }


        public void insertaRegistro(string pila, string cadena)
        {
            //this.lvAux = this.tablaAcciones.Items.Add(pila);
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
            OpenFileDialog fnew = new OpenFileDialog();
            string cadAux = string.Empty;

            fnew.CheckFileExists = true;
            fnew.CheckPathExists = true;
            fnew.Multiselect = false;
            fnew.Filter = "Archivos Tiny (.tiny)|*.tiny|Todos los Archivos (*.*)|*.*";

            if (fnew.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(fnew.FileName))
                {
                    while (sr.Peek() > 0)
                    {
                        cadAux += (sr.ReadLine() + "\r\n");
                    }
                    sr.Close();
                }
                this.fullFileNamePrograma = fnew.FileName;
                this.txtCtrlPrograma.Text = cadAux;
                this.limpiaInstancias();
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
            }
            else
                MessageBox.Show("Antes de solicitar el conjunto primero debe crear o abrir una gramatica");
        }



        private void generaConjuntosPrimeroYSiguiente()
        {
            List<string> conjuntoPrimero;
            List<string> conjuntoSiguiente;
            if (this.g != null)
            {
                conjuntoPrimero = g.getConjuntoPrimero();
                conjuntoSiguiente = g.getConjuntoSiguiente();
            }
        }



        private void editorGramatica_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.formini.Close();
        }



        private void MainWinForm_Load(object sender, EventArgs e)
        {

        }


        private void cargaGramatica()
        {
            if (this.fullFileName != string.Empty && File.Exists(this.fullFileName))
            {
                var fileName = Path.GetFileName(this.fullFileName);
                try
                {
                    using (StreamReader sr = new StreamReader(this.fullFileName))
                    {
                        string texto = string.Empty;
                        while (sr.Peek() >= 0)
                        {
                            texto += (sr.ReadLine() + "\r\n");
                        }
                        this.txtCtrlGramatica.Text = texto;
                    }
                    this.g = new Gramatica(fileName);
                    this.analizaCodigo_Gramatica();
                    this.generaConjuntosPrimeroYSiguiente();
                    this.generaLR1();
                }
                catch (Exception excep)
                {
                    Console.WriteLine(excep.ToString());
                }
            }
        }


        private void generaLR1()
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

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            int selectedIndex = comBoxTipoEjecucion.SelectedIndex;
            Object selectedItem = comBoxTipoEjecucion.SelectedItem;

            if (selectedIndex >= 0)
            {
                this.setValueEnableExecuteButton(false);
                TerminalWinForm terminal = new TerminalWinForm(selectedIndex);
                terminal.Show(this);
            }
        }

        public void setValueEnableExecuteButton(bool value)
        {
            this.btnEjecutar.Enabled = value;
        }

        private void btnVerArbolAbstracto_Click(object sender, EventArgs e)
        {
            if (vAA == null)
            {
                vAA = new VisorArbolAbstracto(this);
                vAA.Show();
            }
            else
            {
                vAA.actualizaArbol();
            }
        }


        public void RefreshTablas()
        {
            this.dataGridViewTablaSimbolos.Refresh();
            this.dataGridViewCuadruplos.Refresh();
        }

        public void setSelectedRowDGVCuadruplos(int row)
        {
            if (row < this.dataGridViewCuadruplos.Rows.Count)
                this.dataGridViewCuadruplos.Rows[row].Selected = true;
        }
    }
}
