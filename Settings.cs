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
    public partial class Settings : MetroFramework.Controls.MetroUserControl
    {
        public Settings()
        {
            InitializeComponent();
         
          
        }




        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (!metroToggle1.Checked)
            {
                frmMain.Active = false;
            }
            else if (metroToggle1.Checked)
            {
                frmMain.Active = true;
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string s = "DELETE FROM LOGT;";

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" + Environment.CurrentDirectory + @"\Log.mdf""; Integrated Security = True; Connect Timeout = 30");
            SqlCommand cmd = new SqlCommand(s, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
