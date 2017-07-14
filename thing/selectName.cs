using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thing
{
    public partial class selectName : Form
    {
        public selectName()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.newName = textBox1.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
