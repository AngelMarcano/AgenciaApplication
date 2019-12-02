using AgenciaViajes.API.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace AgenciaViajes.API.Controllers
{
    public class Viajero_ViajesController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Viajero_Viajes/InsertViajeroViajes")]
        [ResponseType(typeof(Viajero))]
        [EnableCorsAttribute("*", "*", "*")]
        public IHttpActionResult InsertViajeroViajes(ViajeroViaje viajeroViaje)
        {
            if (!string.IsNullOrEmpty(viajeroViaje.Viajero.Nombre) && (viajeroViaje.Viajero.Cedula != 0) && !string.IsNullOrEmpty(viajeroViaje.Viajero.Direccion.Direccion1) &&
                !string.IsNullOrEmpty(viajeroViaje.Viajero.Direccion.Ciudad) && !string.IsNullOrEmpty(viajeroViaje.Viajero.Direccion.Estado) && (viajeroViaje.Viajero.Direccion.TelefonoMovil != 0) &&
                !string.IsNullOrEmpty(viajeroViaje.Viajes.Codigo) && (viajeroViaje.Viajes.Plazas != 0) && (viajeroViaje.Viajes.IdOrigen != 0) && (viajeroViaje.Viajes.IdDestino != 0) && (viajeroViaje.Viajes.Precio != 0) &&
                (viajeroViaje.Viajes.FechaSalida != null) && (viajeroViaje.Viajes.HoraSalida != null) && (viajeroViaje.Viajes.HoraLLegada != null))
            {

                try
                {
                    using (IDbConnection connection = new SqlConnection(Helper.GetConnectionString()))
                    {
                        if (viajeroViaje == null)
                        {
                            return Content(System.Net.HttpStatusCode.BadRequest, "La Entrada de datos es inválida ");
                        }
                        else
                        {
                            string parametros = "@Cedula, @Nombre, @Direccion1, @Direccion2, @Ciudad, @Estado, @TelefonoMovil, @TelefonoFijo, @Codigo, @Plazas, @IdOrigen, @IdDestino,@Precio,@FechaSalida, @HoraSalida,@HoraLLegada";
                            var output = connection.Execute("sp_Insert_ViajeroDireccionViaje " + parametros,
                                                                                                            new
                                                                                                            {
                                                                                                                Cedula = viajeroViaje.Viajero.Cedula,
                                                                                                                Nombre = viajeroViaje.Viajero.Nombre,
                                                                                                                Direccion1 = viajeroViaje.Viajero.Direccion.Direccion1,
                                                                                                                Direccion2 = viajeroViaje.Viajero.Direccion.Direccion2,
                                                                                                                Ciudad = viajeroViaje.Viajero.Direccion.Ciudad,
                                                                                                                Estado = viajeroViaje.Viajero.Direccion.Estado,
                                                                                                                TelefonoMovil = viajeroViaje.Viajero.Direccion.TelefonoMovil,
                                                                                                                TelefonoFijo = viajeroViaje.Viajero.Direccion.TelefonoFijo,
                                                                                                                Codigo = viajeroViaje.Viajes.Codigo,
                                                                                                                Plazas = viajeroViaje.Viajes.Plazas,
                                                                                                                IdOrigen = viajeroViaje.Viajes.IdOrigen,
                                                                                                                IdDestino = viajeroViaje.Viajes.IdDestino,
                                                                                                                Precio = viajeroViaje.Viajes.Precio,
                                                                                                                FechaSalida = viajeroViaje.Viajes.FechaSalida,
                                                                                                                HoraSalida = viajeroViaje.Viajes.HoraSalida,
                                                                                                                HoraLLegada = viajeroViaje.Viajes.HoraLLegada
                                                                                                            });
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
        [System.Web.Http.Route("api/Viajero_Viajes/DeleteViajero2")]
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

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Viajero_Viajes/UpdateViajeroViajes")]
        [ResponseType(typeof(Viajero))]
        [EnableCorsAttribute("*", "*", "*")]
        public IHttpActionResult UpdateViajeroViajes(ViajeroViaje viajeroViaje)
        {
            if (!string.IsNullOrEmpty(viajeroViaje.Viajero.Nombre) && (viajeroViaje.Viajero.Cedula != 0) && !string.IsNullOrEmpty(viajeroViaje.Viajero.Direccion.Direccion1) &&
               !string.IsNullOrEmpty(viajeroViaje.Viajero.Direccion.Ciudad) && !string.IsNullOrEmpty(viajeroViaje.Viajero.Direccion.Estado) && (viajeroViaje.Viajero.Direccion.TelefonoMovil != 0) &&
               !string.IsNullOrEmpty(viajeroViaje.Viajes.Codigo) && (viajeroViaje.Viajes.Plazas != 0) && (viajeroViaje.Viajes.IdOrigen != 0) && (viajeroViaje.Viajes.IdDestino != 0) && (viajeroViaje.Viajes.Precio != 0) &&
               (viajeroViaje.Viajes.FechaSalida != null) && (viajeroViaje.Viajes.HoraSalida != null) && (viajeroViaje.Viajes.HoraLLegada != null) && (viajeroViaje.Viajero.IdViajero != 0) && (viajeroViaje.Viajes.IdViaje != 0))
            {
                try
                {
                    using (IDbConnection connection = new SqlConnection(Helper.GetConnectionString()))
                    {
                        if (viajeroViaje == null)
                        {
                            return Content(System.Net.HttpStatusCode.BadRequest, "La Entrada de datos es inválida ");
                        }
                        else
                        {
                            string parametros = "@IdViajero, @Cedula, @Nombre, @Direccion1, @Direccion2, @Ciudad, @Estado, @TelefonoMovil, @TelefonoFijo, @IdViaje ,@Codigo, @Plazas, @IdOrigen, @IdDestino,@Precio,@FechaSalida, @HoraSalida,@HoraLLegada";
                            var output = connection.Execute("sp_UpdateViajeroDireccionViaje " + parametros,
                                                                                                            new
                                                                                                            {
                                                                                                                IdViajero = viajeroViaje.Viajero.IdViajero,
                                                                                                                Cedula = viajeroViaje.Viajero.Cedula,
                                                                                                                Nombre = viajeroViaje.Viajero.Nombre,
                                                                                                                Direccion1 = viajeroViaje.Viajero.Direccion.Direccion1,
                                                                                                                Direccion2 = viajeroViaje.Viajero.Direccion.Direccion2,
                                                                                                                Ciudad = viajeroViaje.Viajero.Direccion.Ciudad,
                                                                                                                Estado = viajeroViaje.Viajero.Direccion.Estado,
                                                                                                                TelefonoMovil = viajeroViaje.Viajero.Direccion.TelefonoMovil,
                                                                                                                TelefonoFijo = viajeroViaje.Viajero.Direccion.TelefonoFijo,
                                                                                                                IdViaje = viajeroViaje.Viajes.IdViaje,
                                                                                                                Codigo = viajeroViaje.Viajes.Codigo,
                                                                                                                Plazas = viajeroViaje.Viajes.Plazas,
                                                                                                                IdOrigen = viajeroViaje.Viajes.IdOrigen,
                                                                                                                IdDestino = viajeroViaje.Viajes.IdDestino,
                                                                                                                Precio = viajeroViaje.Viajes.Precio,
                                                                                                                FechaSalida = viajeroViaje.Viajes.FechaSalida,
                                                                                                                HoraSalida = viajeroViaje.Viajes.HoraSalida,
                                                                                                                HoraLLegada = viajeroViaje.Viajes.HoraLLegada
                                                                                                            });
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
    }
}
