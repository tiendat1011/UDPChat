using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ChatClientServerUDP
{
    public partial class Client : Form
    {
        public static string ReturnIP { get; set; }
        public static int ReturnPort { get; set; }
        Socket socket;
        IPEndPoint ep;
        byte[] buffer = new byte[1024];
        public Client()
        {
            InitializeComponent();
        }
        

        private void connectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Connect form2 = new Connect();
            form2.ShowDialog();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //socket.Connect(IPAddress.Parse(ReturnIP), ReturnPort);

            //Thread listen = new Thread(() =>
            //{
            //    try
            //    {
            //        while (true)
            //        {
            //            socket.Receive(buffer);
            //            string msg = Encoding.UTF8.GetString(buffer);
            //            lsInformation.Items.Add(new ListViewItem { Text = tbText.Text });
            //        }
            //    }
            //    catch 
            //    { 
            //        Close(); 
            //    }
            //});

        }
        
        private void disconnectToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Client_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            ep = new IPEndPoint(IPAddress.Parse(ReturnIP), ReturnPort);
            byte[] data = Encoding.UTF8.GetBytes(tbText.Text);
            lsInformation.Items.Add(new ListViewItem { Text = tbText.Text });
            socket.SendTo(data, ep);
            btnSend.Enabled = false;
            socket.Receive(buffer);
            string msg = Encoding.UTF8.GetString(buffer);
            lsInformation.Items.Add(new ListViewItem { Text = msg });
            btnSend.Enabled = true;
        }
    }
}
