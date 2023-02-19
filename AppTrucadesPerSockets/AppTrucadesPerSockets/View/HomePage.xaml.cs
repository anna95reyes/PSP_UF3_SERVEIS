using AppTrucadesPerSockets.Model;
using System;
using System.Collections.Generic;
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

        private Properties propietats;

        public HomePage()
        {
            InitializeComponent();
            propietats = new Properties();
            txbData.Text = "DATE: " + DateTime.Now.ToString("yyyy-MM-dd");
            txtUrl.Text = propietats.Url;

        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            lsvClients.ItemsSource = Client.getClients();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lsvClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
