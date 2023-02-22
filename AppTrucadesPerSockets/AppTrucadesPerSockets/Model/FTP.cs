using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AppTrucadesPerSockets.Model
{
    public class FTP
    {
        private Properties propietats;
        private int port = 21;
        private String s = "";

        public FTP()
        {
            propietats = new Properties();
        }

        public void updloadFileFTP(String fileName)
        {
            // Connect to a remote device.
            try
            {
                IPAddress ipAddress = IPAddress.Parse(propietats.Host);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                client.Connect(remoteEP);

                // Presentació
                Receive(client);

                // USER
                s = Send_Order(client, "USER " + propietats.User + "\r\n");

                // PASS 
                s = Send_Order(client, "PASS " + propietats.Password + "\r\n");

                // PWD
                s = Send_Order(client, "PWD" + "\r\n");

                // CWD
                s = Send_Order(client, "CWD /share" + "\r\n");


                // PASSV
                s = Send_Order(client, "PASV " + "\r\n");
                IPEndPoint passiveEP = passv();

                // STOR
                s = Send_Order(client, "STOR " + fileName + "\r\n");
                runStor(fileName, passiveEP);

                // Release the socket.
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private IPEndPoint passv()
        {
            int start = s.IndexOf("(", StringComparison.Ordinal);
            int stop = s.IndexOf(")", StringComparison.Ordinal);

            String d = s.Substring(start + 1, stop - start - 1);

            String[] parts = d.Split(",");
            String ip = parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];
            int passPort = int.Parse(parts[4]) * 256 + int.Parse(parts[5]);

            IPEndPoint passiveEP = new IPEndPoint(IPAddress.Parse(ip), passPort);
            return passiveEP;
        }

        private static string Receive(Socket client)
        {
            try
            {
                const int BufferSize = 256;
                byte[] buffer = new byte[BufferSize];
                StringBuilder sb = new StringBuilder();

                int bytesRead = client.Receive(buffer, BufferSize, SocketFlags.None);
                while (true)
                {
                    sb.Append(Encoding.ASCII.GetString(buffer, 0, bytesRead));
                    if (sb.ToString().Contains("\r\n"))
                        return sb.ToString();

                    bytesRead = client.Receive(buffer);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
        }

        private static string Send_Order(Socket client, string order)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(order);

            client.Send(byteData, byteData.Length, SocketFlags.None);

            return Receive(client);
        }

        private void runStor(string fileName, IPEndPoint passiveEP)
        {
            const int BufferSize = 256;
            byte[] buffer = new byte[BufferSize];
            int size;

            // Create a TCP/IP socket.
            Socket passiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            passiveSocket.Connect(passiveEP);

            
            FileStream fs = new FileStream(fileName, FileMode.Open);

            while ((size = fs.Read(buffer, 0, buffer.Length)) > 0)
            {
                passiveSocket.Send(buffer, size, 0);
            }

            fs.Close();

            passiveSocket.Close();
        }
    }
}
