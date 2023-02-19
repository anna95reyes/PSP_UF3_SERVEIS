using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AppTrucadesPerSockets.Model
{
    public class Properties
    {

        private String url;
        private String host;
        private String user;
        private String password;
        private String mail;

        private String path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\properties.xml";
        private XmlDocument xml;

        public Properties()
        {
            xml = new XmlDocument();
            xml.Load(Path);
            Url = getSelectSingleNode("//properties/url");
            Host = getSelectSingleNode("//properties/host");
            User = getSelectSingleNode("//properties/user");
            Password = getSelectSingleNode("//properties/password");
            Mail = getSelectSingleNode("//properties/mail");

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

        public string Url { get => url; set => url = value; }
        public string Host { get => host; set => host = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Path { get => path; set => path = value; }
    }
}
