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
            textBox5.Text = Form1.edit7;
            textBox6.Text = Form1.edit8;
            textBox7.Text = Form1.edit6;
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            Form1.edit7 = textBox5.Text;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            Form1.edit6 = textBox7.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            Form1.edit8 = textBox6.Text;
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            int test;
            if ((textBox5.Text.Length != 4 || !int.TryParse(textBox5.Text, out test)) && textBox5.Text != string.Empty)
            {
                MessageBox.Show("Keycode must be either a 4 digit number or null.");
                textBox5.Focus();
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            textBox6.UseSystemPasswordChar = false;
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            textBox6.UseSystemPasswordChar = true;
        }
    }
}
