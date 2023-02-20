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
            txtUrl.Text = propietats.Host + "/" + propietats.Url;

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
            String telefon = "";

            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains("<td>"))
                {
                    tds.Add(line);
                }
            }

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
                    telefon = td;
                    Client.addClient(new Client(nom, telefon));
                    nom = "";
                    telefon = "";
                }
            }
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lsvClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //txtTextEmail.Text = lsvClients.SelectedItem.ToString();
        }
    }
}
