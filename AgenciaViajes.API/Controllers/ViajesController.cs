using AgenciaViajes.API.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace AgenciaViajes.API.Controllers
{
    public class ViajesController : ApiController
    {
        // GET: Viajes
        [System.Web.Http.HttpGet]
        [EnableCorsAttribute("*", "*", "*")]
        public IHttpActionResult GetViajesByIdViaje(int? IdViaje)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(Helper.GetConnectionString()))
                {
                    List<Viajes> Listaviajes = null;
                    List<Viajes> viajes = new List<Viajes>(); ;
                    Viajes viaje = null;
                    if (IdViaje == null)
                    {
                        Listaviajes = connection.Query<Viajes>("sp_GetViajesByIdViaje @IdViaje", new { IdViaje = IdViaje }).ToList();
                        if (Listaviajes == null)
                        {
                            return Content(System.Net.HttpStatusCode.NotFound, "Los viajes no fueron encontrados");
                        }
                        foreach (var viajesItem in Listaviajes)
                        {
                            //
                            //Obtiene las ciudades origen y destino
                            var lugares = connection.Query<Ciudades>("sp_GetCiudadByIdCiudad @IdCiudadOrigen, @IdCiudadDestino", new { IdCiudadOrigen = viajesItem.IdOrigen, IdCiudadDestino = viajesItem.IdDestino }).ToList();
                            //Asigna los respectivos lugares de origen y destino
                            foreach (var lugar in lugares)
                            {
                                switch (lugar.Lugar)
                                {
                                    case "origen":
                                        viajesItem.Origen = lugar.Ciudad;
                                        break;
                                    case "destino":
                                        viajesItem.Destino = lugar.Ciudad;
                                        break;
                                }
                            }
                            //
                            var Viajero = connection.Query<Viajero>("sp_GetViajerosByIdViajero @IdViajero", new { IdViajero = viajesItem.IdViajero }).FirstOrDefault();
                            viajesItem.Viajero = Viajero;
                            viajes.Add(viajesItem);
                        }
                        return Ok(viajes);
                    }
                    else
                    {
                        //Obtiene el viaje por el id
                        viaje = connection.Query<Viajes>("sp_GetViajesByIdViaje @IdViaje", new { IdViaje = IdViaje }).FirstOrDefault();
                        if (viaje == null)
                        {
                            return Content(System.Net.HttpStatusCode.NotFound, "El viaje no fue encontrado");
                        }
                        //Obtiene las ciudades origen y destino
                        var lugares = connection.Query<Ciudades>("sp_GetCiudadByIdCiudad @IdCiudadOrigen, @IdCiudadDestino", new { IdCiudadOrigen = viaje.IdOrigen, IdCiudadDestino = viaje.IdDestino }).ToList();
                        //Asigna los respectivos lugares de origen y destino
                        foreach(var lugar in lugares)
                        {
                            switch(lugar.Lugar)
                            {
                                case "origen":
                                    viaje.Origen = lugar.Ciudad;
                                    break;
                                case "destino":
                                    viaje.Destino = lugar.Ciudad;
                                    break;
                            }
                        }
                        var viajero = connection.Query<Viajero>("sp_GetViajerosByIdViajero @IdViajero", new { IdViajero = viaje.IdViajero }).FirstOrDefault();
                        viaje.Viajero = viajero;
                        return Ok(viaje);
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Viajes/InsertViaje")]
        [ResponseType(typeof(Viajes))]
        [EnableCorsAttribute("*", "*", "*")]
        public IHttpActionResult InsertViaje(Viajes viajes)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(Helper.GetConnectionString()))
                {
                    if (viajes == null)
                    {
                        return Content(System.Net.HttpStatusCode.BadRequest, "La Entrada de datos es inválida ");
                    }
                    else
                    {
                        string parametros = "@Codigo, @Plazas, @IdOrigen, @IdDestino, @Precio, @IdViajero, @FechaSalida, @HoraSalida, @HoraLLegada";
                        var output = connection.Execute("sp_InsertarViajes " + parametros, new { Codigo = viajes.Codigo, Plazas = viajes.Plazas, IdOrigen = viajes.IdOrigen, IdDestino = viajes.IdDestino, Precio = viajes.Precio, IdViajero = viajes.IdViajero, FechaSalida = viajes.FechaSalida, HoraSalida = viajes.HoraSalida, HoraLLegada = viajes.HoraLLegada });
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }

            return Ok();
        }


        [System.Web.Http.HttpPost, System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Viajes/UpdateViajes2")]
        [ResponseType(typeof(Viajes))]
        [EnableCorsAttribute("*", "*", "*")]
        public IHttpActionResult UpdateViajes(Viajes viajes, int? IdViaje)
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";
            culture.DateTimeFormat.LongTimePattern = "";
            Thread.CurrentThread.CurrentCulture = culture;

            
            try
            {
                using (IDbConnection connection = new SqlConnection(Helper.GetConnectionString()))
                {
                    if (viajes == null)
                    {
                        return Content(System.Net.HttpStatusCode.BadRequest, "La Entrada de datos es inválida ");
                    }
                    else
                    {
                        var output = connection.Execute($"sp_ActualizarViajes @IdViaje = {IdViaje}, @Nombre = '{viajes.Viajero.Nombre}',  @Codigo = '{viajes.Codigo}', @Plazas = '{viajes.Plazas}', @IdOrigen = {viajes.IdOrigen}, @IdDestino = {viajes.IdDestino}, @Precio = {viajes.Precio}, @IdViajero = {viajes.IdViajero}, @FechaSalida = '{viajes.FechaSalida.ToString("yyyy-MM-dd")}', @HoraSalida = '{viajes.HoraSalida}', @HoraLLegada = '{viajes.HoraLLegada}' ");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
        }

        //Delete
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Viajes/DeleteViajes2")]
        [ResponseType(typeof(Viajes))]
        [EnableCorsAttribute("*", "*", "*")]
        public IHttpActionResult DeleteViajes(int? IdViaje)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(Helper.GetConnectionString()))
                {
                    if (IdViaje == null)
                    {
                        return Content(System.Net.HttpStatusCode.BadRequest, "La Entrada de datos es inválida ");
                    }
                    else
                    {
                        var output = connection.Execute($"sp_EliminarViajes  @IdViaje = {IdViaje}");
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
