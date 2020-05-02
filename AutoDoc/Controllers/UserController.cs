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

        //displays the users NOT USED, was just for test from labsheets
        public ActionResult DisplayUsers()
        {
            var entities = new userEntities();
            return View(entities.users.ToList());
        }

        //Get the login view if session set then route to correct controller
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["UTYPE"] as string == "USER" && Session["EMAIL"] != null)
            {

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

        //post the login data and login
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

        //modifies the password if pass is "password"
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

        //post the modified password and update in db
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

        //get the createuser view (register) 
        [HttpGet]
        public ActionResult CreateUser()
        {
            if (Session["UTYPE"] as string == "USER" && Session["EMAIL"] != null)
            {

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

        //post the data to db and register the user
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

        //open the users main landing page
        [HttpGet]
        public ActionResult OpenUserLanding()
        {
            var entities = new appointmentEntities();

            if (Session["UTYPE"] as string == "USER")
            {

                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //post the data to the db to book appt
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
                        if (IsTimeCorrect(m.time))
                        {
                            if (IsDateFuture(m.date))
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
                            else
                            {
                                ViewData["Message"] = "You need to select a date in the future";
                            }
                        }
                        else
                        {
                            ViewData["Message"] = "Surgery is only open between 9:00 and 17:30";
                        }
                    }
                }
                else
                {
                    ViewData["Message"] = "You already have an appointment, attend or cancel it before booking again";
                }
            }
            return View(m);
        }

        //check if the time is between opening and closing time of surgery
        private bool IsTimeCorrect(string t)
        {
            string[] splitTime = t.Split(':');
            int intTime = int.Parse(splitTime[0]);
            if (intTime <= 17 && intTime >= 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //check if the date is not in the past
        private bool IsDateFuture(string d)
        {
            DateTime systemDate = DateTime.UtcNow.Date;
            string formattedDate = systemDate.ToString("yyyy-MM-dd");
            string[] currDate = formattedDate.Split('-');
            string[] selectedDate = d.Split('-');
            int year, month, day, selectedYear, selectedMonth, selectedDay;
            selectedYear = int.Parse(selectedDate[0]);
            selectedMonth = int.Parse(selectedDate[1]);
            selectedDay = int.Parse(selectedDate[2]);
            year = int.Parse(currDate[0]);
            month = int.Parse(currDate[1]);
            day = int.Parse(currDate[2]);


            if (selectedYear >= year)
            {
                if(selectedMonth >= month || (selectedYear > year && selectedMonth <= month)){
                    if (selectedDay >= day || (selectedMonth > month && selectedDay <= day) || (selectedYear > year && selectedMonth <= month && selectedDay <= day))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //open the user detail update view
        [HttpGet]
        public ActionResult OpenUserDetails()
        {
            var entities = new appointmentEntities();

            if (Session["UTYPE"] as string == "USER")
            {

                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //post the updated details to db
        [HttpPost]
        public ActionResult OpenUserDetails(UserDetailsVM m)
        {
            if (ModelState.IsValid)
            {

                var db = new userEntities();
                var currentUserEmail = Session["EMAIL"] as string;
                var foundUser = db.users.Where(u => u.email.Equals(currentUserEmail) && u.utype.Equals("USER")).FirstOrDefault();


                foundUser.dob = m.date;
                foundUser.firstname = m.firstname;
                foundUser.lastname = m.lastname;

                db.SaveChanges();

                return RedirectToAction("OpenUserDetails", "User");
            }
            return View(m);
        }

        //cancel an already set appt
        public ActionResult CancelAppointment()
        {
            var db = new appointmentEntities();
            var currentUserEmail = Session["EMAIL"] as string;

            var v = db.appointments.Where(u => u.user.Equals(currentUserEmail) && u.astate.Equals("booked")).FirstOrDefault();
            db.appointments.Remove(v);
            db.SaveChanges();
            return RedirectToAction("OpenUserLanding", "User");
        }

        //log the users off by clearing session data
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}