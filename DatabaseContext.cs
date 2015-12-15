using SupervisorDashboard.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupervisorDashboard
{
    class DatabaseContext : DbContext
    {
        public DbSet<Space> SpaceSet { get; set; }
        public DbSet<Sensor> SensorSet { get; set; }
        public DbSet<SensorValue> SensorValueSet { get; set; }
       

        public DatabaseContext() : base("name=AzureAdoSqlConnectionString") { }
    }
}
