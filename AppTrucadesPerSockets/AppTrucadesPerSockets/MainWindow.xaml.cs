﻿using AppTrucadesPerSockets.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace AppTrucadesPerSockets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _mainFrame.Navigate(new HomePage());
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            /*SettingsWindow window = new SettingsWindow();
            window.Show();
            window.Activate();*/

            _mainFrame.NavigationService.Navigate(new SettingsPage());
        }
    }
}
