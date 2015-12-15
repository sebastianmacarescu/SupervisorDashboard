using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupervisorDashboard.models
{
    [Table("space")]
    public partial class Space
    {
        [Key]
        public Guid guid { get; set; }
    }
}
