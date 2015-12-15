using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupervisorDashboard.models
{
    [Table("sensor")]
    class Sensor
    {
        [Key]
        public Guid id { get; set; }
        public string producer;
        public string type;
        public float latitue;
        public float longitude;
        public string status;
    }
}
