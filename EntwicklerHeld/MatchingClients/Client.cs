using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MatchingClientsCsharpImplementation
{
    public class Client
    {
        [JsonProperty("first_name")]
        public String FirstName;
        [JsonProperty("last_name")]
        public String LastName;
        [JsonProperty("phone_number")]
        public String PhoneNumber;
        [JsonProperty("street")]
        public String Street;
        [JsonProperty("house_number")]
        public String HouseNumber;
        [JsonProperty("postcode")]
        public String Postcode;
        [JsonProperty("client_id")]
        public String ClientId;
        [JsonProperty("city")]
        public String City;
        [JsonProperty("other_ids")]
        public List<String> OtherIds;

        Client(String firstName, String lastName, String phoneNumber, String street, String houseNumber, String postcode, String city, String clientId)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Street = street;
            this.HouseNumber = houseNumber;
            this.Postcode = postcode;
            this.ClientId = clientId;
            this.City = city;
            this.OtherIds = new List<String>();
        }

        [Newtonsoft.Json.JsonConstructor]
        Client(String first_name, String last_name, String phone_number, String street, String house_number, String postcode, String city, String client_id, List<String> other_ids)
        {
            this.FirstName = first_name;
            this.LastName = last_name;
            this.PhoneNumber = phone_number;
            this.Street = street;
            this.HouseNumber = house_number;
            this.Postcode = postcode;
            this.ClientId = client_id;
            this.City = city;
            this.OtherIds = other_ids;
        }
    }
}