using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDoc.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult OpenDoctorLanding()
        {
            return View();
        }
    }
}