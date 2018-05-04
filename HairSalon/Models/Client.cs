using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Models
{
    public class Client
    {
        private string _name;
        private int _stylistId;
        private int _id;

        public Client(string name, int stylistId, int id=0)
        {
            _name = name;
            _stylistId = stylistId;
            _id = id;
        }

        public string GetName()
        {
            return _name;
        }
        public void SetName(string Name)
        {
            _name = Name;
        }

        public int GetClientId()
        {
            return _id;
        }

        public int GetStylistId()
        {
            return _stylistId;
        }


        public override bool Equals(System.Object otherClient)
        {
          if (!(otherClient is Client))
          {
            return false;
          }
          else
          {
             Client newClient = (Client) otherClient;
             bool idEquality = this.GetClientId() == newClient.GetClientId();
             bool nameEquality = this.GetName() == newClient.GetName();
             bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
             return (idEquality && nameEquality && stylistEquality);
           }
        }
        public override int GetHashCode()
        {
             return this.GetName().GetHashCode();
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@name, @stylist_id);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_id";
            stylistId.Value = this._stylistId;
            cmd.Parameters.Add(stylistId);


            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int clientId = rdr.GetInt32(0);
              string clientName = rdr.GetString(1);
              int stylistId = rdr.GetInt32(2);
              Client newClient = new Client(clientName, stylistId, clientId);
              allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int clientId = 0;
            string clientName = "";
            int clientCuisineId = 0;

            while(rdr.Read())
            {
              clientId = rdr.GetInt32(0);
              clientName = rdr.GetString(1);
              clientCuisineId = rdr.GetInt32(2);
            }
            Client newClient = new Client(clientName, clientCuisineId, clientId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newClient;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
