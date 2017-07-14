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
    public partial class importForm : Form
    {
        public importForm()
        {
            InitializeComponent();
        }

        private void importForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will overwrite all previous data with data from the excel file. Are you sure you want to continue?", "Confirm overwrite", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Form1.ioDir = textBox1.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
