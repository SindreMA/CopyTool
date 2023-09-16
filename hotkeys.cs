using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CopyTool
{
    public partial class hotkeys : UserControl
    {
        Keyvbordhook.KeyboardHook hook = new Keyvbordhook.KeyboardHook();
        public hotkeys()
        {
            InitializeComponent();
            metroComboBox1.Text = "2";
            metroComboBox2.Text = "3";
            metroComboBox3.Text = "4";
            metroComboBox4.Text = "5";

      //      hook.KeyPressed +=
      //new EventHandler<Keyvbordhook.KeyPressedEventArgs>(hook_KeyPressed);
      //      // register the control + alt + F12 combination as hot key.
      //      hook.RegisterHotKey(Keyvbordhook.ModifierKeys.Control & Keyvbordhook.ModifierKeys.Shift, Keys.Z);
        }
        
        void hook_KeyPressed(object sender, Keyvbordhook.KeyPressedEventArgs e)
        {
            if (e.Key == Keys.Q )
            {

            }
            
        }

        private void hotkeys_Load(object sender, EventArgs e)
        {
       
        }
    }
}
