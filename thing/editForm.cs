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
    public partial class editForm : Form
    {
        public editForm()
        {
            InitializeComponent();
            textBox1.Text = Form1.edit2;
            textBox2.Text = Form1.edit3;
            textBox3.Text = Form1.edit4;
            textBox4.Text = Form1.edit5;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Form1.edit1 = 1;
            }
            else if (radioButton2.Checked)
            {
                Form1.edit1 = 2;
            }
            else if (radioButton3.Checked)
            {
                Form1.edit1 = 3;
            }
            else
            {
                Form1.edit1 = 0;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Form1.edit2 = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Form1.edit3 = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Form1.edit4 = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Form1.edit5 = textBox4.Text;
        }
    }
}
