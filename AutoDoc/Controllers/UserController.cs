using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoDoc.Models;

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
            return View();
        }

        /*[HttpGet]
        public ActionResult CheckUserExists()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckUserExists()
        {
            return View();
        }*/

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
                db.users.Add(new user
                {
                    email = model.email,
                    firstname = model.firstname,
                    lastname = model.lastname,
                    password = model.password,
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