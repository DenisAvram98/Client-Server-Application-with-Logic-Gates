using MindFusion.Diagramming;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form2 : Form
    {
        int labelCounter, counter;
        DataTable dt;
        Socket ClientSocket;

        public Form2()
        {
            InitializeComponent();
            diagram1.DiagramLinkStyle.Stroke = new MindFusion.Drawing.SolidBrush(Color.FromArgb(0, 52, 102));
            diagram1.DiagramLinkStyle.HeadStroke = new MindFusion.Drawing.SolidBrush(Color.FromArgb(0, 52, 102));
        }

        private void diagram1_Clicked(object sender, MindFusion.Diagramming.DiagramEventArgs e)
        {
            if (e.MouseButton==MouseButton.Left) //verificam ce sa apasat
            {
                if (listView1.SelectedItems.Count==0) //daca nu sa selectat nimic din lista cu circuite
                {
                    return;
                }
                if (NodTerminalExistent(listView1.SelectedItems[0].Index)==true) //nodul terminal nu exista sau este vorba despre alt nod care se poate duplica
                {
                    CreareNod(listView1.SelectedItems[0].Index, e.MousePosition.X, e.MousePosition.Y); //se creaza nodul (poarta) la pozitia unde sa dat click
                    listView1.Items[listView1.SelectedItems[0].Index].Selected = false; //deselectam circuitu din listview1
                }
                else
                {
                    MessageBox.Show("Exista deja un 'OUTPUT'.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else //daca nu ii dat clic stanga (aici o sa facem stergerea intregi diagrame folosind context menu)
            {
                diagram1.Selection.Clear();

                Point point = diagramView1.DocToClient((e.MousePosition));
                point.X = point.X + diagramView1.Bounds.X;
                contextMenuStrip2.Show(this, point);
            }
        }

        private void CreareNod(int index, float x, float y)
        {
            counter = counter + 1; //incrementam numarul de noduri (porti logice)
            ShapeNode nod = diagram1.Factory.CreateShapeNode(x - 16, y - 16, 70, 70); // pozitia x, pozitia y, latima, inaltimea
            nod.Transparent = true;
            nod.EnabledHandles = AdjustmentHandles.Move; //sa se poata muta nodu pe diagrama fara a fi schimbata dimensiunea nodului
            nod.Tag = index + "|" + counter.ToString(); // tagu contine indexul porti logice din listview1 si numarul de la counter

            nod.ImageAlign = MindFusion.Drawing.ImageAlign.TopLeft;
            nod.TextFormat.Alignment = StringAlignment.Center;
            nod.TextFormat.LineAlignment = StringAlignment.Far;
            nod.AnchorPattern = PoartaLogica.Ancorare(index); // daca modificam dimensiunea 70, 70 trebuie modificate si puncetele de ancorare

            //setam imaginea si textul pentru nod (poarta logica)
            if (index==0) //AND
            {
                nod.Image = imageList1.Images[0];
                nod.Text = "AND";
            }
            else if (index==1) //OR
            {
                nod.Image = imageList1.Images[1];
                nod.Text = "OR";
            }
            else if (index==2) //XOR
            {
                nod.Image = imageList1.Images[2];
                nod.Text = "XOR";
            }
            else if (index==3) //NOT
            {
                nod.Image = imageList1.Images[3];
                nod.Text = "NOT";
            }
            else if (index == 4) //INPUT 0
            {
                nod.Image = imageList1.Images[4];
                nod.Text = labelCounter + "#IN: 0";
                labelCounter = labelCounter + 1;
            }
            else if (index==5) //INPUT 1
            {
                nod.Image = imageList1.Images[5];
                nod.Text = labelCounter + "#IN: 1";
                labelCounter = labelCounter + 1;
            }
            else if (index==6) //terminator
            {
                nod.Image = imageList1.Images[6];
                nod.Text = "OUTPUT";
            }
        }

        private bool NodTerminalExistent(int index)
        {
            if (index == 6) //nodul terminal
            {
                foreach (ShapeNode nod in diagram1.Nodes) //parcurgem diagrama
                {
                    string[] s = nod.Tag.ToString().Split('|');
                    if (s.Length>0)
                    {
                        int test = Int32.Parse(s[0]);
                        if (test==6)
                        {
                            return false; // nodul terminal deja exista in diagrama
                        }
                    }
                }
                return true; //nodul nu exista in diagrama
            }
            else //daca nu este nod terminal se poate repeta nodu (circuitu logic)
            {
                return true;
            }
        }

        private void diagram1_NodeCreating(object sender, NodeValidationEventArgs e)
        {
            e.Cancel = true;
        }

        DiagramNode nod=null;
        private void diagram1_NodeClicked(object sender, NodeEventArgs e)
        {
            if (e.MouseButton==MouseButton.Right)
            {
                Point point = diagramView1.DocToClient((e.MousePosition));
                point.X = point.X + diagramView1.Bounds.X;
                contextMenuStrip1.Show(this, point);
                nod = e.Node;
            }
        }

        private void stergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Confirmati stergerea?", "Stergere", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (nod != null)
                {
                    diagram1.Nodes.Remove(nod);
                    nod = null;
                }
                else if (link != null)
                {
                    diagram1.Links.Remove(link);
                    link = null;
                }
            } 
        }

        DiagramLink link=null;
        private void diagram1_LinkClicked(object sender, LinkEventArgs e)
        {
            if (e.MouseButton == MouseButton.Right)
            {
                Point point = diagramView1.DocToClient((e.MousePosition));
                point.X = point.X + diagramView1.Bounds.X;
                contextMenuStrip1.Show(this, point);
                link = e.Link;
            }
        }

        private void stergeTotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Confirmati stergerea?", "Stergere", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                diagram1.ClearAll();
                labelCounter = 0;
                counter = -1;
            }
        }

        private void generareRezultatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = false;
            dt = null;
            dt = PoartaLogica.CreareTemplateDataTable(); //creare template tabela

            if (diagram1.Nodes.Count > 0)
            {
                foreach (ShapeNode sN in diagram1.Nodes) //parcurgem nodurile din diagrama
                {
                    string[] f = sN.Tag.ToString().Split('|'); // tagu contine tip poarta | counter (ID)
                    DiagramLinkCollection inputAnchorLinks = sN.IncomingLinks; //numarul de input intr-o poarta logica
                    DiagramLinkCollection outputAnchorLinks = sN.OutgoingLinks; //numarul de output dintr-o poarta logica

                    if (PoartaLogica.VerificareLegaturi(Int32.Parse(f[0]), inputAnchorLinks.Count, outputAnchorLinks.Count) == true) //verificam daca poarta logica este adecvat conectata
                    {
                        check = true;
                        //incarcam diagrama intr-un DataTable
                        DataRow dr = dt.NewRow();

                        if (inputAnchorLinks.Count==0) //INPUT Gates
                        {
                            dr["FirstSourceID"] = "Inexistent";
                            dr["SecondSourceID"] = "Inexistent";
                            dr["FirstSourceType"] = "Inexistent";
                            dr["SecondSourceType"] = "Inexistent";
                            dr["InputID"] = sN.Text.Substring(0, 1);
                        }
                        else if (inputAnchorLinks.Count==1) //OUTPUT & NOT Gates
                        {
                            string[] f1 = inputAnchorLinks[0].Origin.Tag.ToString().Split('|');
                            dr["FirstSourceID"] = f1[1];
                            dr["SecondSourceID"] = "Inexistent";
                            dr["FirstSourceType"] = f1[0];
                            dr["SecondSourceType"] = "Inexistent";
                        }
                        else if (inputAnchorLinks.Count==2) //AND, OR, XOR Gates
                        {
                            string[] f1 = inputAnchorLinks[0].Origin.Tag.ToString().Split('|');
                            string[] f2 = inputAnchorLinks[1].Origin.Tag.ToString().Split('|');
                            dr["FirstSourceID"] = f1[1];
                            dr["SecondSourceID"] = f2[1];
                            dr["FirstSourceType"] = f1[0];
                            dr["SecondSourceType"] = f2[0];
                        }

                        if (outputAnchorLinks.Count==0) //OUTPUT Gate
                        {
                            dr["DestinationID"] = "Inexistent";
                            dr["DestinationType"] = "Inexistent";
                        }
                        else if (outputAnchorLinks.Count==1) //ALL Gates expect OUTPUT
                        {
                            string[] f1 = outputAnchorLinks[0].Destination.Tag.ToString().Split('|');
                            dr["DestinationID"] = f1[1];
                            dr["DestinationType"] = f1[0];
                        }

                        dr["CurrentID"] = f[1];
                        dr["CurrentType"] = f[0];
                        dr["OutputResult"] = "Server";
                        dr["FirstInput"] = "Server";
                        dr["SecondInput"] = "Server";
                        dr["GateName"] = sN.Text;

                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        check = false;
                        MessageBox.Show("Circuitul logic este incorect! Verificati daca toate portile logice sunt conectate adecvat.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }
                //test
                //Form3 form3 = new Form3(dt);
                //form3.ShowDialog();
                //

                if (check == true)
                {
                    //Convertare DataSet - DataTable in byte[]
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    MemoryStream stream = new MemoryStream();
                    dt.RemotingFormat = SerializationFormat.Binary;
                    bFormatter.Serialize(stream, dt);

                    byte[] b = stream.ToArray();
                    stream.Close();

                    //trimiterea pe server
                    /*ClientSocket.Poll returns true if:
                        connection is closed, reset, terminated or pending (meaning no active connection)
                        connection is active and there is data available for reading
                    ClientSocket.Available returns number of bytes available for reading
                    If both are true:
                        there is no data available to read so connection is not active
                    */
                    if (ClientSocket.Poll(1000, SelectMode.SelectRead) && ClientSocket.Available == 0)
                    {
                        MessageBox.Show("Serverul nu este activ: " + "An existing connection was forcibly closed by the remote host");
                        conectareLaServerToolStripMenuItem.Enabled = true;
                        conectareLaServerToolStripMenuItem.Visible = true;
                    }
                    else
                    {
                        ClientSocket.Send(b, 0, b.Length, SocketFlags.None);
                        conectareLaServerToolStripMenuItem.Enabled = false;
                        conectareLaServerToolStripMenuItem.Visible = false;


                        //citirea de pe server
                        //string rez = null;
                        //byte[] rezultat = new byte[1024];
                        //int size = ClientSocket.Receive(rezultat);
                        //rez = "Rezultatul generat: " + Encoding.ASCII.GetString(rezultat, 0, size);

                        NetworkStream nStream = new NetworkStream(ClientSocket);
                        byte[] rezultat = ReadToEnd(nStream);
                        nStream.Close();

                        DataTable dt2 = null;
                        stream = new MemoryStream(rezultat);
                        if (stream.Length != 0)
                        {
                            dt2 = (DataTable)bFormatter.Deserialize(stream);
                        }
                        stream.Close();

                        if (dt2 != null)
                        {
                            Form3 form3 = new Form3(dt2);
                            form3.ShowDialog();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nu se poate genera un rezultat daca portile logice si legaturile lor nu au fost plasate in diagrama.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static byte[] ReadToEnd(NetworkStream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length) //daca sunt egale realocam o dimensiune mai mare
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                    if (stream.DataAvailable)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead) //dimensiunea alocata difera de dimensiunea totala citita
                {
                    buffer = new byte[totalBytesRead]; // cream un buffer de dimensiunea cat a fost transmis pe server
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead); // punem in buffer fix cat sa citit
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClientSocket.Close();
        }

        private void conectareLaServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ConectareServer();
                conectareLaServerToolStripMenuItem.Enabled = false;
                conectareLaServerToolStripMenuItem.Visible = false;
                MessageBox.Show("Conexiunea a fost restabilita cu succes.", "Conexiune server", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Serverul nu este activ: " + ex.Message);
                this.Text = "Connection error";
                this.Refresh();
                //this.Close();
            }
        }

        private void ConectareServer ()
        {
            //Conectare la server
            int port = 13000;
            string IpAdress = "192.168.0.3";
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(IpAdress), port);
            ClientSocket.Connect(ipep);
            this.Text = "Client connected!";
            this.Refresh();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            labelCounter = 1; // id-ul inputului cand avem mai multe inputuri
            listView1.Items[0].Selected = true;
            diagramView1.ScrollTo(0, 0);

            //showInstrucionBox();

            counter = -1; //numara noduri - elemente(porti) logice

            try
            {
                ConectareServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Serverul nu este activ: "+ ex.Message);
                this.Text = "Connection error";
                this.Refresh();
                this.Close();
            }
        }
    }
}
