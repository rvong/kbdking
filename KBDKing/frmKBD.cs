using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KBDKing
{
    public partial class frmKBD : Form
    {
        KBDHook kbdHook = new KBDHook();

        public frmKBD()
        {
            InitializeComponent();
        }

        private void frmKBD_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
