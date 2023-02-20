using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppTrucadesPerSockets.Model
{
    public class Client
    {
        private static ObservableCollection<Client> clients = new ObservableCollection<Client>();

        private String nom;
        private String telefon;

        public Client(string nom, string telefon)
        {
            Nom = nom;
            Telefon = telefon;
        }

        public string Nom { get => nom; set => nom = value; }
        public string Telefon { get => telefon; set => telefon = value; }

        
        public static ObservableCollection<Client> getClients()
        {
            return clients;
        }


        public static void addClient(Client c)
        {
            clients.Add(c);
        }

        public override string ToString()
        {
            return Nom + " - " + Telefon;
        }
    }
}
