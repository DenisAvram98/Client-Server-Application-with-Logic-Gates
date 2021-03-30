using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Socket ClientSocket;

        private void Form1_Load(object sender, EventArgs e)
        {
            int port = 13000;
            string IpAdress = "192.168.0.6";
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IpAdress), port);
            ClientSocket.Connect(ep);
            this.Text = "Client connected!";
            this.Refresh();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            string messageFromClient = null;
            messageFromClient = sendTB.Text;
             ClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(messageFromClient), 0, messageFromClient.Length, SocketFlags.None);

            //read from server
            byte[] msgFromServer = new byte[1024];
            int size = ClientSocket.Receive(msgFromServer);
            receiveTB.Text = "Server: " + System.Text.Encoding.ASCII.GetString(msgFromServer, 0, size);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClientSocket.Close();
        }
    }
}
