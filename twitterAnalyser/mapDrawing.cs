using GMap.NET;
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

namespace twitterAnalyser
{
    public partial class mapDrawing : Form
    {
        public string file = "";
        public mapDrawing()
        {
            InitializeComponent();
        }

        private void mapDrawing_Load(object sender, EventArgs e)
        {

        }

        public void Drawing()
        {
            coordsParse cp = new coordsParse();
            GMapOverlay polyOverlay = new GMapOverlay("States");
            Dictionary<string, List<GMapPolygon>> polygons = cp.Polygons();

            Dictionary<GMapPolygon, double> polygonsMood = cp.averageMoodst(file);
            double scaleColor = (polygonsMood.Values.Max() - polygonsMood.Values.Min()) / 255;

            foreach (var str in polygonsMood)
            {
                GMapPolygon polygon = str.Key;
                polygon.Fill = new SolidBrush(Color.FromArgb(Convert.ToInt32((str.Value - polygonsMood.Values.Min()) / scaleColor), randomColor()));
                polygon.Stroke = new Pen(Color.Black, 3);
                polyOverlay.Polygons.Add(polygon);
            }
            map.Overlays.Add(polyOverlay);
        }
        Color randomColor()
        {
            Random rand = new Random();
            int r, g, b;
            r = rand.Next(0, 255);
            g = rand.Next(0, 255);
            b = rand.Next(0, 255);
            return Color.FromArgb(r, g, b);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            map.MapProvider = GMapProviders.BingMap;
            map.DragButton = MouseButtons.Left;
            map.Position = new PointLatLng(40, -100);
            map.MinZoom = 4;
            map.MaxZoom = 200;
            map.Zoom = 4;
            Drawing();
        }
    }
}