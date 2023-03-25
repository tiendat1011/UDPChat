using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ChatClientServerUDP
{
    public partial class Connect : Form
    {
        public Connect()
        {
            InitializeComponent();
        }
        
        private void btnConnect_Click(object sender, EventArgs e)
        {
            Client.ReturnIP = tbIP.Text;
            Client.ReturnPort = Int32.Parse(tbPort.Text);
            MessageBox.Show("Connect succesfully", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void tbIP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
