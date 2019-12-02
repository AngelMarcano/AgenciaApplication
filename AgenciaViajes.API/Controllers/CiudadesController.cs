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

namespace AgenciaViajes.API.Controllers
{
    public class CiudadesController : ApiController
    {
        [System.Web.Http.HttpGet]
        [EnableCorsAttribute("*", "*", "*")]
        public IHttpActionResult GetCiudadesById(int? IdCiudad)
        {
            using (IDbConnection connection = new SqlConnection(Helper.GetConnectionString()))
            {
                List<Ciudades> ListaCiudades = null;
                List<Ciudades> ciudades = new List<Ciudades>(); ;
                Ciudades ciudad = null;
                if (IdCiudad == null)
                {
                    // Obtiene todas las ciudades
                    ListaCiudades = connection.Query<Ciudades>("sp_GetCiudades @IdCiudad", new { IdCiudad = IdCiudad }).ToList();
                    if (ListaCiudades == null)
                    {
                        return Content(System.Net.HttpStatusCode.NotFound, "Las ciudades no fueron encontradas");
                    }

                    return Ok(ListaCiudades);
                }
                else
                {
                    //Obtiene la ciudad por el id
                    ciudad = connection.Query<Ciudades>("sp_GetCiudades @IdCiudad", new { IdCiudad = IdCiudad }).FirstOrDefault();
                    if (ciudad == null)
                    {
                        return Content(System.Net.HttpStatusCode.NotFound, "La ciudad no fue encontrada");
                    }

                    return Ok(ciudad);
                }
            }
        }
    }
}
