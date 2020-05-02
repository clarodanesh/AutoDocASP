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
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //allows admin to manage the doctors
        public ActionResult ManageDoctors()
        {
            var entities = new appointmentEntities();

            if (Session["UTYPE"] as string == "ADMIN")
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //opens the view to manage patients
        public ActionResult ManagePatients()
        {
            var entities = new appointmentEntities();

            if (Session["UTYPE"] as string == "ADMIN")
            {

                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //allows admin to manage admins
        public ActionResult ManageAdmins()
        {
            var entities = new appointmentEntities();

            if (Session["UTYPE"] as string == "ADMIN")
            {

                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //shpows the update user form
        [HttpGet]
        public ActionResult ShowUpdateForm()
        {
            var entities = new appointmentEntities();

            if (Session["UTYPE"] as string == "ADMIN")
            {

                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //posts the update user form with get variables in url and viwew mdoel
        [HttpPost]
        public ActionResult ShowUpdateForm(string type, int id, UpdateVM m)
        {
            var entities = new userEntities();

            if (Session["UTYPE"] as string == "ADMIN")
            {
                if (ModelState.IsValid)
                {

                    var db = new userEntities();
                    var foundUser = db.users.Where(u => u.Id.Equals(id)).FirstOrDefault();

                    foundUser.dob = m.dob;
                    foundUser.firstname = m.firstname;
                    foundUser.lastname = m.lastname;

                    db.SaveChanges();

                    if (foundUser.utype == "DOCTOR")
                    {
                        return RedirectToAction("ManageDoctors", "Admin");
                    }
                    else if (foundUser.utype == "USER")
                    {
                        return RedirectToAction("ManagePatients", "Admin");
                    }
                    else if (foundUser.utype == "ADMIN")
                    {
                        return RedirectToAction("ManageAdmins", "Admin");
                    }
                }
                else
                {
                    return View();
                }
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //shows the add user form
        [HttpGet]
        public ActionResult ShowAddForm()
        {
            var entities = new appointmentEntities();

            if (Session["UTYPE"] as string == "ADMIN")
            {

                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //post the add user form with type from url
        [HttpPost]
        public ActionResult ShowAddForm(string type, AddVM m)
        {
            

            if (Session["UTYPE"] as string == "ADMIN")
            {
                if (ModelState.IsValid)
                {
                    var db = new userEntities();
                    var currentUserEmail = m.email;

                    var v = db.appointments.Where(u => u.user.Equals(currentUserEmail)).FirstOrDefault();

                    if (v == null)
                    {
                        var hash = Crypto.HashPassword("password");

                        db.users.Add(new user
                        {
                            email = m.email,
                            firstname = m.firstname,
                            lastname = m.lastname,
                            password = hash,
                            dob = m.dob,
                            utype = type
                        });
                        db.SaveChanges();

                        if (type == "DOCTOR")
                        {
                            return RedirectToAction("ManageDoctors", "Admin");
                        }
                        else if (type == "USER")
                        {
                            return RedirectToAction("ManagePatients", "Admin");
                        }
                        else if (type == "ADMIN")
                        {
                            return RedirectToAction("ManageAdmins", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("OpenAdminLanding", "Admin");
                        }
                    }
                    else
                    {
                        ViewData["Message"] = "User already exists";
                    }
                }
                else
                {
                    return View();
                }
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //delete a user
        public ActionResult Delete(string type, int id)
        {
            var db = new userEntities();
            var foundUser = db.users.Where(u => u.Id.Equals(id)).FirstOrDefault();
            db.users.Remove(foundUser);
            db.SaveChanges();

            if (type == "DOCTOR")
            {
                return RedirectToAction("ManageDoctors", "Admin");
            }
            else if (type == "USER")
            {
                return RedirectToAction("ManagePatients", "Admin");
            }
            else if (type == "ADMIN")
            {
                return RedirectToAction("ManageAdmins", "Admin");
            }
            else
            {
                return RedirectToAction("OpenAdminLanding", "Admin");
            }
        }

        //cancel the form
        public ActionResult Cancel(string type)
        {
            if (type == "DOCTOR")
            {
                return RedirectToAction("ManageDoctors", "Admin");
            }
            else if(type == "USER")
            {
                return RedirectToAction("ManagePatients", "Admin");
            }
            else if(type == "ADMIN")
            {
                return RedirectToAction("ManageAdmins", "Admin");
            }
            else
            {
                return RedirectToAction("OpenAdminLanding", "Admin");
            }
        }
    }
}