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
    public partial class VisorArbolAbstracto : Form
    {
        MainWinForm parentWindow;

        public VisorArbolAbstracto()
        {
            InitializeComponent();
        }


        public VisorArbolAbstracto(MainWinForm parentWindowInstance)
        {
            InitializeComponent();
            this.parentWindow = parentWindowInstance;
            creaNodoRaiz();
        }


        public void actualizaArbol()
        {
            this.ctrlTreeViewer.Nodes.Clear();
            this.ctrlTreeViewer.Refresh();
            this.creaNodoRaiz();
        }


        private void creaNodoRaiz()
        {
            NodoArblAS raiz = Pilas.Stacks.peekPAA();
            string textToshow;

            if (raiz != null)
            {
                textToshow = raiz.getToken().Value == "end-if" ? "if" : raiz.getToken().Value;
                TreeNode tNodeRaiz = new TreeNode("Raiz => " + textToshow);
                this.ctrlTreeViewer.Nodes.Add(tNodeRaiz);

                agregaNodoAlArbol(raiz, tNodeRaiz);
            }
        }


        private void agregaNodoAlArbol(NodoArblAS logNodo, TreeNode ctrlNodoParent)
        {
            NodoArblAS logNodoDerecho, logNodoIzquierdo;
            TreeNode ctrlNodoIzq, ctrlNodoDer;
            string textToshow;

            logNodoIzquierdo = logNodo.getNodoIzquierdo();
            logNodoDerecho = logNodo.getNodoDerecho();

            if (logNodoIzquierdo != null && ctrlNodoParent != null)
            {
                textToshow = logNodoIzquierdo.getToken().Value == "end-if" ? "if" : logNodoIzquierdo.getToken().Value;
                ctrlNodoIzq = new TreeNode("Nodo Izquierdo => " + textToshow);
                ctrlNodoParent.Nodes.Add(ctrlNodoIzq);
                agregaNodoAlArbol(logNodoIzquierdo, ctrlNodoIzq);
            }

            if (logNodoDerecho != null && ctrlNodoParent != null)
            {
                textToshow = logNodoDerecho.getToken().Value == "end-if" ? "if" : logNodoDerecho.getToken().Value;
                ctrlNodoDer = new TreeNode("Nodo Derecho => " + textToshow);
                ctrlNodoParent.Nodes.Add(ctrlNodoDer);
                agregaNodoAlArbol(logNodoDerecho, ctrlNodoDer);
            }
        }

        private void VisorArbolAbstracto_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.parentWindow.vAA = null;
        }
    }
}
