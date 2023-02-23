using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AppTrucadesPerSockets.Model
{
    public class Properties
    {

        private String http_host;
        private String http_port;
        private String http_url;
        private String ftp_host;
        private String ftp_port;
        private String ftp_user;
        private String ftp_password;
        private String smtp_host;
        private String smtp_port;
        private String smtp_mail_from;
        private String smtp_mail_to;

        private String path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\properties.xml";
        private XmlDocument xml;

       

        public Properties()
        {
            xml = new XmlDocument();
            xml.Load(Path);

            Http_host = getSelectSingleNode("//properties/http/host");
            Http_port = getSelectSingleNode("//properties/http/port");
            Http_url = getSelectSingleNode("//properties/http/url");

            Ftp_host = getSelectSingleNode("//properties/ftp/host");
            Ftp_port = getSelectSingleNode("//properties/ftp/port");
            Ftp_user = getSelectSingleNode("//properties/ftp/user");
            Ftp_password = getSelectSingleNode("//properties/ftp/password");

            Smtp_host = getSelectSingleNode("//properties/smtp/host");
            Smtp_port = getSelectSingleNode("//properties/smtp/port");
            Smtp_mail_from = getSelectSingleNode("//properties/smtp/mail_from");
            Smtp_mail_to = getSelectSingleNode("//properties/smtp/mail_to");

        }

        private String getSelectSingleNode(string ubicacio)
        {
            XmlNode node = xml.SelectSingleNode(ubicacio);
            return node.FirstChild.Value;
        }

        public void modifySelectSingleNode(string ubicacio, String replaceValueNode, String propietat)
        {
            XmlNode node = xml.SelectSingleNode(ubicacio);
            node.FirstChild.Value = replaceValueNode;
            xml.Save(Path);
            propietat = replaceValueNode;
        }

        public string Http_host { get => http_host; set => http_host = value; }
        public string Http_port { get => http_port; set => http_port = value; }
        public string Http_url { get => http_url; set => http_url = value; }
        public string Ftp_host { get => ftp_host; set => ftp_host = value; }
        public string Ftp_port { get => ftp_port; set => ftp_port = value; }
        public string Ftp_user { get => ftp_user; set => ftp_user = value; }
        public string Ftp_password { get => ftp_password; set => ftp_password = value; }
        public string Smtp_host { get => smtp_host; set => smtp_host = value; }
        public string Smtp_port { get => smtp_port; set => smtp_port = value; }
        public string Smtp_mail_from { get => smtp_mail_from; set => smtp_mail_from = value; }
        public string Smtp_mail_to { get => smtp_mail_to; set => smtp_mail_to = value; }
        public string Path { get => path; set => path = value; }

    }
}
