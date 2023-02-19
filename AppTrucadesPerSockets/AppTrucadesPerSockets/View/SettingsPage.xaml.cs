using AppTrucadesPerSockets.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Lógica de interacción para SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {

        private Properties propietats;

        public SettingsPage()
        {
            InitializeComponent();
            propietats = new Properties();
            carregarPropietats();
        }

        private void carregarPropietats()
        {
            txtUrl.Text = propietats.Url;
            txtHost.Text = propietats.Host;
            txtUser.Text = propietats.User;
            txtPassword.Text = propietats.Password;
            txtMail.Text = propietats.Mail;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            carregarPropietats();
            this.NavigationService.Navigate(new HomePage());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            propietats.modifySelectSingleNode("//properties/url", txtUrl.Text, propietats.Url);
            propietats.modifySelectSingleNode("//properties/host", txtHost.Text, propietats.Host);
            propietats.modifySelectSingleNode("//properties/user", txtUser.Text, propietats.User);
            propietats.modifySelectSingleNode("//properties/password", txtPassword.Text, propietats.Password);
            propietats.modifySelectSingleNode("//properties/mail", txtMail.Text, propietats.Mail);

            this.NavigationService.Navigate(new HomePage());
        }

        
    }
}
