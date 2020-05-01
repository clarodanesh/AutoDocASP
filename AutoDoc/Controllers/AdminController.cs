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

        [HttpGet]
        public ActionResult ShowUpdateForm()
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

        [HttpPost]
        public ActionResult ShowUpdateForm(string type, int id, UpdateVM m)
        {
            var entities = new userEntities();
            //var currentUserEmail = Session["EMAIL"] as string;
            //var v = entities.appointments.Where(u => u.user.Equals(currentUserEmail)).ToList();
            if (Session["UTYPE"] as string == "ADMIN")
            {
                if (ModelState.IsValid)
                {
                    /*// get a tracked entity
                    var entity = context.User.Find(userId);
                    entity.someProp = someValue;
                    // other property changes might come here
                    context.SaveChanges();*/
                    var db = new userEntities();
                    var foundUser = db.users.Where(u => u.Id.Equals(id)).FirstOrDefault();
                    //var foundUser = db.users.Find(currentUserEmail);

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