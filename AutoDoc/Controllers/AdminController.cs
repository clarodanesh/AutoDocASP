using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using AutoDoc.Models;
using AutoDoc.ViewModels;
using System.Web.Helpers;

namespace AutoDoc.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult OpenAdminLanding()
        {
            if (Session["UTYPE"] as string == "ADMIN")
            {
                //do something interesting

                //since the user who is logged in is a standard user
                //open the users landing page
                return View(/*entities.appointments.ToList()*/);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ManageDoctors()
        {
            var entities = new appointmentEntities();
            //var currentUserEmail = Session["EMAIL"] as string;
            //var v = entities.appointments.Where(u => u.user.Equals(currentUserEmail)).ToList();
            if (Session["UTYPE"] as string == "ADMIN")
            {
                //do something interesting

                //since the user who is logged in is a standard user
                //open the users landing page
                return View(/*entities.appointments.ToList()*/);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}