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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user model)
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
                    return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult OpenUserLanding()
        {
            var entities = new userEntities();
            //var v = entities.users.Where(u => u.utype.Equals("USER")).ToList();
            return View(/*entities.users.ToList()*/);
        }

        [HttpPost]
        public ActionResult OpenUserLanding(user model)
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
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}