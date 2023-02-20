using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AppTrucadesPerSockets.Model
{
    public class HTTP
    {

        private Properties propietats;
        private int port = 80;

        public HTTP()
        {
            propietats = new Properties();
        }

        public String downloadHTTP()
        {
            String fileName;

            try
            {
                IPAddress ipAddress = IPAddress.Parse(propietats.Host);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                client.Connect(remoteEP);

                // Send request
                send_request(client, propietats.Host);

                // Get response
                fileName = get_response_binary(client);

                // Release the socket.
                client.Shutdown(SocketShutdown.Both);

                client.Close();

                return fileName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }

        }

        private void send_request(Socket client, string host)
        {
            // Convert the string data to byte data using ASCII encoding.

            String request = "GET " + "/" + propietats.Url + " HTTP/1.1\r\nHost: " + host + "\r\n\r\n";

            byte[] byteData = Encoding.ASCII.GetBytes(request);

            client.Send(byteData, byteData.Length, SocketFlags.None);

            return;
        }

        private string get_response(Socket client)
        {
            try
            {
                const int BufferSize = 256;
                byte[] buffer = new byte[BufferSize];
                StringBuilder sb = new StringBuilder();

                int bytesRead = client.Receive(buffer, BufferSize, SocketFlags.None);
                while (bytesRead > 0)
                {
                    sb.Append(Encoding.ASCII.GetString(buffer, 0, bytesRead));
                    bytesRead = client.Receive(buffer);
                }
                return sb.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
        }

        private String get_response_binary(Socket client)
        {
            const int BufferSize = 256;
            byte[] buffer = new byte[BufferSize];
            StringBuilder sb = new StringBuilder();
            String fileName = "clients.html";

            try
            {
                using (FileStream stream = File.Open(fileName, FileMode.Create))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {

                        int bytesRead = client.Receive(buffer, BufferSize, SocketFlags.None);
                        while (bytesRead > 0)
                        {
                            writer.Write(buffer, 0, bytesRead);
                            sb.Append(writer.ToString());
                            bytesRead = client.Receive(buffer);
                        }
                    }
                }
                return fileName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
