using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}