using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppTrucadesPerSockets.Model
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
            if (clients == null)
            {
                clients = new ObservableCollection<Client>();
                clients.Add(new Client("Client 1", "611111111"));
                clients.Add(new Client("Client 2", "611111112"));
                clients.Add(new Client("Client 3", "611111113"));
                clients.Add(new Client("Client 4", "611111114"));
                clients.Add(new Client("Client 5", "611111115"));
                clients.Add(new Client("Client 6", "611111116"));
                clients.Add(new Client("Client 7", "611111117"));
                clients.Add(new Client("Client 8", "611111118"));
                clients.Add(new Client("Client 9", "611111119"));
            }
            return clients;
        }


        public override string ToString()
        {
            return Nom + " - " + Telefon;
        }
    }
}
