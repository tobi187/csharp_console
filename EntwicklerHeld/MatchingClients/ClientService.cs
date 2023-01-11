using System;
using System.Data;
using System.Linq;

namespace MatchingClientsCsharpImplementation
{
    public class ClientService
    {
        public static Boolean AddClient(Client client)
        {
            var hasClient = ClientDatabase.GetAllClients().Where(
                x =>
                Comp(x.FirstName, client.FirstName) &&
                Comp(x.LastName, client.LastName) &&
                Comp(x.PhoneNumber, client.PhoneNumber) &&
                Comp(x.Street, client.Street) &&
                Comp(x.HouseNumber, client.HouseNumber) &&
                Comp(x.Postcode, client.Postcode) &&
                Comp(x.City, client.City) &&
                Comp(x.ClientId, client.ClientId)
            );

            if (hasClient.Count() > 0) return false;
            var clientId = ClientDatabase.GetAllClients().Find(x => x.ClientId == client.ClientId);
            if (clientId == null)
            {
                ClientDatabase.AddClient(client);
            }
            else
            {
                clientId.FirstName = client.FirstName;
                clientId.LastName = client.LastName;
                clientId.PhoneNumber = client.PhoneNumber;
                clientId.Street = client.Street;
                clientId.HouseNumber = client.HouseNumber;
                clientId.Postcode = client.Postcode;
                clientId.City = client.City;
            }
            return true;
        }

        public static bool Comp(Client c1, Client c2)
        {
            c1.
        }
    }

    internal class ClientDatabase
    {
        public List<Client> GetAllClients()
        {
            return new List<Client>();
        }
        public 
    }
}