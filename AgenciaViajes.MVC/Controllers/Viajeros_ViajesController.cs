using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgenciaViajes.MVC.Controllers
{
    public class Viajeros_ViajesController : Controller
    {
        // GET: Viajeros_Viajes
        public ActionResult ViajerosViajes()
        {
            return View();
        }

        public ActionResult CreateViajeroViaje()
        {
            return View();
        }

        public ActionResult EditViajeroViaje(int IdViaje)
        {
            return View(IdViaje);
        }

        public ActionResult DeleteViajeroViaje(int IdViaje)
        {
            return View(IdViaje);
        }
    }
}