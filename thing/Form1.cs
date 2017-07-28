using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace thing
{
    public partial class Form1 : Form
    {
        //selectedRow cRow;
        DateTime currentDate;
        private BindingSource bs;
        private string filterStr = "True";
        private string searchStr = "True";
        public Form1()
        {
            InitializeComponent();
        }

        //stuff for searching, uses the user typed text as input and searches when button is pressed
        private void filterButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0 && comboBox1.Text.Length != 0)
            {
                searchStr = "";
                searchStr += comboBox1.Text;
                searchStr += " LIKE '*";
                searchStr += textBox1.Text;
                searchStr += "*'";
            }
            else
            {
                searchStr = "True";
            }
            bs.Filter = filterStr + " And " + searchStr;
            refreshData();
        }

        //filter results based on which of the check boxes are checked
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterStr = "(Type = 0";
            if (checkedListBox1.GetItemChecked(0))
            {
                filterStr += " Or Type = 1";
            }
            if (checkedListBox1.GetItemChecked(1))
            {
                filterStr += " Or Type = 2";
            }
            if (checkedListBox1.GetItemChecked(2))
            {
                filterStr += " Or Type = 3";
            }
            filterStr += ")";

            bs.Filter = filterStr + " And " + searchStr;
            refreshData();
        }

        public static int edit1;            //this probably would've been better as an arrayyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy
        public static string edit2 = "";
        public static string edit3 = "";
        public static string edit4 = "";
        public static string edit5 = "";
        public static string edit6 = "";
        public static string edit7 = "";
        public static string edit8 = "";

        //edits a row in the database, prompts the user to select the type of device and input names. Default values are the previous names
        //date is automatically set to the current date
        private void editButton_Click(object sender, EventArgs e)
        {
            try
            {
                edit1 = 0;
                currentDate = DateTime.Now;     //spagetti under here
                edit2 = dt.Rows[dt.Rows.IndexOf((dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row as DataRow)].ItemArray.GetValue(2).ToString();
                edit3 = dt.Rows[dt.Rows.IndexOf((dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row as DataRow)].ItemArray.GetValue(3).ToString();
                edit4 = dt.Rows[dt.Rows.IndexOf((dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row as DataRow)].ItemArray.GetValue(4).ToString();
                edit5 = dt.Rows[dt.Rows.IndexOf((dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row as DataRow)].ItemArray.GetValue(9).ToString();
                edit6 = dt.Rows[dt.Rows.IndexOf((dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row as DataRow)].ItemArray.GetValue(6).ToString(); //dept
                edit7 = dt.Rows[dt.Rows.IndexOf((dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row as DataRow)].ItemArray.GetValue(7).ToString(); //key
                edit8 = dt.Rows[dt.Rows.IndexOf((dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row as DataRow)].ItemArray.GetValue(8).ToString(); //pass
                editForm eForm = new editForm();
                if (dataGridView1.CurrentRow.Index == -1)
                {
                    MessageBox.Show("Please make a selection.");
                }
                else
                {
                    if (eForm.ShowDialog() == DialogResult.OK)
                    {
                        DataRow row = dt.Rows[dt.Rows.IndexOf((dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row as DataRow)];
                        if (edit1 != 0)
                        {
                            row[1] = edit1;
                        }
                        row[2] = edit2;
                        row[3] = edit3;
                        row[4] = edit4;
                        row[5] = currentDate.ToString("d");
                        row[9] = edit5;
                        row[6] = edit6;
                        row[7] = edit7;
                        row[8] = edit8;
                        objConnect.UpdateDatabase(dt);
                        refreshData();
                    }
                }
                eForm.Dispose();
            }
            catch(Exception e4)
            {
                MessageBox.Show("Stuff unexpectedly went wrong. If this message appears, contact a system admin.\nError Code:\n" + e4.Message);
            }
        }
        
        public static int add1 = 0;
        public static string add2 = "";
        public static string add3 = "";
        public static string add4 = "";
        public static string add5 = "";
        public static string add6 = "";
        public static string add7 = "";
        public static string add8 = "";

        //adds a row, promts the user to select type and input names and stuff
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                add1 = 0;
                addForm aForm = new addForm();
                if (aForm.ShowDialog() == DialogResult.OK)
                {
                    currentDate = DateTime.Now;
                    DataRow row = dt.NewRow();
                    row[1] = add1;
                    row[2] = add2;
                    row[3] = add3;
                    row[4] = add4;
                    row[5] = currentDate.ToString("d");
                    row[6] = add6;
                    row[7] = add7;
                    row[8] = add8;
                    row[9] = add5;
                    //MessageBox.Show(add1.ToString());
                    dt.Rows.Add(row);
                    objConnect.UpdateDatabase(dt);
                    refreshData();
                }
                add2 = "";
                add3 = "";
                add4 = "";
                add5 = "";
                resetInc = new SqlCommand(resetseed + ((int)(dt.Rows[(dt.Rows.Count - 1)][0])).ToString() + ")", something);
                stuff = resetInc.BeginExecuteNonQuery();
                resetInc.EndExecuteNonQuery(stuff);
                resetInc.Dispose();
            }
            catch (Exception e3)
            {
                MessageBox.Show("Stuff unexpectedly went wrong. If this message appears, contact a system admin.\nError Code:\n" + e3.Message);
            }
        }
        
        //deletes the currently selected row
        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                deleteForm dForm = new deleteForm();
                if (dataGridView1.CurrentRow.Index == -1)
                {
                    MessageBox.Show("Please make a selection.");
                }
                else
                {
                    if (dForm.ShowDialog() == DialogResult.OK)
                    {
                        dt.Rows[dt.Rows.IndexOf((dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row as DataRow)].Delete();
                        objConnect.UpdateDatabase(dt);
                        refreshData();
                    }
                }
                dForm.Dispose();
                if (dt.Rows.Count != 0)
                {
                    resetInc = new SqlCommand(resetseed + ((int)(dt.Rows[(dt.Rows.Count - 1)][0])).ToString() + ")", something);
                    stuff = resetInc.BeginExecuteNonQuery();
                    resetInc.EndExecuteNonQuery(stuff);
                    resetInc.Dispose();
                }
            }
            catch(Exception e2)
            {
                MessageBox.Show("Stuff unexpectedly went wrong. If this message appears, contact a system admin.\nError Code:\n" + e2.Message);
            }
        }

        //help button, this thing is pretty useless
        private void helpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This UI displays employee device data.\nColumns ID and Date are automatically set. ID, Date, and Type cannot be Null.\nTo add a new user, click add.\nTo edit an existing user, first select the user from the bottom table, then click edit.\nTo delete a user, select the user from the bottom table, then click delete.\nUse the check boxes in the middle to filter by device.\nAlso if the printing thing looks ugly, try manually adjusting the column widths.","Info");
        }


        //initialize connection stuff
        dbConnection objConnect;
        string conString;
        //DataSet ds;
        //DataRow dRow;
        DataTable dt;
        public static string dbDir;
        string resetseed = "DBCC CHECKIDENT ([Table], RESEED, ";
        SqlCommand resetInc;
        SqlConnection something;
        IAsyncResult stuff;
        public static bool createnew = false;
        public static string newName;

        private void Form1_Load(object sender, EventArgs e)
        {
            bool blah = true;   //Loops until database is found or user gives up out of frustration
            while (blah)
            {
                finddbForm dForm = new finddbForm();    //dialogue for finding the directory
                if (dForm.ShowDialog() == DialogResult.Cancel)
                {
                    Environment.Exit(1);
                }
                if (createnew)
                {
                    selectName sForm = new selectName();
                    sForm.ShowDialog();
                    something = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Integrated security=SSPI;database=master");
                    string cCommand = "CREATE DATABASE " + newName + " ON PRIMARY " +
                                      "(NAME = " + newName + "_Data, " +
                                      "FILENAME = '" + dbDir + ".mdf', " +
                                      "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
                                      "LOG ON (NAME = " + newName + "_log, " +
                                      "FILENAME = '" + dbDir + "_log.ldf', " +
                                      "SIZE = 1MB, " +
                                      "MAXSIZE = 5MB, " +
                                      "FILEGROWTH = 10%)";
                    SqlCommand myCommand = new SqlCommand(cCommand, something);
                    try
                    {
                        something.Open();
                        myCommand.ExecuteNonQuery();
                        something.Close();
                        something = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbDir + ".mdf;Integrated Security=True;Connect Timeout=30");
                        something.Open();
                        SqlCommand createTable = new SqlCommand("CREATE TABLE [dbo].[Table] (" +
                                                                "[Id]     INT           IDENTITY(1, 1) NOT NULL,"+
                                                                "[Type]   INT           NOT NULL," +
                                                                "[Device] NVARCHAR(50) NULL," +
                                                                "[First]  NVARCHAR(50) NULL," +
                                                                "[Last]   NVARCHAR(50) NULL," +
                                                                "[Date]   DATE          NULL," +
                                                                "[Department] NVARCHAR(50) NULL," +
                                                                "[Keycard] NVARCHAR(50) NULL," + 
                                                                "[Password] NVARCHAR(50) NULL," +
                                                                "[Notes]  NVARCHAR(50) NULL," +
                                                                "PRIMARY KEY CLUSTERED([Id] ASC)" +
                                                                "); ", something);
                        createTable.ExecuteNonQuery();
                        currentDate = DateTime.Now;
                        SqlCommand defaultRow = new SqlCommand("INSERT INTO [Table] (Type, Device, First, Last, Date, Department, Keycard, Password, Notes) VALUES (1, 'Default', 'First', 'Last', '" + currentDate.ToString("d") + "', 'Default', '0000', '123', 'Notes')", something);
                        defaultRow.ExecuteNonQuery();
                        MessageBox.Show("DataBase has been Created Successfully", ":)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        something.Close();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), ":(", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    dbDir = dbDir + ".mdf";
                }
                dForm.Dispose();
                
                try
                {
                    objConnect = new dbConnection();
                    //conString = Properties.Settings.Default.Database1ConnectionString;
                    conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=";    //alien language
                    conString += dbDir;
                    conString += ";Integrated Security=True;Connect Timeout=30";
                    objConnect.connection_string = conString;
                    objConnect.Sql = Properties.Settings.Default.SQL;

                    dt = objConnect.getDataTable;
                    bs = new BindingSource();
                    bs.DataSource = dt;
                    this.dataGridView1.DataSource = bs;
                    checkedListBox1.SetItemChecked(0, true);    //sets default checks to all
                    checkedListBox1.SetItemChecked(1, true);
                    checkedListBox1.SetItemChecked(2, true);
                    dataGridView1.Columns[0].Visible = false;        //makes the thing look nice
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[2].Width = 150;
                    dataGridView1.Columns[3].Width = 150;
                    dataGridView1.Columns[4].Width = 150;
                    dataGridView1.Columns[5].Width = 150;
                    dataGridView1.Columns[9].Width = 500;
                    something = new SqlConnection(conString);
                    something.Open();
                    resetInc = new SqlCommand(resetseed + ((int)(dt.Rows[(dt.Rows.Count - 1)][0])).ToString() + ")", something);
                    stuff = resetInc.BeginExecuteNonQuery();
                    resetInc.EndExecuteNonQuery(stuff);
                    resetInc.Dispose();
                    blah = false;
                }
                catch (Exception e1)
                {
                    MessageBox.Show("Error opening database.\n" + e1.Message);
                }
            }
            //MessageBox.Show(dt.Rows.Count.ToString());
            //ds = objConnect.GetConnection;
            //cRow = new selectedRow();
            //this.tableTableAdapter2.Fill(dt);
        }

        //refreshes the datatable after each edit add or delete
        private void refreshData()
        {
            try
            {
                dt = objConnect.getDataTable;
                /*string test1 = resetseed;
                string test2 = (dt.Rows[(dt.Rows.Count - 1)][0]).ToString();
                string test = resetseed + (dt.Rows[(dt.Rows.Count - 1)][0]).ToString() + ")";*/
                bs.DataSource = dt;
                this.dataGridView1.DataSource = bs;
                dataGridView1.EndEdit();
            }
            catch(Exception e5)
            {
                MessageBox.Show("Something went wrong when refreshing the data. \n" + e5.Message);
            }
        }


        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //cRow.setRow(e.RowIndex);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        //clears the search
        private void clsButton_Click(object sender, EventArgs e)
        {
            searchStr = "True";
            bs.Filter = filterStr;
            refreshData();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        //Allows the user to click enter instead of manually clicking search
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Length != 0 && comboBox1.Text.Length != 0)
                {
                    searchStr = "";
                    searchStr += comboBox1.Text;
                    searchStr += " LIKE '*";
                    searchStr += textBox1.Text;
                    searchStr += "*'";
                }
                else
                {
                    searchStr = "True";
                }
                bs.Filter = filterStr + " And " + searchStr;
                refreshData();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            //MessageBox.Show(dt.Rows.IndexOf((dt.Select("ID = " + bs.Position.ToString()))[0]).ToString());
            int stuff;
            stuff = dt.Rows.IndexOf((bs.).Rows[dataGridView1.CurrentRow.Index]);
            MessageBox.Show((stuff).ToString());
            */
            //var drv = dt.Rows.IndexOf((dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row as DataRow);
        }

        //prints using some code found on internet
        private void printButton_Click(object sender, EventArgs e)
        {
            currentDate = DateTime.Now;
            ClsPrint ptable = new ClsPrint(dataGridView1, "Employee Device Data " + currentDate.ToString("d"));
            ptable.PrintForm();
        }

        public class InvalidDirectory: Exception
        {
            public InvalidDirectory(string message): base(message)
            { 
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                try
                {
                    deleteForm dForm = new deleteForm();
                    if (dataGridView1.CurrentRow.Index == -1)
                    {
                        MessageBox.Show("Please make a selection.");
                    }
                    else
                    {
                        if (dForm.ShowDialog() == DialogResult.OK)
                        {
                            dt.Rows[dt.Rows.IndexOf((dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row as DataRow)].Delete();
                            objConnect.UpdateDatabase(dt);
                            refreshData();
                        }
                    }
                    dForm.Dispose();
                }
                catch (Exception e2)
                {
                    MessageBox.Show("Stuff unexpectedly went wrong. If this message appears, contact a system admin.\nError Code:\n" + e2.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public static string ioDir;
        private void button3_Click(object sender, EventArgs e)
        {
            /*dataGridView1.SelectAll();
            DataObject datob = dataGridView1.GetClipboardContent();
            Clipboard.SetDataObject(datob, true);
            MessageBox.Show("Data copied to clipboard.");*/
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveDialog.FilterIndex = 2;
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                progressBar1.Visible = true;
                ioDir = saveDialog.FileName;
                backgroundWorker1.RunWorkerAsync();
            }
            saveDialog.Dispose();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Excel._Application ExApp = new Excel.Application();
                Excel._Workbook workbook = ExApp.Workbooks.Add(Type.Missing);
                Excel._Worksheet worksheet = null;
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Exports";
                int cRowInd = 1;
                int cColInd = 1;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[cRowInd, cColInd] = dataGridView1.Columns[j].HeaderText;
                    cColInd++;
                }
                cRowInd++;
                cColInd = 1;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[cRowInd, cColInd] = dataGridView1.Rows[i].Cells[j].Value;
                        cColInd++;
                    }
                    backgroundWorker1.ReportProgress(i * 100 / (dataGridView1.Rows.Count - 1));
                    cColInd = 1;
                    cRowInd++;
                }
                workbook.SaveAs(ioDir);
                backgroundWorker1.ReportProgress(100);
                MessageBox.Show("View exported successfully");
                ExApp.Quit();
                workbook = null;
                ExApp = null;
                backgroundWorker1.ReportProgress(0);
            }
            catch (Exception e6)
            {
                MessageBox.Show("An unexpected error occured.\n" + e6);
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ioDir = openFileDialog1.FileName;
                if (File.Exists(ioDir))
                {
                    progressBar1.Value = 0;
                    progressBar1.Visible = true;
                    backgroundWorker2.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("File does not exist. Please try again.");
                }
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Excel.Application ExApp;
                Excel.Workbook ExWb;
                Excel.Worksheet ExSht;
                DataRow drow;
                int rIndex = 2;
                ExApp = new Excel.Application();
                ExWb = ExApp.Workbooks.Open(ioDir);
                ExSht = ExWb.ActiveSheet;
                Excel.Range last = ExSht.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                while (ExSht.Cells[rIndex, 1].value != null)
                {
                    drow = dt.NewRow();
                    drow[1] = ExSht.Cells[rIndex, 2].value;
                    drow[2] = ExSht.Cells[rIndex, 3].value;
                    drow[3] = ExSht.Cells[rIndex, 4].value;
                    drow[4] = ExSht.Cells[rIndex, 5].value;
                    drow[5] = ExSht.Cells[rIndex, 6].value;
                    drow[6] = ExSht.Cells[rIndex, 7].value;
                    drow[7] = ExSht.Cells[rIndex, 8].value;
                    drow[8] = ExSht.Cells[rIndex, 9].value;
                    drow[9] = ExSht.Cells[rIndex, 10].value;
                    dt.Rows.Add(drow);
                    backgroundWorker2.ReportProgress(rIndex * 100 / (last.Row - 1));
                    rIndex++;
                }
                ExApp.Quit();
                ExWb = null;
                ExApp = null;
                backgroundWorker1.ReportProgress(0);
            }
            catch(Exception e2)
            {
                MessageBox.Show("Error exporting.\n" + e2.Message);
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            objConnect.UpdateDatabase(dt);
            refreshData();
            MessageBox.Show("Import successful.");
            progressBar1.Visible = false;
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        
        public static DataRow somerow;
        private void details_Click(object sender, EventArgs e)
        {
            try
            {
                somerow = dt.Rows[dt.Rows.IndexOf((dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row as DataRow)];
                printindiv dinfo = new printindiv();
                dinfo.ShowDialog();
            }
            catch(Exception e1)
            {
                MessageBox.Show("Either an error occured or no row has been selected.\n" + e1.Message);
            }
        }

        private void btnPrintForm_Click(object sender, EventArgs e)
        {
            
        }
    }
}
