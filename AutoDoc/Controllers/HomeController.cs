using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoDoc.Models;

namespace AutoDoc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //check if a session is set and if a session is set
            //then go to the dashboard of that utype
            //if not set open login
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}