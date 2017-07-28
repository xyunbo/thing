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
    public partial class printindiv : Form
    {
        public printindiv()
        {
            InitializeComponent();
            textBox1.Text = "First Name:            \n" + Form1.somerow[3].ToString();
            textBox2.Text = "Last Name:             \n" + Form1.somerow[4].ToString();
            textBox3.Text = "Date Modified:     \n" + Form1.somerow[5].ToString();
            textBox4.Text = "Device Type:       \n";
            textBox5.Text = "Device Name:       \n" + Form1.somerow[2].ToString();
            textBox6.Text = "Department:        \n" + Form1.somerow[6].ToString();
            textBox7.Text = "Keycard Number:            \n" + Form1.somerow[7].ToString();
            textBox8.Text = "Password:                  \n" + Form1.somerow[8].ToString();
            textBox9.Text = "Notes:                     \n" + Form1.somerow[9].ToString();
            int var = (int)Form1.somerow[1];
            if(var == 1)
            {
                textBox4.Text += "Desktop";
            }
            if(var == 2)
            {
                textBox4.Text += "Monitor";
            }
            if(var == 3)
            {
                textBox4.Text += "Laptop";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void printindiv_Load(object sender, EventArgs e)
        {

        }
    }
}
