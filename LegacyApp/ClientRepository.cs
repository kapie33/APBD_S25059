using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyApp
{
    public class ClientRepository : IClientRepository
    {
        public static readonly Dictionary<int, Client> Database = new Dictionary<int, Client>()
        {
            {1, new Client { ClientId = 1, Name = "Kowalski", Address = "Warszawa, Złota 12", Email = "kowalski@wp.pl", Type = "NormalClient" }},
            {2, new Client { ClientId = 2, Name = "Malewski", Address = "Warszawa, Koszykowa 86", Email = "malewski@gmail.pl", Type = "VeryImportantClient" }},
            // More entries can be added here
        };

        public Client GetById(int clientId)
        {
            Thread.Sleep(new Random().Next(2000)); // Simulating delay
            if (Database.ContainsKey(clientId))
            {
                return Database[clientId];
            }
            throw new ArgumentException("Client not found");
        }
    }
}