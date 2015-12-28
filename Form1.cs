using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Drawing.Drawing2D;

namespace SupervisorDashboard
{
    public partial class Form1 : Form
    {
        List<Floor> floors = new List<Floor>();
        List<RadioButton> radios = new List<RadioButton>();

        Floor currentFloor = null;
        RadioButton currentRadio = null;
        Poly newPolygon = null;

        FloorSensor currentSensor = null;
        FloorSensor movingSensor = null;
        FloorSensor.Type currentSensorType = FloorSensor.Type.GENERIC;

        // The current mouse position while drawing a new polygon.
        Point newPoint;

        // The polygon and index of the corner we are moving.
        Poly movingPolygon = null;
        int movingPoint = -1;
        int offsetX, offsetY;

        // The add point cursor.
        private Cursor addPointCursor;
        private Helpers.ColorGenerator colorGenerator = new Helpers.ColorGenerator();

        public Form1()
        {
            addPointCursor = new Cursor(Properties.Resources.add_point.GetHicon());
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        // The grid spacing.
        private const int gridGap = 8;

        // Snap to the nearest grid point.
        private Point SnapToGrid(Point point)
        {
            int x = gridGap * (int)Math.Round((float)point.X / gridGap);
            int y = gridGap * (int)Math.Round((float)point.Y / gridGap);
            return new Point(x, y);
        }

        private Bitmap getBackgroundGrid()
        {
            Bitmap bm = new Bitmap(
                gridBox.ClientSize.Width,
                gridBox.ClientSize.Height);
            for (int x = 0; x < gridBox.ClientSize.Width; x += gridGap)
            {
                for (int y = 0; y < gridBox.ClientSize.Height; y += gridGap)
                {
                    bm.SetPixel(x, y, Color.Black);
                }
            }
            return bm;
        }

        private void addFloorBtn_Click(object sender, EventArgs e)
        {
            //Ask for floorName
            string floorName = Interaction.InputBox("Input the floor name", "Add new floor", "Floor " + floors.Count);
            if(floorName.Length == 0)
            {
                Interaction.MsgBox("Invalid name");
                return;
            }

            Floor floor = new Floor {
                name = floorName,
                bmp = getBackgroundGrid(),
                color = colorGenerator.NextColor()
            };

            //Create the radio button
            RadioButton radio = new RadioButton();
            radio.Text = floorName;
            radio.ForeColor = floor.color;
            radio.Tag = floor;
            radio.Checked = true;
            radio.Location = new Point(5, floors.Count * 4);
            radio.CheckedChanged += radioFloors_CheckedChanged;
            flowLayoutRadio.Controls.Add(radio);

            //Uncheck the previous radio
            if (currentRadio != null)
                currentRadio.Checked = false;

            radios.Add(radio);
            floors.Add(floor);

            //Set the grid
            gridBox.BackgroundImage = floor.bmp;
            currentRadio = radio;
            currentFloor = floor;
        }

        private void radioFloors_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                currentFloor = radio.Tag as Floor;
                currentRadio = radio;
                gridBox.BackgroundImage = currentFloor.bmp;
            }
        }

