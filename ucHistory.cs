using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CopyTool
{
    public partial class ucHistory : MetroFramework.Controls.MetroUserControl
    {




        public ucHistory()
        {
            InitializeComponent();
            UpdateGrid();

        }
     
        public void UpdateGrid()
        {
            try
            {
                logTTableAdapter.FillBy(logds.LogT);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void ucHistory_Load(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private List<string> items;

        private void timer1_Tick(object sender, EventArgs e)
        {
      
            
            string s = "Select * From LogT;";

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" + Environment.CurrentDirectory + @"\Log.mdf""; Integrated Security = True; Connect Timeout = 30");
            con.Open();
            SqlCommand cmd = new SqlCommand(s, con);

            List<string> ds = new List<string>();
             SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ds.Add(reader["TimeT"].ToString());
                       
            }

            reader.Close();
            con.Close();
            try
            {

                if (items.Count != ds.Count || items == null)
                {
                    UpdateGrid();
                }
                items = ds;
            }
            catch (Exception)
            {


                items = ds;
            }

        }



        private void metroGrid1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            foreach (DataGridViewRow row in metroGrid1.SelectedRows)
            {
                string value1 = row.Cells[0].Value.ToString();
                string value2 = row.Cells[1].Value.ToString();
                //...
            }

        }
    }
}
