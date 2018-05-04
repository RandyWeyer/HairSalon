using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {

          [HttpGet("/")]
          public ActionResult Index()
          {

          return View();
          }

          [HttpGet("/view-all-stylists")]
          public ActionResult ViewAllStylists()
          {

          List<Stylist> allStylists = Stylist.GetAll();
         return View(allStylists);

         }

         [HttpGet("/view-all-clients")]
         public ActionResult ViewAllClients()
         {

         List<Client> allClients = Client.GetAll();
        return View(allClients);
         }

         [HttpGet("/create-new-client")]
         public ActionResult CreateNewClient()
         {
         List<Stylist> allStylists = Stylist.GetAll();
        return View(allStylists);
         }

        [HttpPost("/submit")]
        public ActionResult Submit()
        {
          int newClientStylist = int.Parse(Request.Form["client-stylist"]);
          string newClientName = Request.Form["restaurant-name"];
          Client newClient = new Client(newClientName, newClientStylist);
          newClient.Save(); //This line doesn't work
          return View("Index");
        }
    }
}
