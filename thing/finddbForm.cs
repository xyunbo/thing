using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace thing
{
    public partial class finddbForm : Form
    {
        private string newdbDir;
        private string olddbDir;
        public finddbForm()
        {
            InitializeComponent();
            openFileDialog1.Filter = "MDF files(*.mdf) | *.mdf";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                olddbDir = openFileDialog1.FileName;
                textBox1.Text = olddbDir;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.dbDir = olddbDir;
            this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Form1.dbDir = textBox1.Text;
        }

        private void finddbForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                newdbDir = folderBrowserDialog1.SelectedPath;
                textBox2.Text = newdbDir + "\\NewDatabase";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            newdbDir = textBox2.Text;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(newdbDir))
            {
                MessageBox.Show("File already exists, please choose a different name.");
            }
            else {
                Form1.dbDir = newdbDir;
                Form1.createnew = true;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
