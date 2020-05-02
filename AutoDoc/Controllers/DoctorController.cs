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
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //delete the appointment from the db
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