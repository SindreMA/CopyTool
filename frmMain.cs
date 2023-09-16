using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace CopyTool
{
    public partial class frmMain : MetroFramework.Forms.MetroForm
    {
        public static bool Active;
        static frmMain _instance;
        public static frmMain Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new frmMain();
                return _instance;
            }
        }
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWnd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
        private IntPtr _ClipboardViewerNext;
        private const int WM_DRAWCLIPBOARD = 0x0308;
        private void StartClipboardViewer()
        {
            _ClipboardViewerNext = SetClipboardViewer(this.Handle);
        }
        private void StopClipboardViewer()
        {
            ChangeClipboardChain(this.Handle, _ClipboardViewerNext);
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_DRAWCLIPBOARD)
            {
                if (Active)
                {


                    string NewCopy = Clipboard.GetText();
                    if (NewCopy != Oldcopy)
                    {
                        Oldcopy = NewCopy;
                        if (NewCopy != null && NewCopy != "" && NewCopy != " ")
                        {
                            string Time = DateTime.Now.ToLongTimeString().Replace(".", ":") + " " + DateTime.Now.ToShortDateString();
                            string s = "INSERT INTO LogT (Text, TimeT) VALUES('" + NewCopy.Replace("\n", "") + "', '" + Time + "');";

                            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" + Environment.CurrentDirectory + @"\Log.mdf""; Integrated Security = True; Connect Timeout = 30");
                            SqlCommand cmd = new SqlCommand(s, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            //File.AppendAllText(LogFile, Environment.NewLine + NewCopy);

                        }

                    }
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        public string Oldcopy;
        public static string LogFile;
        public static string ServiceMode;
        public MetroFramework.Controls.MetroPanel MetroContainer
        {
            get { return metroPanel1; }
            set { metroPanel1 = value; }
        }
        public MetroFramework.Controls.MetroLink MetroHistory
        {
            get { return metroLink1; }
            set { metroLink1 = value; }
        }
        public frmMain()
        {
            //if (ConfigurationManager.AppSettings["FilePath"] == "")
            //{
            //    ConfigurationManager.AppSettings["FilePath"] = Environment.CurrentDirectory + "\\Clipboard.Log";
            //}
            //LogFile = ConfigurationManager.AppSettings["FilePath"];
            //ServiceMode = ConfigurationManager.AppSettings["SetServiceMode"];
            StartClipboardViewer();
            Active = true;
            InitializeComponent();
            Showing = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _instance = this;
            ucHistory uc = new ucHistory();
            uc.Dock = DockStyle.Fill;
            metroPanel1.Controls.Add(uc);

        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            metroPanel1.Controls["ucHistory"].BringToFront();
        }

        private void metroLink2_Click(object sender, EventArgs e)
        {
            _instance = this;
            Settings uc = new Settings();
            uc.Dock = DockStyle.Fill;
            metroPanel1.Controls.Add(uc);
            metroPanel1.Controls["Settings"].BringToFront();
        }

        
        public void timer1_Tick(object sender, EventArgs e)
        {

           
        }
        private bool Showing;
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            if (Showing)
            {
                this.Hide();
                Showing = false;
            }
            else if (!Showing)
            {
                this.Show();
                Showing = true;
            }
        }

        private void frmMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Hide();
        }

        private void metroLink3_Click(object sender, EventArgs e)
        {
            _instance = this;
            hotkeys uc = new hotkeys();
            uc.Dock = DockStyle.Fill;
            metroPanel1.Controls.Add(uc);
            metroPanel1.Controls["hotkeys"].BringToFront();
        }
    }
}
