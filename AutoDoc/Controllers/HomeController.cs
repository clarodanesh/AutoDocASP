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
            if (Session["UTYPE"] as string == "USER")
            {
                //do something interesting

                //since the user who is logged in is a standard user
                //open the users landing page
                return RedirectToAction("OpenUserLanding", "User");
            }
            else if (Session["UTYPE"] as string == "ADMIN")
            {
                return RedirectToAction("About", "Home");
            }
            else if (Session["UTYPE"] as string == "DOCTOR")
            {
                return RedirectToAction("About", "Home");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
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