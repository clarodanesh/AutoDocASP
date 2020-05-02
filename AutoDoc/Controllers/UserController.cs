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
                    if(Crypto.VerifyHashedPassword(v.password, "password"))
                    {
                        Session["TEMP"] = v.email;
                        return RedirectToAction("ModifyPassword", "User");
                    }
                    else
                    {
                        //ViewData["Message"] = "Record exists";
                        Session["UTYPE"] = v.utype;
                        Session["EMAIL"] = v.email;
                        return RedirectToAction("Index", "Home");
                    } 
                }
                else
                {
                    ViewData["Message"] = "Incorrect details entered";
                }
            }
            return View(model);
            
        }

        [HttpGet]
        public ActionResult ModifyPassword()
        {
            if (Session["TEMP"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult ModifyPassword(PasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var db = new userEntities();
                var currentUserEmail = Session["TEMP"];

                var v = db.users.Where(u => u.email == currentUserEmail).FirstOrDefault();

                if (v != null)
                {
                    var hash = Crypto.HashPassword(model.password);
                    v.password = hash;

                    db.SaveChanges();
                    //Session["UTYPE"] = "USER";
                    //Session["EMAIL"] = Session["TEMP"];
                    Session.Remove("TEMP");
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    ViewData["Message"] = "User doesnt exist";
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
                var currentUserEmail = model.email;

                var v = db.appointments.Where(u => u.user.Equals(currentUserEmail)).FirstOrDefault();

                if (v == null)
                {
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
                else
                {
                    ViewData["Message"] = "User already exists";
                }
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
        public ActionResult OpenUserLanding(UserLandingVM m)
        {
            if (ModelState.IsValid)
            {
                var db = new userEntities();
                var currentUserEmail = Session["EMAIL"] as string;

                var v = db.appointments.Where(u => u.user.Equals(currentUserEmail) && u.astate.Equals("booked")).FirstOrDefault();

                if (v == null)
                {
                    var aBooking = db.appointments.Where(u => u.doctor.Equals(m.doctor) && u.date.Equals(m.date) && u.time.Equals(m.time) && u.astate.Equals("booked")).FirstOrDefault();
                    if (aBooking != null)
                    {
                        ViewData["Message"] = "This doctor is booked at this time and on this date, choose another";
                    }
                    else
                    {
                        db.appointments.Add(new appointment
                        {
                            doctor = m.doctor,
                            user = Session["EMAIL"] as string,
                            date = m.date,
                            time = m.time,
                            astate = "booked"
                        });
                        db.SaveChanges();
                        return RedirectToAction("OpenUserLanding", "User");
                    }
                }
                else
                {
                    ViewData["Message"] = "You already have an appointment, attend or cancel it before booking again";
                }
            }
            return View(m);
        }

        [HttpGet]
        public ActionResult OpenUserDetails()
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
        public ActionResult OpenUserDetails(UserDetailsVM m)
        {
            if (ModelState.IsValid)
            {
                /*// get a tracked entity
                var entity = context.User.Find(userId);
                entity.someProp = someValue;
                // other property changes might come here
                context.SaveChanges();*/
                var db = new userEntities();
                var currentUserEmail = Session["EMAIL"] as string;
                var foundUser = db.users.Where(u => u.email.Equals(currentUserEmail) && u.utype.Equals("USER")).FirstOrDefault();
                //var foundUser = db.users.Find(currentUserEmail);

                foundUser.dob = m.date;
                foundUser.firstname = m.firstname;
                foundUser.lastname = m.lastname;

                db.SaveChanges();

                return RedirectToAction("OpenUserDetails", "User");
            }
            return View(m);
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