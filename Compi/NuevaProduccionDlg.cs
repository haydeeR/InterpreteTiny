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
    public partial class NuevaProduccionDlg : Form
    {
        Gramatica g = null;
        editorGramatica p = null;

        public NuevaProduccionDlg(Gramatica gram, editorGramatica padre)
        {
            InitializeComponent();
            this.g = gram;
            this.p = padre;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarProduccion_Click(object sender, EventArgs e)
        {
            if (!this.g.evaluaProduccion(this.textBox1.Text))
            {
                MessageBox.Show("Hay un error en la producción", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.textBox1.Text = "";
                p.muestraProducciones();
            }
        }
    }
}
