using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AppTrucadesPerSockets.Model
{
    public class SMTP
    {
        private Properties propietats;
        private int port;

        public SMTP()
        {
            propietats = new Properties();
        }

        public void sendEmail(String subject, String message)
        {
            try
            {
                port = int.Parse(propietats.Smtp_port);
                IPAddress ipAddress = IPAddress.Parse(propietats.Smtp_host);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                client.Connect(remoteEP);

                // Send request
                String result = smtp(client, subject, message);

                // Release the socket.
                client.Shutdown(SocketShutdown.Both);

                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private string smtp(Socket client, String subject, String message)
        {
            // User: admin
            String userId = Base64Encode("admin");

            // Pass: annaanna
            String passId = Base64Encode("annaanna");

            String result = "";

            // Rebem la salutacio
            result = Receive(client);

            // Send HELO
            result = Send_Order(client, "HELO\r\n");
            result = Receive(client);

            // AUTH LOGIN
            result = Send_Order(client, "AUTH LOGIN\r\n");
            result = Receive(client);

            // User
            result = Send_Order(client, userId + "\r\n");
            result = Receive(client);

            // Pass
            result = Send_Order(client, passId + "\r\n");
            result = Receive(client);

            // Mail From
            result = Send_Order(client, "MAIL FROM: " + propietats.Smtp_mail_from + "\r\n");
            result = Receive(client);

            // Mail To
            result = Send_Order(client, "RCPT TO: " + propietats.Smtp_mail_to + "\r\n");
            result = Receive(client);

            // Send DATA
            result = Send_Order(client, "DATA" + "\r\n");
            result = Receive(client);

            // Mail content
            // From: 
            result = Send_Order(client, "From: ANNA <" + propietats.Smtp_mail_from + ">\r\n");

            // To
            result = Send_Order(client, "To: PSP <" + propietats.Smtp_mail_to + ">\r\n");

            // Subject
            result = Send_Order(client, "Subject: " + subject + "\r\n");

            // End Of Header
            result = Send_Order(client, "\r\n");

            result = Send_Order(client, message);

            // Mail EOF
            result = Send_Order(client, "\r\n.\r\n");
            result = Receive(client);

            // Mail QUIT
            result = Send_Order(client, "QUIT\r\n");
            result = Receive(client);


            return result;
        }

        private static string Send_Order(Socket client, string order)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(order);

            client.Send(byteData, byteData.Length, SocketFlags.None);
            return "";
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

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
