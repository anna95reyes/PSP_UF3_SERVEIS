using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AppTrucadesPerObjectes.Model
{
    public class HTTP
    {

        private Properties propietats;

        public HTTP()
        {
            propietats = new Properties();
        }

        public String DownloadHTTP()
        {

            String fileName = "clients.html";

            try
            {
                Uri uri = new Uri("http://" + propietats.Http_host + "/" + propietats.Http_url);
                HttpWebRequest request = WebRequest.CreateHttp(uri);
                request.Method = "POST";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (FileStream stream = File.Open(fileName, FileMode.Create))
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(reader.ReadToEnd());
                        }
                    }
                }
                
                response.Close();

                return fileName;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return "";
            }

        }

        
    }
}
