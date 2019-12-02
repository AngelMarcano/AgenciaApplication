using AgenciaViajes.API.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace AgenciaViajes.API.Controllers
{
    public class ViajeroController : ApiController
    {
        // GET: Viajero
        [System.Web.Http.HttpGet]
        [EnableCorsAttribute("*", "*", "*")]
        public IHttpActionResult GetViajerosByIdViajero(int? IdViajero)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(Helper.GetConnectionString()))
                {
                    List<Viajero> Listaviajeros = null;
                    List<Viajero> viajeros = new List<Viajero>(); ;
                    Viajero viajero = null;
                    if (IdViajero == null)
                    {
                        Listaviajeros = connection.Query<Viajero>("sp_GetViajerosByIdViajero @IdViajero", new { IdViajero = IdViajero }).ToList();
                        if (Listaviajeros == null)
                        {
                            return Content(System.Net.HttpStatusCode.NotFound, "Los viajeros no fueron encontrados");
                        }
                        foreach (var viajeroItem in Listaviajeros)
                        {
                            var Direccion = connection.Query<Direccion>("sp_GetDireccionByIdDireccion @IdDireccion", new { IdDireccion = viajeroItem.IdDireccion }).FirstOrDefault();
                            viajeroItem.Direccion = Direccion;
                            viajeros.Add(viajeroItem);
                        }
                        return Ok(viajeros);
                    }
                    else
                    {
                        viajero = connection.Query<Viajero>("sp_GetViajerosByIdViajero @IdViajero", new { IdViajero = IdViajero }).FirstOrDefault();
                        if (viajero == null)
                        {
                            return Content(System.Net.HttpStatusCode.NotFound, "El viajero no fue encontrado");
                        }
                        var Direccion = connection.Query<Direccion>("sp_GetDireccionByIdDireccion @IdDireccion", new { IdDireccion = viajero.IdDireccion }).FirstOrDefault();
                        viajero.Direccion = Direccion;
                        return Ok(viajero);
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //
        [System.Web.Http.HttpGet]
        [EnableCorsAttribute("*", "*", "*")]
        public IHttpActionResult GetViajeroByCedula(int cedula)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(Helper.GetConnectionString()))
                {
                    Viajero viajero = null;
                    viajero = connection.Query<Viajero>("sp_GetViajeroByCedula @Cedula", new { Cedula = cedula }).FirstOrDefault();
                    if (viajero == null)
                    {
                        return Content(System.Net.HttpStatusCode.NotFound, "El viajero no fue encontrado");
                    }
                    return Ok(viajero);

                }
            }
            catch (Exception ex)
            {
                return Content(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Viajero/InsertViajero")]
        [ResponseType(typeof(Viajero))]
        [EnableCorsAttribute("*", "*", "*")]
        public IHttpActionResult InsertViajero(Viajero viajero)
        {
            if (!string.IsNullOrEmpty(viajero.Nombre) || !(viajero.Cedula == 0) || !string.IsNullOrEmpty(viajero.Direccion.Direccion1) ||
                !string.IsNullOrEmpty(viajero.Direccion.Ciudad) || !string.IsNullOrEmpty(viajero.Direccion.Estado) || !(viajero.Direccion.TelefonoMovil == 0))
            {

                try
                {
                    using (IDbConnection connection = new SqlConnection(Helper.GetConnectionString()))
                    {
                        if (viajero == null)
                        {
                            return Content(System.Net.HttpStatusCode.BadRequest, "La Entrada de datos es inválida ");
                        }
                        else
                        {
                            string parametros = "@Cedula, @Nombre, @Direccion1, @Direccion2, @Ciudad, @Estado, @TelefonoMovil, @TelefonoFijo";
                            var output = connection.Execute("sp_InsertarViajero " + parametros, new { Cedula = viajero.Cedula, Nombre = viajero.Nombre, Direccion1 = viajero.Direccion.Direccion1, Direccion2 = viajero.Direccion.Direccion2, Ciudad = viajero.Direccion.Ciudad, Estado = viajero.Direccion.Estado, TelefonoMovil = viajero.Direccion.TelefonoMovil, TelefonoFijo = viajero.Direccion.TelefonoFijo });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Content(System.Net.HttpStatusCode.InternalServerError, ex.Message);
                }

                return Ok();
            }
            else
            {
                return Content(System.Net.HttpStatusCode.BadRequest, "La Entrada de datos es inválida ");
            }
        }

        [System.Web.Http.HttpPost, System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Viajero/UpdateViajero2")]
        [ResponseType(typeof(Viajero))]
        [EnableCorsAttribute("*", "*", "*")]
        public IHttpActionResult UpdateViajero(Viajero viajero, int? IdViajero)
        {
            if (!string.IsNullOrEmpty(viajero.Nombre) || !(viajero.Cedula == 0) || !string.IsNullOrEmpty(viajero.Direccion.Direccion1) ||
               !string.IsNullOrEmpty(viajero.Direccion.Ciudad) || !string.IsNullOrEmpty(viajero.Direccion.Estado) || !(viajero.Direccion.TelefonoMovil == 0))
            {
                try
                {
                    using (IDbConnection connection = new SqlConnection(Helper.GetConnectionString()))
                    {
                        if (IdViajero == null)
                        {
                            return Content(System.Net.HttpStatusCode.BadRequest, "La Entrada de datos es inválida ");
                        }
                        else
                        {
                            var output = connection.Execute($"sp_ActualizarViajero @IdViajero = {IdViajero}, @Cedula = {viajero.Cedula}, @Nombre = '{viajero.Nombre}', @Direccion1 = '{viajero.Direccion.Direccion1}',  @Direccion2 = '{viajero.Direccion.Direccion2}', @Ciudad = '{viajero.Direccion.Ciudad}', @Estado = '{viajero.Direccion.Estado}', @TelefonoMovil = {viajero.Direccion.TelefonoMovil}, @TelefonoFijo = {viajero.Direccion.TelefonoFijo}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Content(System.Net.HttpStatusCode.InternalServerError, ex.Message);
                }
                return Ok();
            }
            else
            {
                return Content(System.Net.HttpStatusCode.BadRequest, "La Entrada de datos es inválida ");
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Viajero/DeleteViajero2")]
        [ResponseType(typeof(Viajero))]
        [EnableCorsAttribute("*", "*", "*")]
        public IHttpActionResult DeleteViajero(int? IdViajero)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(Helper.GetConnectionString()))
                {
                    if (IdViajero == null)
                    {
                        return Content(System.Net.HttpStatusCode.BadRequest, "La Entrada de datos es inválida ");
                    }
                    else
                    {
                        var output = connection.Execute($"sp_EliminarViajero @IdViajero = {IdViajero}");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }
    }
}