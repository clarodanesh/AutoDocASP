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
        //routes the user if logged in to correct controller or to login
        public ActionResult Index()
        {

            if (Session["UTYPE"] as string == "USER")
            {
                return RedirectToAction("OpenUserLanding", "User");
            }
            else if (Session["UTYPE"] as string == "ADMIN")
            {
                return RedirectToAction("OpenAdminLanding", "Admin");
            }
            else if (Session["UTYPE"] as string == "DOCTOR")
            {
                return RedirectToAction("OpenDoctorLanding", "Doctor");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "about not used";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "contact not used";

            return View();
        }
    }
}