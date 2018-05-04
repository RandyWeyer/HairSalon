using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Models
{
    public class Stylist
    {
        private string _name;
        private string _stylistDetails;
        private int _id;

        public Stylist(string name, string stylistDetails, int Id = 0)
        {
            _name = name;
            _stylistDetails = stylistDetails;
            _id = Id;
        }

        public int GetStylistId()
        {
          return _id;
        }

        public string GetStylistName()
        {
          return _name;
        }

        public string GetStylistDetails()
        {
          return _stylistDetails;
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int StylistId = rdr.GetInt32(0);
              string StylistName = rdr.GetString(1);
              string StylistDetails = rdr.GetString(2);
              Stylist newStylist = new Stylist(StylistName, StylistDetails, StylistId);
              allStylists.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }

        public static Stylist Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int StylistId = 0;
            string StylistName = "";
            string StylistDetails = "";

            while(rdr.Read())
            {
              StylistId = rdr.GetInt32(0);
              StylistName = rdr.GetString(1);
              StylistDetails = rdr.GetString(2);
            }
            Stylist newStylist = new Stylist(StylistName, StylistDetails, StylistId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newStylist;
        }
    }
}
