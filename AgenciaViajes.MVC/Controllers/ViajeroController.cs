using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgenciaViajes.MVC.Controllers
{
    public class ViajeroController : Controller
    {
        // GET: Viajero
        public ActionResult Viajero()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditViajero(int IdViajero)
        {
            return View(IdViajero);
        }
        [HttpGet]
        public ActionResult DeleteViajero(int IdViajero)
        {
            return View(IdViajero);
        }

        [HttpGet]
        public ActionResult CreateViajero()
        {
            return View();
        }
    }
}