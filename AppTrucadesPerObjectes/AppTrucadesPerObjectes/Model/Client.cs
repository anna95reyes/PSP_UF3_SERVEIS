using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppTrucadesPerObjectes.Model
{
    public class Client
    {
        private static ObservableCollection<Client> clients;

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

        public static void inicialitzaLlistaClients()
        {
            clients = new ObservableCollection<Client>();
        }

        public static void addClient(Client c)
        {
            clients.Add(c);
        }

        public static void removeClient (Client c)
        {
            clients.Remove(c);
        }

        public override string ToString()
        {
            return Nom + " - " + Telefon;
        }
    }
}
