using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupervisorDashboard
{
    class Floor
    {
        public string name { get; set; }
        public Bitmap bmp { get; set; }
        public Color color { get; set; }

        // Each polygon is represented by a List<Point>.
        public List<Poly> polygons = new List<Poly>();

        public List<FloorSensor> sensors = new List<FloorSensor>();

        public Floor()
        {
            color = Helpers.RandomColor.GetRandomColor();
        }
    }
}
