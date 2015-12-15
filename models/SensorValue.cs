using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupervisorDashboard.models
{
    [Table("sensor_value")]
    class SensorValue
    {
        [Key]
        public long id { get; set; }
        public float value { get; set; }
        public string jsonStringValue { get; set; }

        [ForeignKey("sensor")]
        public Guid sensor_id { get; set; }

        [JsonIgnore]
        public virtual Sensor sensor { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? creation_date_time { get; set; }
    }
}
