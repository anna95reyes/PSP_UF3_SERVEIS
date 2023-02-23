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
            txtHttpHost.Text = propietats.Http_host;
            txtHttpPort.Text = propietats.Http_port;
            txtHttpUrl.Text = propietats.Http_url;

            txtFtpHost.Text = propietats.Ftp_host;
            txtFtpPort.Text = propietats.Ftp_port;
            txtFtpUser.Text = propietats.Ftp_user;
            txtFtpPassword.Text = propietats.Ftp_password;

            txtSmtpHost.Text = propietats.Smtp_host;
            txtSmtpPort.Text = propietats.Smtp_port;
            txtSmtpMailFrom.Text = propietats.Smtp_mail_from;
            txtSmtpMailTo.Text = propietats.Smtp_mail_to;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            carregarPropietats();
            this.NavigationService.Navigate(new HomePage());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            propietats.modifySelectSingleNode("//properties/http/host", txtHttpHost.Text, propietats.Http_host);
            propietats.modifySelectSingleNode("//properties/http/port", txtHttpPort.Text, propietats.Http_port);
            propietats.modifySelectSingleNode("//properties/http/url", txtHttpUrl.Text, propietats.Http_url);

            propietats.modifySelectSingleNode("//properties/ftp/host", txtFtpHost.Text, propietats.Ftp_host);
            propietats.modifySelectSingleNode("//properties/ftp/port", txtFtpPort.Text, propietats.Ftp_port);
            propietats.modifySelectSingleNode("//properties/ftp/user", txtFtpUser.Text, propietats.Ftp_user);
            propietats.modifySelectSingleNode("//properties/ftp/password", txtFtpPassword.Text, propietats.Ftp_password);

            propietats.modifySelectSingleNode("//properties/smtp/host", txtSmtpHost.Text, propietats.Smtp_host);
            propietats.modifySelectSingleNode("//properties/smtp/port", txtSmtpPort.Text, propietats.Smtp_port);
            propietats.modifySelectSingleNode("//properties/smtp/mail_from", txtSmtpMailFrom.Text, propietats.Smtp_mail_from);
            propietats.modifySelectSingleNode("//properties/smtp/mail_to", txtSmtpMailTo.Text, propietats.Smtp_mail_to);

            this.NavigationService.Navigate(new HomePage());
        }

        
    }
}
