using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compi
{
    public partial class PrimeroYSiguienteModal : Form
    {
        public PrimeroYSiguienteModal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metodo que es mandado llamar por el padre para mandar imprimir un conjunto o ambos
        /// </summary>
        /// <param name="primero"></param>
        /// <param name="siguiente"></param>
        public void muestraConjunto(List<string> primero = null, List<string> siguiente = null)
        {
            if (primero != null)
                this.muestraConjuntoPrimero(primero);
            else
            {
                this.listViewPrimero.Visible = false;
                this.tabla.Visible = false;
            }
            if (siguiente != null)
                this.muestraConjuntoSiguiente(siguiente);
            else
            {
                this.listView2.Visible = false;
                this.label1.Visible = false;
            }
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

        /// <summary>
        /// Metodo que imprime en la modal el conjunto primero o el conjunto siguiente
        /// solo pinta un conjunto
        /// Pendiente quitar la referencia mejor que cree dinamicamente el control de la tabla
        /// </summary>
        /// <param name="c"></param>
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
