using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AgenciaViajes.API.Models
{
    public static class Helper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["AgenciaDB"].ConnectionString;
        }
    }
}