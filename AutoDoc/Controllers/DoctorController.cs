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
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult OpenDoctorLanding()
        {
            if (Session["UTYPE"] as string == "DOCTOR")
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

        public ActionResult DeleteAppointment(int id)
        {
            var db = new appointmentEntities();
            var foundAppointment = db.appointments.Where(u => u.Id.Equals(id)).FirstOrDefault();
            db.appointments.Remove(foundAppointment);
            db.SaveChanges();

            return RedirectToAction("OpenDoctorLanding", "Doctor");

        }
    }
}