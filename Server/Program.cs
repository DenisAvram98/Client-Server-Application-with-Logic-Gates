using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 13000;
            string IpAdress = "192.168.0.3";
            Socket ServerListner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IpAdress), port);
            ServerListner.Bind(ep);
            ServerListner.Listen(100); //server is listening
            Console.WriteLine("Server is listening...");

            //client
            Socket ClientSocket = default(Socket);
            int count = 0;
            Program p = new Program();
            while (true)
            {
                count++;
                ClientSocket = ServerListner.Accept();
                Console.WriteLine(count + " Clients connected");
                Thread UserThread = new Thread(new ThreadStart(() => p.User(ClientSocket, Thread.CurrentThread))); //for every client difderent thread
                UserThread.Start();
            }
        }

        public static byte[] ReadToEnd(NetworkStream stream)
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

                    if (totalBytesRead == readBuffer.Length)
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
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
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

        public void User(Socket client, Thread userThread) //repplaying function
        {
            while (true)
            {
                NetworkStream stream = new NetworkStream(client);
                byte[] d = ReadToEnd(stream);
                stream.Close();
                //Console.WriteLine("Received: " + dataStr);

                DataTable dt = null;
                BinaryFormatter bFormatter = new BinaryFormatter();
                MemoryStream mStream = new MemoryStream(d);
                //mStream.Seek(0, SeekOrigin.Begin);
                if (mStream.Length!=0)
                {
                    dt = (DataTable)bFormatter.Deserialize(mStream);
                }
                mStream.Close();
                if (dt != null)
                {
                    /*
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Client ID: " + userThread.GetHashCode().ToString());
                    foreach (DataRow row in dt.Rows)
                    {
                        Console.WriteLine("----------------------");
                        Console.WriteLine("FSID: " + row["FirstSourceID"]);
                        Console.WriteLine("SSID: " + row["SecondSourceID"]);
                        Console.WriteLine("FST: " + row["FirstSourceType"]);
                        Console.WriteLine("SST: " + row["SecondSourceType"]);
                        Console.WriteLine("DID: " + row["DestinationID"]);
                        Console.WriteLine("DT: " + row["DestinationType"]);
                        Console.WriteLine("CID: " + row["CurrentID"]);
                        Console.WriteLine("CT: " + row["CurrentType"]);
                        Console.WriteLine("OR: " + row["OutputResult"]);
                        Console.WriteLine("FI: " + row["FirstInput"]);
                        Console.WriteLine("SI: " + row["SecondInput"]);
                        Console.WriteLine("IID: " + row["InputID"]);
                        Console.WriteLine("GN: " + row["GateName"]);
                    }
                    */
                    //Calculare rezultat
                    int rezultat = PoartaLogica.CalculRezultat(ref dt);
                    Console.WriteLine("----------------------");
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Client ID: " + userThread.GetHashCode().ToString() + " \nRezultat: " + rezultat);
                    /*foreach (DataRow row in dt.Rows)
                    {
                        Console.WriteLine("----------------------");
                        Console.WriteLine("FSID: " + row["FirstSourceID"]);
                        Console.WriteLine("SSID: " + row["SecondSourceID"]);
                        Console.WriteLine("FST: " + row["FirstSourceType"]);
                        Console.WriteLine("SST: " + row["SecondSourceType"]);
                        Console.WriteLine("DID: " + row["DestinationID"]);
                        Console.WriteLine("DT: " + row["DestinationType"]);
                        Console.WriteLine("CID: " + row["CurrentID"]);
                        Console.WriteLine("CT: " + row["CurrentType"]);
                        Console.WriteLine("OR: " + row["OutputResult"]);
                        Console.WriteLine("FI: " + row["FirstInput"]);
                        Console.WriteLine("SI: " + row["SecondInput"]);
                        Console.WriteLine("IID: " + row["InputID"]);
                        Console.WriteLine("GN: " + row["GateName"]);
                    }*/

                    DataTable dt2 = new DataTable();
                    DataColumn dc;

                    //template tabel nou
                    dc = new DataColumn();
                    dc.ColumnName = "FirstInput";
                    dc.DataType = Type.GetType("System.String");
                    dt2.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.ColumnName = "SecondInput";
                    dc.DataType = Type.GetType("System.String");
                    dt2.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.ColumnName = "GateName";
                    dc.DataType = Type.GetType("System.String");
                    dt2.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.ColumnName = "OutputResult";
                    dc.DataType = Type.GetType("System.String");
                    dt2.Columns.Add(dc);

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Int32.Parse(dr["CurrentType"].ToString())!=4 && Int32.Parse(dr["CurrentType"].ToString()) != 5 && Int32.Parse(dr["CurrentType"].ToString()) != 6) //diferit de Input 0, 1, Output
                        {
                            DataRow dr1 = dt2.NewRow();
                            dr1["FirstInput"] = dr["FirstInput"];
                            dr1["SecondInput"] = dr["SecondInput"].ToString() is "Server" ? "Inexistent" : dr["SecondInput"];
                            dr1["GateName"] = dr["GateName"].ToString() + " | ID: " + dr["CurrentID"].ToString();
                            dr1["OutputResult"] = dr["OutputResult"];
                            dt2.Rows.Add(dr1);
                        }
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Int32.Parse(dr["CurrentType"].ToString())==6)
                        {
                            DataRow dr1 = dt2.NewRow();
                            dr1["FirstInput"] = dr["FirstInput"];
                            dr1["SecondInput"] = dr["SecondInput"].ToString() is "Server" ? "Inexistent" : dr["SecondInput"];
                            dr1["GateName"] = dr["GateName"].ToString() + " | ID: " + dr["CurrentID"].ToString();
                            dr1["OutputResult"] = dr["OutputResult"];
                            dt2.Rows.Add(dr1);
                        }
                    }
                    foreach (DataRow row in dt2.Rows)
                    {
                        Console.WriteLine("----------------------");
                        Console.WriteLine("FI: " + row["FirstInput"]);
                        Console.WriteLine("SI: " + row["SecondInput"]);
                        Console.WriteLine("GN: " + row["GateName"]);
                        Console.WriteLine("OR: " + row["OutputResult"]);
                    }

                    //raspuns catre client
                    mStream = new MemoryStream();
                    dt2.RemotingFormat = SerializationFormat.Binary;
                    bFormatter.Serialize(mStream, dt2);

                    byte[] b = mStream.ToArray();
                    mStream.Close();

                    if (client.Poll(1000, SelectMode.SelectRead) && client.Available == 0)
                    {
                        Console.WriteLine("Clientul ID: "+ userThread.GetHashCode().ToString() + " sa deconectat de la server.");
                    }
                    else
                    {
                        client.Send(b, 0, b.Length, SocketFlags.None);
                    }
                }
            }
        }
    }
}
