using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace thing
{
    class dbConnection
    {
        private string sql_string;
        private string strCon;
        SqlDataAdapter da;
        DataTable dt;
        SqlConnection conn;
        //DataSet ds = new DataSet();

        public string Sql
        {
            set { sql_string = value; }
        }

        public string connection_string
        {
            set { strCon = value; }
        }

        public DataTable getDataTable
        {
            get
            { return MyDataTable(); }
        }

        public void UpdateDatabase(DataTable dt1)
        {
            conn.Open();
            SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
            da.DeleteCommand = cmdb.GetDeleteCommand(true);
            da.UpdateCommand = cmdb.GetUpdateCommand(true);
            da.InsertCommand = cmdb.GetInsertCommand(true);
            da.AcceptChangesDuringUpdate = true;
            
            da.Update(dt1);
            conn.Close();
        }

        private DataTable MyDataTable()
        {
            //System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(strCon);

            //con.Open();

            //da1 = new System.Data.SqlClient.SqlDataAdapter(sql_string, con);

            //System.Data.DataSet dat_set = new System.Data.DataSet();

            //da1.Fill(dat_set, "data_1");
            try
            {
                conn = new SqlConnection(strCon);
                da = new SqlDataAdapter(sql_string, conn);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql_string;
                da.SelectCommand = cmd;
                dt = new DataTable();
                //da.Fill(ds, "[Table]");

                //DataSet ds = new DataSet();

                conn.Open();
                //da.Fill(ds);
                da.Fill(dt);
                conn.Close();

                return dt;
                // return ds;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Selected directory does not contain a valid data table.\n" + e.Message);
                return null;
            }

        }
        public class InvalidDatabaseTable: Exception
        {
            public InvalidDatabaseTable(string message) : base(message)
            { }
        }
    }
}
