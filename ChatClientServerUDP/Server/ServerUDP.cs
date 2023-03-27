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
    public partial class ServerUDP : Form
    {
        Socket socket;
        IPEndPoint IPep;
        const int bufferSize = 1024;
        State state = new State();
        AsyncCallback recv = null;
        EndPoint epF = new IPEndPoint(IPAddress.Any, 0);
        public class State
        {
            public byte[] buffer = new byte[bufferSize];
        }
        void Create()
        {
            btnSend.Enabled = true;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint IPep = new IPEndPoint(IPAddress.Parse(tbIP.Text), Int32.Parse(tbPort.Text));
            socket.Bind(IPep);
            Thread listen = new Thread(Receive);
            listen.IsBackground = true;
            listen.Start();
        }
        void Receive()
        {
            while (true)
            {
                btnSend.Enabled = true;
                string received_data;
                socket.ReceiveFrom(state.buffer, 0, bufferSize, SocketFlags.None, ref epF);
                received_data = Encoding.UTF8.GetString(state.buffer);
                listView1.Items.Add(new ListViewItem { Text = received_data });
            }
            //byte[] data = new byte[1024];
            //    socket.BeginReceiveFrom(data, 0, data.Length, SocketFlags.None, ref ep , recv = ar => {
            //        try
            //        {
            //            byte[] del = (byte[])ar.AsyncState;
            //            int bytes = socket.EndReceiveFrom(ar, ref ep);
            //            socket.BeginReceiveFrom(del, 0, del.Length, SocketFlags.None, ref ep, recv, del);
            //            listView1.Items.Add(new ListViewItem() { new ListViewItem() { Text = Encoding.UTF8.GetString(del, 0, bytes) } });
            //        }
            //        catch { };
            //    }, buffer );
        }
        void Send()
        {
            byte[] data = Encoding.UTF8.GetBytes(tbText.Text);
            listView1.Items.Add(new ListViewItem { Text = tbText.Text });
            socket.SendTo(data, epF);
            //byte[] send_buffer = Encoding.UTF8.GetBytes(tbText.Text);
            //socket.BeginSend(send_buffer, 0, send_buffer.Length, SocketFlags.None, ar =>
            //{
            //    byte[] del = (byte[])ar.AsyncState;
            //    int bytes = socket.EndSend(ar);
            //}, buffer);
        }
        public ServerUDP()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Send();
            btnSend.Enabled = false;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            //udpClient = new UdpClient(Int32.Parse(tbPort.Text));
            //IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, Int32.Parse(tbPort.Text));
            //string received_data;
            //byte[] receive_byte_array;
            //while (true)
            //{
            //    receive_byte_array = udpClient.Receive(ref groupEP);
            //    received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
            //    if (received_data != string.Empty) listView1.Items.Add(received_data);
            //}
            Create();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ServerUDP_Load(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
        }
    }
}
