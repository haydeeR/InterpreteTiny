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
    public partial class TerminalWinForm : Form
    {
        int typeExecution;

        public TerminalWinForm()
        {
            InitializeComponent();
        }


        public TerminalWinForm(int tipoEjecucion)
        {
            this.typeExecution = tipoEjecucion;
            InitializeComponent();
            this.limpiaControles();
            this.toolTipPrompt.SetToolTip(this.txtOutputTinyProgram, "$");
        }


        public void limpiaControles()
        {
            this.txtLineaEjecucion.Text = string.Empty;
            this.txtOutputTinyProgram.Text = string.Empty;

            this.txtNumLinea.Text = "0";
            this.txtNumCuadruplo.Text = "0";

            switch (this.typeExecution)
            {
                case 0:
                    this.btnEjecutaSiguiente.Text = "Ejecuta Programa";
                    this.btnEjecutaSiguiente.Image = Properties.Resources.right_arrow;
                    //this.btnEjecutaSiguiente.Enabled = false;
                    break;
                case 1:
                    this.btnEjecutaSiguiente.Text = "Ejecuta Sig. Línea";
                    this.btnEjecutaSiguiente.Image = Properties.Resources.play_next_button;
                    break;
                case 2:
                    this.btnEjecutaSiguiente.Text = "Ejecuta Sig. Cuádruplo";
                    this.btnEjecutaSiguiente.Image = Properties.Resources.play_next_button;
                    break;
            }
        }

        private void btnEjecutaSiguiente_Click(object sender, EventArgs e)
        {
            Cuadruplos.Instance.Terminal = this;
            Cuadruplos.Instance.ejecuta(this.typeExecution);
            
        }


        public void print(string stringToWrite)
        {
            this.txtOutputTinyProgram.Text += ("\r\n" + stringToWrite.Replace("\"", ""));
        }



        private void TerminalWinForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            (this.Owner as MainWinForm).setValueEnableExecuteButton(true);
        }



        public void refreshTablaSimbolos()
        {
            (this.Owner as MainWinForm).RefreshTablaSimbolos();
        }
    }
}
