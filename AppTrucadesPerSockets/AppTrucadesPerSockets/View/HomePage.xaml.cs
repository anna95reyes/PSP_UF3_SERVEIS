using AppTrucadesPerSockets.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppTrucadesPerSockets.View
{
    /// <summary>
    /// Lógica de interacción para HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        DateTime dataActual;
        private Properties propietats;

        public HomePage()
        {
            InitializeComponent();
            propietats = new Properties();
            dataActual = DateTime.Now;
            txbData.Text = "DATE: " + dataActual.ToString("yyyy-MM-dd");
            txtUrl.Text = propietats.Http_host + "/" + propietats.Http_url;
            btnDoneIsEnabled();

        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            HTTP http = new HTTP();
            readFileICarregarClients(http.downloadHTTP());
            lsvClients.ItemsSource = Client.getClients();
        }

        private void readFileICarregarClients(String fileName)
        {
            using FileStream fs = File.OpenRead(fileName);
            using StreamReader sr = new StreamReader(fs);

            String line;
            ObservableCollection<String> tds = new ObservableCollection<String>();
            String nom = "";

            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("<td>"))
                {
                    tds.Add(line);
                }
            }

            fs.Close();

            Client.inicialitzaLlistaClients();

            for (int i = 0; i < tds.Count; i++)
            {
                String td = tds[i].Trim();
                td = td.Substring(4, td.Length - 9);
                if (i % 2 == 0)
                {
                    nom = td;
                }
                else
                {
                    Client.addClient(new Client(nom, td));
                    nom = "";
                }
            }
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            Client client = (Client)lsvClients.SelectedItem;
            FTP ftp = new FTP();
            SMTP smtp = new SMTP();
            String nameClientFile = client.Nom.Replace(" ", "_");
            String fileName = dataActual.ToString("yyyy-MM-dd_HH-mm-ss") + "_" + nameClientFile + ".txt";
            String subject = dataActual.ToString("dd-MM-yyyy HH:mm:ss") + " - " + client.Nom;
            crearArxiu(fileName);
            ftp.updloadFileFTP(fileName);
            smtp.sendEmail(subject, txtTextEmail.Text);
            netejarFormulari(client);
            eliminarArxiu(fileName);
        }

        private static void eliminarArxiu(string fileName)
        {
            File.Delete(fileName);
        }

        private void netejarFormulari(Client client)
        {
            txtTextEmail.Text = "";
            Client.removeClient(client);
            lsvClients.SelectedItem = null;
        }

        private void crearArxiu(string fileName)
        {
            FileStream fs = File.Open(fileName, FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes(txtTextEmail.Text);

            fs.Write(bytes, 0, bytes.Length);

            fs.Close();
        }

        private void lsvClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDoneIsEnabled();
        }

        private void txtTextEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnDoneIsEnabled();
        }

        private void btnDoneIsEnabled()
        {
            btnDone.IsEnabled = lsvClients.SelectedItem != null && txtTextEmail.Text.Trim().Length > 0;
        }
    }
}
