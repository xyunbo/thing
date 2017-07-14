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
    public partial class addForm : Form
    {
        public addForm()
        {
            InitializeComponent();
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Form1.add1 = 1;
            }
            else if (radioButton2.Checked)
            {
                Form1.add1 = 2;
            }
            else if (radioButton3.Checked)
            {
                Form1.add1 = 3;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Form1.add2 = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Form1.add3 = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Form1.add4 = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Form1.add5 = textBox4.Text;
        }
    }
}
