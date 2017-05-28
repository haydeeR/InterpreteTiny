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

        private List<String> cabeceras = null;
        
        public editorGramatica()
        {
            InitializeComponent();
        }

        public void muestraProducciones(List<string> p = null)
        {
            ListViewItem aux;
            List<string> listProd = p;
            if (listProd == null)
            {
                listProd = this.g.getProducciones();
            }
            this.listView1.Items.Clear();
            if (listProd != null)
            {
                foreach (string prod in listProd)
                {
                    aux = new ListViewItem(prod);
                    this.listView1.Items.Add(aux);
                }
            }
        }
        /**
         * Método que crea una gramatica y la guarda en un archivo con el nombre especificado y una extencion de .gramm
         */
        private void btnNuevaGramatica_Click(object sender, EventArgs e)
        {
            NuevaGramatica newG = new NuevaGramatica();
            if (newG.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((newG.getNombre().Trim()) != "")
                    {
                        Stream stream = new FileStream(newG.getNombre()+".gramm", FileMode.Create, FileAccess.Write, FileShare.None);
                        g = new Gramatica(newG.getNombre());
                    }
                    else
                        MessageBox.Show("No se puede crear una gramatica sin nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch(Exception ex) 
                {
                    MessageBox.Show("Error: No se puede leer el archivo del disco, Error Original:" + ex.Message);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /**
         * Método que crea una produccion nueva y la va agregando a la gramatica 
         */
        private void btnNuevaProduccion_Click(object sender, EventArgs e)
        {
            NuevaProduccionDlg newP;
            if (this.g != null)
            {
                newP = new NuevaProduccionDlg(g,this);
                newP.Show();
                
            }
            else
                MessageBox.Show("Antes de crear una produccion debe crear o abrir una gramatica");
        }

        /** Método que abre una gramatica de un archivo 
         * en seguida de que abre la gramatica muestra las producciones en el listado de producciones
         */
        private void btnAbrirGramatica_Click(object sender, EventArgs e)
        {
            OpenFileDialog fnew = new OpenFileDialog();
            Stream file = null;
            fnew.Filter = "gramm files (*.gramm)| *.gramm | All files (*.*)|*.*";

            if (fnew.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((file = fnew.OpenFile()) != null)
                    {
                        IFormatter formatter = new BinaryFormatter();
                        Stream stream = new FileStream(fnew.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                        this.g = (Gramatica)formatter.Deserialize(stream);
                        stream.Close();
                        muestraProducciones();
                    }
                }
                catch(Exception ex) 
                {
                    MessageBox.Show("Error: No se puede leer el archivo del disco, Error Original:" + ex.Message);
                }
            }
        }

        /**
         * Metodo que guarda una produccion y va serializando la gramatica completa
         * con extension .gramm
         */
        private void btnGuarda_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveF = new SaveFileDialog();
            if (g != null)
            {
                saveF.DefaultExt = "*.gramm";
                saveF.Filter = "gramm files|*.gramm";
                if (saveF.ShowDialog() == DialogResult.OK && saveF.FileName.Length > 0)
                {

                    g.setNombre(saveF.FileName);
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(saveF.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                    formatter.Serialize(stream, g);
                    stream.Close();
                }
            }
            else 
            {
                MessageBox.Show("Para Guardar una gramatica primero debe abrir o crear una");
            }
        }

        //Borrar una produccion
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        public void reducePila(int ind, List<string> pila)
        {
            for (int i = pila.Count()-1; i >= ind; i-- )
                pila.Remove(pila[i]);
        }

        private void btnFirstAndNext_Click(object sender, EventArgs e)
        {
            PrimeroYSiguiente firstAndNext;
            List<string> conjuntoPrimero;
            if (this.g != null)
            {
                firstAndNext = new PrimeroYSiguiente(this.g.getTokensXProd(), this.g.getNTerminal(), this.g.getTerminales());
                conjuntoPrimero = firstAndNext.getConjuntoPrimero();
                this.muestraConjuntoPrimero(conjuntoPrimero);
            }
            else
                MessageBox.Show("Antes de solicitar el conjunto primero debe crear o abrir una gramatica");
        }

        public void muestraConjuntoPrimero(List<string> c = null)
        {
            ListViewItem aux;
            List<string> listConjunto = c;
            this.listViewPrimero.Items.Clear();
            if (listConjunto != null)
            {
                foreach (string primero in listConjunto)
                {
                    aux = new ListViewItem(primero);
                    this.listViewPrimero.Items.Add(aux);
                }
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            PrimeroYSiguiente firstAndNext;
            List<string> conjuntoSiguiente;
            if (this.g != null)
            {
                firstAndNext = new PrimeroYSiguiente(this.g.getTokensXProd(), this.g.getNTerminal(), this.g.getTerminales());
                conjuntoSiguiente = firstAndNext.getConjuntoSiguiente();
                this.muestraConjuntoSiguiente(conjuntoSiguiente);
            }
            else
                MessageBox.Show("Antes de solicitar el conjunto siguinte debe crear o abrir una gramatica");
        }

        public void muestraConjuntoSiguiente(List<string> c = null)
        {
            ListViewItem aux;
            List<string> listConjunto = c;
            this.listView2.Items.Clear();
            if (listConjunto != null)
            {
                foreach (string siguiente in listConjunto)
                {
                    aux = new ListViewItem(siguiente);
                    this.listView2.Items.Add(aux);
                }
            }
        }
    }
}
