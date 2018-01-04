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
    public partial class ReadDatoDlg : Form
    {
        public int ReadValue
        {
            get
            {
                return int.Parse(this.numValue.Value.ToString());
            }
        }

        public ReadDatoDlg()
        {
            InitializeComponent();
        }

    }
}