        private void superposeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            gridBox.Invalidate();
        }

        #region gridBoxListeners

        // Start or continue drawing a new polygon / start moving a corner or polygon.
        // Or Place sensor
        private void gridBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Return if no floor created
            if (currentFloor == null)
                return;
            
            // See what we're over.
            Point mouse_pt = SnapToGrid(e.Location);
            Poly hit_polygon;
            int hit_point, hit_point2;
            Point closest_point;

            if (drawPolyRadio.Checked)
            {
                if (newPolygon != null)
                {
                    // We are already drawing a polygon.
                    // If it's the right mouse button, finish this polygon.
                    if (e.Button == MouseButtons.Right)
                    {
                        // Finish this polygon.
                        if (newPolygon.vertices.Count > 2) currentFloor.polygons.Add(newPolygon);
                        newPolygon = null;

                        // We no longer are drawing.
                        gridBox.MouseMove += gridBox_MouseMove_NotDrawing;
                        gridBox.MouseMove -= gridBox_MouseMove_Drawing;
                    }
                    else
                    {
                        // Add a point to this polygon.
                        if (newPolygon.vertices[newPolygon.vertices.Count - 1] != mouse_pt)
                        {
                            newPolygon.vertices.Add(mouse_pt);
                        }
                    }
                }
                else if (MouseIsOverCornerPoint(mouse_pt, out hit_polygon, out hit_point))
                {
                    // Start dragging this corner.
                    gridBox.MouseMove -= gridBox_MouseMove_NotDrawing;
                    gridBox.MouseMove += gridBox_MouseMove_MovingCorner;
                    gridBox.MouseUp += gridBox_MouseUp_MovingCorner;

                    // Remember the polygon and point number.
                    movingPolygon = hit_polygon;
                    movingPoint = hit_point;

                    // Remember the offset from the mouse to the point.
                    offsetX = hit_polygon[hit_point].X - e.X;
                    offsetY = hit_polygon[hit_point].Y - e.Y;
                }
                else if (MouseIsOverEdge(mouse_pt, out hit_polygon,
                    out hit_point, out hit_point2, out closest_point))
                {
                    // Add a point.
                    hit_polygon.vertices.Insert(hit_point + 1, closest_point);
                }
                else if (MouseIsOverPolygon(mouse_pt, out hit_polygon))
                {
                    // Start moving this polygon.
                    gridBox.MouseMove -= gridBox_MouseMove_NotDrawing;
                    gridBox.MouseMove += gridBox_MouseMove_MovingPolygon;
                    gridBox.MouseUp += gridBox_MouseUp_MovingPolygon;

                    // Remember the polygon.
                    movingPolygon = hit_polygon;

                    // Remember the offset from the mouse to the segment's first point.
                    offsetX = hit_polygon[0].X - e.X;
                    offsetY = hit_polygon[0].Y - e.Y;
                }
                else
                {
                    // Start a new polygon.
                    newPolygon = new Poly();
                    newPoint = mouse_pt;
                    newPolygon.AddVertex(mouse_pt);

                    // Get ready to work on the new polygon.
                    gridBox.MouseMove -= gridBox_MouseMove_NotDrawing;
                    gridBox.MouseMove += gridBox_MouseMove_Drawing;
                }

                // Redraw.
                gridBox.Invalidate();
            }
            else if (placeSensorRadio.Checked)
            {
                Point correctLocation = new Point
                {
                    X = e.Location.X - FloorSensor.getImageByType(currentSensorType).Width / 2 + 2,
                    Y = e.Location.Y - FloorSensor.getImageByType(currentSensorType).Height / 2 + 2
                };
                FloorSensor sensor;
                if (MouseIsOverSensor(e.Location, out sensor))
                {
                    // Start moving this sensor.
                    gridBox.MouseMove -= gridBox_MouseMove_NotDrawing;
                    gridBox.MouseMove += gridBox_MouseMove_MovingSensor;
                    gridBox.MouseUp += gridBox_MouseUp_MovingSensor;

                    // Remember the sensor.
                    movingSensor = sensor;

                    // Remember the offset from the mouse to the point.
                    offsetX = correctLocation.X - e.X;
                    offsetY = correctLocation.Y - e.Y;
                }
                else
                {
                    
                    currentSensor = new FloorSensor
                    {
                        name = sensorNameBox.Text,
                        guid = Guid.NewGuid(),
                        provider = providerNameBox.Text,
                        type = currentSensorType,
                        lowerSafeValue = (int)lowerNumeric.Value,
                        upperSafeValue = (int)upperNumeric.Value,
                        location = correctLocation
                    };
                    currentFloor.sensors.Add(currentSensor);
                }
                
                gridBox.Invalidate();
            }
        }

        // Move the next point in the new polygon.
        private void gridBox_MouseMove_Drawing(object sender, MouseEventArgs e)
        {
            newPoint = SnapToGrid(e.Location);
            gridBox.Invalidate();
        }

        // Move the selected corner.
        private void gridBox_MouseMove_MovingCorner(object sender, MouseEventArgs e)
        {
            // Move the point.
            movingPolygon[movingPoint] =
                SnapToGrid(new Point(e.X + offsetX, e.Y + offsetY));

            // Redraw.
            gridBox.Invalidate();
        }

        // Finish moving the selected corner.
        private void gridBox_MouseUp_MovingCorner(object sender, MouseEventArgs e)
        {
            gridBox.MouseMove += gridBox_MouseMove_NotDrawing;
            gridBox.MouseMove -= gridBox_MouseMove_MovingCorner;
            gridBox.MouseUp -= gridBox_MouseUp_MovingCorner;
        }

        // Move the selected polygon.
        private void gridBox_MouseMove_MovingPolygon(object sender, MouseEventArgs e)
        {
            // See how far the first point will move.
            int new_x1 = e.X + offsetX;
            int new_y1 = e.Y + offsetY;

            int dx = new_x1 - movingPolygon[0].X;
            int dy = new_y1 - movingPolygon[0].Y;

            // Snap the movement to a multiple of the grid distance.
            dx = gridGap * (int)(Math.Round((float)dx / gridGap));
            dy = gridGap * (int)(Math.Round((float)dy / gridGap));

            if (dx == 0 && dy == 0) return;

            // Move the polygon.
            for (int i = 0; i < movingPolygon.VertextCount; i++)
            {
                movingPolygon[i] = new Point(
                    movingPolygon[i].X + dx,
                    movingPolygon[i].Y + dy);
            }

            // Redraw.
            gridBox.Invalidate();
        }

        // Finish moving the selected polygon.
        private void gridBox_MouseUp_MovingPolygon(object sender, MouseEventArgs e)
        {
            gridBox.MouseMove += gridBox_MouseMove_NotDrawing;
            gridBox.MouseMove -= gridBox_MouseMove_MovingPolygon;
            gridBox.MouseUp -= gridBox_MouseUp_MovingPolygon;
        }

        // Move the selected sensor.
        private void gridBox_MouseMove_MovingSensor(object sender, MouseEventArgs e)
        {
            // See how far the first point will move.
            int new_x1 = e.X + offsetX;
            int new_y1 = e.Y + offsetY;

            int dx = new_x1 - movingSensor.location.X;
            int dy = new_y1 - movingSensor.location.Y;

            if (dx == 0 && dy == 0) return;
            movingSensor.location.X += dx;
            movingSensor.location.Y += dy;

            // Redraw.
            gridBox.Invalidate();
        }

        // Finish moving the selected sensor.
        private void gridBox_MouseUp_MovingSensor(object sender, MouseEventArgs e)
        {
            gridBox.MouseMove += gridBox_MouseMove_NotDrawing;
            gridBox.MouseMove -= gridBox_MouseMove_MovingSensor;
            gridBox.MouseUp -= gridBox_MouseUp_MovingSensor;
        }

        // See if we're over a polygon or corner point or sensor
        private void gridBox_MouseMove_NotDrawing(object sender, MouseEventArgs e)
        {
            // Return if no floor created
            if (currentFloor == null)
                return;

            Cursor new_cursor = Cursors.Cross;

            // See what we're over.
            Point mouse_pt = SnapToGrid(e.Location);
            Poly hit_polygon;
            FloorSensor hit_sensor;
            int hit_point, hit_point2;
            Point closest_point;

            if (placeSensorRadio.Checked)
            {
                if (MouseIsOverSensor(e.Location, out hit_sensor))
                    new_cursor = Cursors.Hand;
                else
                    new_cursor = new Cursor(FloorSensor.getImageByType(currentSensorType).GetHicon());
            }
            else if (MouseIsOverCornerPoint(mouse_pt, out hit_polygon, out hit_point))
            {
                new_cursor = Cursors.Arrow;
            }
            else if (MouseIsOverEdge(mouse_pt, out hit_polygon,
                out hit_point, out hit_point2, out closest_point))
            {
                new_cursor = addPointCursor;
            }
            else if (MouseIsOverPolygon(mouse_pt, out hit_polygon))
            {
                new_cursor = Cursors.Hand;
            }

            // Set the new cursor.
            if (gridBox.Cursor != new_cursor)
            {
                gridBox.Cursor = new_cursor;
            }
        }

        private void drawFloorPolygons(Floor floor, PaintEventArgs e, bool fill=true, Color? polyColor=null)
        {
            // Draw the old polygons.
            foreach (Poly polygon in floor.polygons)
            {
                // Draw the polygon.
                Pen pen = new Pen(polyColor ?? Color.Blue);
                if(fill)
                    e.Graphics.FillPolygon(Brushes.White, polygon.ToArray());
                e.Graphics.DrawPolygon(pen, polygon.ToArray());

                // Draw the corners.
                foreach (Point corner in polygon.vertices)
                {
                    Rectangle rect = new Rectangle(
                        corner.X - Poly.object_radius, corner.Y - Poly.object_radius,
                        2 * Poly.object_radius + 1, 2 * Poly.object_radius + 1);
                    e.Graphics.FillEllipse(Brushes.White, rect);
                    e.Graphics.DrawEllipse(Pens.Black, rect);
                }
            }

            //Draw sensors
            foreach(FloorSensor sensor in floor.sensors)
            {
                e.Graphics.DrawImage(sensor.Image, sensor.location);
            }
        }

        // Redraw old polygons in blue. Draw the new polygon in green.
        // Draw the final segment dashed.
        private void gridBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Return if no floor created
            if (currentFloor == null)
                return;

            //Draw old polygons first
            if (superposeCheckBox.Checked)
            {
                //Draw all floors first
                foreach(Floor floor in floors)
                    //Skip over currentFloor to avoid re-drawing same polys
                    if (floor != currentFloor)
                        drawFloorPolygons(floor, e, false, floor.color);
                drawFloorPolygons(currentFloor, e, false, currentFloor.color);
            }
            else
            {
                drawFloorPolygons(currentFloor, e, true, currentFloor.color);
            }

            // Draw the new polygon.
            if (newPolygon != null)
            {
                // Draw the new polygon.
                if (newPolygon.VertextCount > 1)
                {
                    e.Graphics.DrawLines(Pens.Green, newPolygon.ToArray());
                }

                // Draw the newest edge.
                if (newPolygon.VertextCount > 0)
                {
                    using (Pen dashed_pen = new Pen(Color.Green))
                    {
                        dashed_pen.DashPattern = new float[] { 3, 3 };
                        e.Graphics.DrawLine(dashed_pen,
                            newPolygon[newPolygon.VertextCount - 1],
                            newPoint);
                    }
                }
            }
        }

        private void gridBox_Resize(object sender, EventArgs e)
        {
            gridBox.BackgroundImage = getBackgroundGrid();
        }

        #endregion gridBoxListeners

        #region polygonMouseHelpers

        // See if the mouse is over a corner point.
        private bool MouseIsOverCornerPoint(Point mouse_pt, out Poly hit_polygon, out int hit_pt)
        {
            // See if we're over a corner point.
            foreach (Poly polygon in currentFloor.polygons)
            {
                // See if we're over one of the polygon's corner points.
                for (int i = 0; i < polygon.vertices.Count; i++)
                {
                    // See if we're over this point.
                    if (Poly.FindDistanceToPointSquared(polygon.vertices[i], mouse_pt) < Poly.over_dist_squared)
                    {
                        // We're over this point.
                        hit_polygon = polygon;
                        hit_pt = i;
                        return true;
                    }
                }
            }

            hit_polygon = null;
            hit_pt = -1;
            return false;
        }

        // See if the mouse is over a polygon's edge.
        private bool MouseIsOverEdge(Point mouse_pt, out Poly hit_polygon, out int hit_pt1, out int hit_pt2, out Point closest_point)
        {
            // Examine each polygon.
            // Examine them in reverse order to check the ones on top first.
            for (int pgon = currentFloor.polygons.Count - 1; pgon >= 0; pgon--)
            {
                Poly polygon = currentFloor.polygons[pgon];

                // See if we're over one of the polygon's segments.
                for (int p1 = 0; p1 < polygon.vertices.Count; p1++)
                {
                    // Get the index of the polygon's next point.
                    int p2 = (p1 + 1) % polygon.vertices.Count;

                    // See if we're over the segment between these points.
                    PointF closest;
                    if (Poly.FindDistanceToSegmentSquared(mouse_pt,
                        polygon.vertices[p1], polygon.vertices[p2], out closest) < Poly.over_dist_squared)
                    {
                        // We're over this segment.
                        hit_polygon = polygon;
                        hit_pt1 = p1;
                        hit_pt2 = p2;
                        closest_point = Point.Round(closest);
                        return true;
                    }
                }
            }

            hit_polygon = null;
            hit_pt1 = -1;
            hit_pt2 = -1;
            closest_point = new Point(0, 0);
            return false;
        }

        // See if the mouse is over a polygon's body.
        private bool MouseIsOverPolygon(Point mouse_pt, out Poly hit_polygon)
        {
            // Examine each polygon.
            // Examine them in reverse order to check the ones on top first.
            for (int i = currentFloor.polygons.Count - 1; i >= 0; i--)
            {
                // Make a GraphicsPath representing the polygon.
                GraphicsPath path = new GraphicsPath();
                path.AddPolygon(currentFloor.polygons[i].vertices.ToArray());

                // See if the point is inside the GraphicsPath.
                if (path.IsVisible(mouse_pt))
                {
                    hit_polygon = currentFloor.polygons[i];
                    return true;
                }
            }

            hit_polygon = null;
            return false;
        }

        // See if the mouse is over a polygon's body.
        private bool MouseIsOverSensor(Point mouse_pt, out FloorSensor hit_sensor)
        {
            // Examine each sensor.
            // Examine them in reverse order to check the ones on top first.
            for (int i = currentFloor.sensors.Count - 1; i >= 0; i--)
            {
                // Make a GraphicsPath representing the polygon.
                GraphicsPath path = new GraphicsPath();
                path.AddRectangle(new Rectangle(currentFloor.sensors[i].location, currentFloor.sensors[i].Image.Size));

                // See if the point is inside the GraphicsPath.
                if (path.IsVisible(mouse_pt))
                {
                    hit_sensor = currentFloor.sensors[i];
                    return true;
                }
            }

            hit_sensor = null;
            return false;
        }

        #endregion polygonMouseHelpers

        #region sensorListeners
        private void tempSensorRadio_CheckedChanged(object sender, EventArgs e)
        {
            currentSensorType = FloorSensor.Type.TEMPERATURE;
            sensorPictureBox.Image = Properties.Resources.temp_sensor;
        }

        private void pressureSensorRadio_CheckedChanged(object sender, EventArgs e)
        {
            currentSensorType = FloorSensor.Type.PRESSURE;
            sensorPictureBox.Image = Properties.Resources.pressure_sensor;
        }

        private void methaneSensorRadio_CheckedChanged(object sender, EventArgs e)
        {
            currentSensorType = FloorSensor.Type.METHANE;
            sensorPictureBox.Image = Properties.Resources.gas_sensor;
        }

        private void genericSensorRadio_CheckedChanged(object sender, EventArgs e)
        {
            currentSensorType = FloorSensor.Type.GENERIC;
            sensorPictureBox.Image = Properties.Resources.generic_sensor;
        }

        private void placeSensorRadio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;

            //Activate/deactivate sensor controls
            sensorConfGroup.Enabled = radio.Checked;
            sensorRadioGroup.Enabled = radio.Checked;
        }

        #endregion sensorListeners
    }
}
