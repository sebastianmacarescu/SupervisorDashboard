using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupervisorDashboard
{
    class FloorSensor
    {
        public string name { get; set; }
        public Guid guid { get; set; }
        public string provider { get; set; }
        public Type type { get; set; }

        public int lowerSafeValue { get; set; }
        public int upperSafeValue { get; set; }

        public Bitmap Image { get { return getImageByType(type); } }

        //Sensor location within floor
        public Point location;

        public enum Type{
            TEMPERATURE,
            PRESSURE,
            METHANE,
            GENERIC
        };

        public static Bitmap getImageByType(Type type)
        {
            switch (type)
            {
                case Type.TEMPERATURE: return Properties.Resources.temp_sensor;
                case Type.PRESSURE: return Properties.Resources.pressure_sensor;
                case Type.METHANE: return Properties.Resources.gas_sensor;
                default: return Properties.Resources.generic_sensor;
            }
        }

    }
}
