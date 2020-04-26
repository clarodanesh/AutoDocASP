using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using AutoDoc.Models;
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
        public ActionResult CheckUserExists()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckUserExists(user model)
        {
            if (ModelState.IsValid)
            {
                var db = new userEntities();

                var v = db.users.Where(u => u.firstname.Equals(model.firstname)).FirstOrDefault();
                if (v != null)
                {
                    ViewData["Message"] = "Record exists";
                }
                else
                {
                    ViewData["Message"] = "Fail";
                }
            }
            return View(model);

        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(user model)
        {
            if (ModelState.IsValid)
            {
                var db = new userEntities();

                var hash = Crypto.HashPassword(model.password);
                
                db.users.Add(new user
                {
                    email = model.email,
                    firstname = model.firstname,
                    lastname = model.lastname,
                    password = hash,
                    dob = model.dob,
                    utype = model.utype
                });
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

    }
}