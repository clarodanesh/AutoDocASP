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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayUsers()
        {
            var entities = new userEntities();
            return View(entities.users.ToList());
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["UTYPE"] as string == "USER" && Session["EMAIL"] != null)
            {
                //do something interesting

                //since the user who is logged in is a standard user
                //open the users landing page
                return RedirectToAction("OpenUserLanding", "User");

            }
            else if (Session["UTYPE"] as string == "DOCTOR" && Session["EMAIL"] != null)
            {
                return RedirectToAction("OpenDoctorLanding", "Doctor");
            }
            else if (Session["UTYPE"] as string == "ADMIN" && Session["EMAIL"] != null)
            {
                return RedirectToAction("OpenAdminLanding", "Admin");
            }
            else
            {
                return View();
            }
            //return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var db = new userEntities();
                var hashedPass = Crypto.HashPassword(model.password);

                var v = db.users.Where(u => u.email.Equals(model.email)).FirstOrDefault();
                if (v != null && v.email == model.email && Crypto.VerifyHashedPassword(v.password, model.password))
                {
                    //ViewData["Message"] = "Record exists";
                    Session["UTYPE"] = v.utype;
                    Session["EMAIL"] = v.email;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Message"] = "Incorrect details entered";
                }
            }
            return View(model);
            
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            if (Session["UTYPE"] as string == "USER" && Session["EMAIL"] != null)
            {
                //do something interesting

                //since the user who is logged in is a standard user
                //open the users landing page
                return RedirectToAction("OpenUserLanding", "User");

            }
            else if (Session["UTYPE"] as string == "DOCTOR" && Session["EMAIL"] != null)
            {
                return RedirectToAction("OpenDoctorLanding", "Doctor");
            }
            else if (Session["UTYPE"] as string == "ADMIN" && Session["EMAIL"] != null)
            {
                return RedirectToAction("OpenAdminLanding", "Admin");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateUser(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var db = new userEntities();

                var hash = Crypto.HashPassword(model.password);
                var user = "USER";

                db.users.Add(new user
                {
                    email = model.email,
                    firstname = model.firstname,
                    lastname = model.lastname,
                    password = hash,
                    dob = model.dob,
                    utype = user
                });
                db.SaveChanges();
                Session["UTYPE"] = "USER";
                Session["EMAIL"] = model.email;
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult OpenUserLanding()
        {
            var entities = new appointmentEntities();
            //var currentUserEmail = Session["EMAIL"] as string;
            //var v = entities.appointments.Where(u => u.user.Equals(currentUserEmail)).ToList();
            if (Session["UTYPE"] as string == "USER")
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

        [HttpPost]
        public ActionResult OpenUserLanding(appointment model, user m)
        {
            if (ModelState.IsValid)
            {
                var db = new userEntities();
                
                db.appointments.Add(new appointment
                { 
                    doctor = m.doctor,
                    user = Session["EMAIL"] as string,
                    date = model.date,
                    time = model.time,
                    astate = "booked"
                });
                db.SaveChanges();
                return RedirectToAction("OpenUserLanding", "User");
            }
            return View(model);
        }

        public ActionResult CancelAppointment()
        {
            var db = new appointmentEntities();
            var currentUserEmail = Session["EMAIL"] as string;

            var v = db.appointments.Where(u => u.user.Equals(currentUserEmail) && u.astate.Equals("booked")).FirstOrDefault();
            db.appointments.Remove(v);
            db.SaveChanges();
            return RedirectToAction("OpenUserLanding", "User");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}