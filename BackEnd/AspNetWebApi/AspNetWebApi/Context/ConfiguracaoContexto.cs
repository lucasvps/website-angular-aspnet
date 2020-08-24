using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace AspNetWebApi.Context
{
    public class ConfiguracaoContexto : DbMigrationsConfiguration<Contexto>
    {
        public ConfiguracaoContexto()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "AspNetWebApi";
        }
    }
}