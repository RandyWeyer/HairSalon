using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTests : IDisposable
    {
        public void Dispose()
        {
            Client.DeleteAll();
        }
        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password="";port=3306;database=hair_salon;";
        }

        [TestMethod]
        public void ClientList_isEmpty()
        {
            int result = Client.GetAll().Count;

            Assert.AreEqual(0, result);
        }
    }

    [TestClass]
    public class StylistTests
    {
        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password="";port=3306;database=hair_salon;";
        }

        [TestMethod]
        public void Stylist_returns_Chris()
        {
            Stylist result = Stylist.Find(1);
            string finalResult = result.GetStylistName();

            Assert.AreEqual("Chris", finalResult);
        }
    }
}
