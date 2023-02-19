using AppTrucadesPerSockets.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
    /// Lógica de interacción para UserControl1.xaml
    /// </summary>
    public partial class UIClient : UserControl
    {
        public UIClient()
        {
            InitializeComponent();
        }
        
        public Client ElClient
        {
            get { return (Client)GetValue(ElClientProperty); }
            set { SetValue(ElClientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ElClient.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElClientProperty =
            DependencyProperty.Register("ElClient", typeof(Client), typeof(UIClient), new PropertyMetadata(null, clientChangedCallbackStatic));

        private static void clientChangedCallbackStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIClient c = (UIClient)d;
            c.ClientChangedCallbackAsync(d, e);
        }

        private void ClientChangedCallbackAsync(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (ElClient != null)
            {
                txbNom.Text = ElClient.Nom;
                txbTelefon.Text = ElClient.Telefon;
            }

        }
    }
}
