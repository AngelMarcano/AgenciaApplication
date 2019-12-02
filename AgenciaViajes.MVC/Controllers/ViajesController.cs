using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgenciaViajes.MVC.Controllers
{
    public class ViajesController : Controller
    {
        // GET: Viajes
        public ActionResult Viajes()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateViajes()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditViajes(int IdViaje)
        {
            return View(IdViaje);
        }
        [HttpGet]
        public ActionResult DeleteViajes(int IdViaje)
        {
            return View(IdViaje);
        }
       

    }
}