using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AppTrucadesPerObjectes.Model
{
    public class FTP
    {
        private Properties propietats;

        public FTP()
        {
            propietats = new Properties();
        }

        public void updloadFileFTP(String fileName)
        {
            const int BufferSize = 256;
            byte[] buffer = new byte[BufferSize];
            int size;
            // Connect to a remote device.
            try
            {
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://" + propietats.Ftp_host + "/share/" + fileName);
                request.Credentials = new NetworkCredential(propietats.Ftp_user, propietats.Ftp_password);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                Stream stream = request.GetRequestStream();
                FileStream fs = new FileStream(fileName, FileMode.Open);
                
                while ((size = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, size);
                }

                fs.Close();
                stream.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

    }
}
