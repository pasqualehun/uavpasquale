using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Instruments : Form
    {
		public const int MAX_POINTS = 10;

		Decode_serial decoder= new Decode_serial();

		const int SIZE = 75;

		static byte[] receivedBytesA = new byte[SIZE];
		static byte[] receivedBytesB = new byte[SIZE];

		static double[] decodedFromA = new double[77];
		static double[] decodedFromB = new double[77];

		static DataElement[] elements = new DataElement[77];

		static String[] names = { "idő", "idő", "phi", "theta", "psi", "angvel x", "angvel y", "angvel z", "acc x", "acc y", "acc z", "vn", "ve", "vd", "lat", "lon", "alt", "control_cm.da", "control_meas.da", "control_meas.current[0]", "dummy", "dummy", "control_cm.de", "control_meas.de", "control_meas.current[1]", "dummy", "dummy", "control_cm.dr", "control_meas.dr", "control_meas.current[2]", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "control_cm.dthr", "control_meas.dthr", "dummy", "dummy", "dummy", "control_cm.dthr", "control_meas.dthr", "dummy", "dummy", "dummy", "is_master", "error_imu_gps_ahrs", "error_imu_gps_ahrs", "error_nav", "error_control", "error_cpu", "error_other_fcc", "checksum", "Speed", "HDG" };

		static object lockObj = new object();

		


        public Instruments()
        {
			InitializeComponent();
            

			// Nice methods to browse all available ports:
			string[] ports = SerialPort.GetPortNames();

			// Add all port names to the combo box:
			foreach (string port in ports)
			{
				comboBox1.Items.Add(port);
                comboBox2.Items.Add(port);
			}
			comboBox1.Items.Add("Close");
			comboBox2.Items.Add("Close");

			for (int i = 0; i < elements.Length; i++)
			{
				elements[i] = new DataElement(names[i]);
			}

        }
        
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
        
        
            if(serialPort1.IsOpen)
				try
				{
					(sender as System.IO.Ports.SerialPort).Read(receivedBytesA, 0, SIZE);
				}
				catch (Exception)
				{
					serialPort1.Close();
					MessageBox.Show("PortA bezárva", "Soros port hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				
				}
                                
			decodedFromA = decoder.Decode(receivedBytesA);

			lock (lockObj)
			{
				for (int i = 0; i < decodedFromA.Length; i++)
				{
					elements[i].AddA(decodedFromA[i]);
					elements[i].Calculate();
				}
			}
        }

		private void serialPort2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{


			if (serialPort2.IsOpen)
				try
				{
					(sender as System.IO.Ports.SerialPort).Read(receivedBytesB, 0, SIZE);
				}
				catch (Exception)
				{
					serialPort2.Close();
					MessageBox.Show("PortB bezárva", "Soros port hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}

			decodedFromB = decoder.Decode(receivedBytesB);

			lock (lockObj)
			{
				for (int i = 0; i < decodedFromB.Length; i++)
				{
					elements[i].AddB(decodedFromB[i]);
					elements[i].Calculate();
				}
			}

		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (serialPort1.IsOpen)
				serialPort1.Close();
			if (comboBox1.SelectedItem.ToString() != "Close")
				serialPort1.PortName = comboBox1.SelectedItem.ToString();
			else
				return;


			// try to open the selected port:
			try
			{
				serialPort1.Open();
			}
			// give a message, if the port is not available:
			catch
			{
				MessageBox.Show("A " + serialPort1.PortName + "-t nem lehet megnyitni az A-n ", "Soros port hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				comboBox1.SelectedText = "";
			}
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (serialPort2.IsOpen)
				serialPort2.Close();
			serialPort2.PortName = comboBox2.SelectedItem.ToString();


			// try to open the selected port:
			try
			{
				serialPort2.Open();
			}
			// give a message, if the port is not available:
			catch
			{
				MessageBox.Show("A " + serialPort2.PortName + "-t nem lehet megnyitni a B-n ", "Soros port hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				comboBox1.SelectedText = "";
			}
		}


		public void UpdateView()
		{
			while (true)
			{
				Thread.Sleep(1000);

				if (serialPort1.IsOpen && decodedFromA[14] != 0)//&& serialPort2.IsOpen)
				{
					AddCoordinate(new PointLatLng(decodedFromA[14], decodedFromA[15]));


					view1.Update(decodedFromA,decodedFromB);

					view1.Update(ref elements);

					//speed1.UpdateSpeed((float)decodedFromA[75]);

					speed1.UpdateSpeed((float)elements[75].GetData());

					vario1.UpdateClimb((float)decodedFromA[13]);
					altimeter1.UpdateAlt((float)decodedFromA[16]);
					compass1.UpdateHeading((float)decodedFromA[76]);
				

					vario1.Invalidate();

					altimeter1.Invalidate();
					speed1.Invalidate();
					compass1.Invalidate();
					view1.Invalidate();
				}
			}
		}






		static List<PointLatLng> points = new List<PointLatLng>();
		
		static GMapRoute flightRoute = new GMapRoute("");

		static GMapMarkerImage planeMarker;

		GMapOverlay planeMarkerOverlay = new GMapOverlay("markers");

		private void gMapControl1_Load(object sender, EventArgs e)
		{
			gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
			gmap.CacheLocation="map";
			gmap.DragButton = MouseButtons.Left;

			GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
			//GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.CacheOnly;

			gmap.SetPositionByKeywords("Budapest, Hungary");
			gmap.Position = new PointLatLng(47.471154, 19.062481);

			

			Bitmap btm = new Bitmap("png1t.png");
			if (btm == null)
				throw new NoNullAllowedException("as");

			planeMarker = new GMapMarkerImage(new PointLatLng(47.471154, 19.062481), btm);
			planeMarkerOverlay.Markers.Add(planeMarker);
			gmap.Overlays.Add(planeMarkerOverlay);


			GMapOverlay flightRouteOverlay = new GMapOverlay("polygons");			
	
			flightRoute.Stroke = new Pen(Color.Red, 2);
			flightRouteOverlay.Routes.Add(flightRoute);
			gmap.Overlays.Add(flightRouteOverlay);

			GMapOverlay routeOverlay2 = new GMapOverlay("route2");

			//gmap.Overlays.Add(markerOverlay);

			flightRouteOverlay.Routes.Add(flightRoute);

			routeOverlay2.Routes.Add(plannedRoute);

			gmap.Overlays.Add(routeOverlay2);




			points.Add(new PointLatLng(47.471154, 19.062481));
			points.Add(new PointLatLng(47.473214, 19.059091));
			points.Add(new PointLatLng(47.473925, 19.059219));
			points.Add(new PointLatLng(47.474505, 19.05952));
			points.Add(new PointLatLng(47.47436, 19.058876));
			points.Add(new PointLatLng(47.47449, 19.058533));
			points.Add(new PointLatLng(47.47449, 19.058533));
			points.Add(new PointLatLng(47.475172, 19.056151));
			points.Add(new PointLatLng(47.474867, 19.055378));
			points.Add(new PointLatLng(47.474273, 19.054241));
			points.Add(new PointLatLng(47.473939, 19.053512));
			points.Add(new PointLatLng(47.473606, 19.052761));
			points.Add(new PointLatLng(47.473142, 19.052417));
			points.Add(new PointLatLng(47.472764, 19.052482));
			points.Add(new PointLatLng(47.470792, 19.052932));
			points.Add(new PointLatLng(47.471154, 19.053512));
			points.Add(new PointLatLng(47.472387, 19.055293));
			points.Add(new PointLatLng(47.473258, 19.056516));
			points.Add(new PointLatLng(47.473838, 19.056988));


			
		}



		int aa = 0;	
		void AddCoordinate(PointLatLng a)
		{
			if (aa > 10)
				aa = 0;
			//flightRoute.Points.Add(points.ElementAt(aa));
			flightRoute.Points.Add(a);

			gmap.UpdateRouteLocalPosition(flightRoute);

			//planeMarker.Position = points.ElementAt(aa);
			planeMarker.Position = a;

			gmap.Invalidate();
			aa++;
		}

		public class GMapMarkerImage : GMap.NET.WindowsForms.GMapMarker
		{
			private Image img;


			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="p">The position of the marker</param>
			public GMapMarkerImage(PointLatLng p, Image image)
				: base(p)
			{
				img = image;
				Size = new Size(30, 30);
				Offset = new System.Drawing.Point(-15, -15);
				//img = image;
				//Size = img.Size;
				//Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2);
			}

			public override void OnRender(Graphics g)
			{
											
				
				g.DrawImage(RotateImage(img, (float)decodedFromA[76]), LocalPosition.X, LocalPosition.Y, Size.Height, Size.Width);
				//g.DrawImage(img, LocalPosition.X, LocalPosition.Y, Size.Height, Size.Width);			
				
				base.OnRender(g);
			}
		}

		public static Image RotateImage(Image img, float rotationAngle)
		{
			//create an empty Bitmap image
			Bitmap bmp = new Bitmap(img.Width, img.Height);

			//turn the Bitmap into a Graphics object
			Graphics gfx = Graphics.FromImage(bmp);

			//now we set the rotation point to the center of our image
			gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

			//now rotate the image
			gfx.RotateTransform(rotationAngle);

			gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);


			//now draw our new image onto the graphics object
			gfx.DrawImage(img, new Point(0, 0));

			//dispose of our Graphics object
			gfx.Dispose();

			//return the image
			return bmp;
		}

		private void gmap_plan_Load(object sender, EventArgs e)
		{
			gmap_plan.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
			gmap_plan.CacheLocation = "map";
			gmap_plan.DragButton = MouseButtons.Left;

			//GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
			GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.CacheOnly;

			gmap_plan.SetPositionByKeywords("Budapest, Hungary");
			gmap_plan.Position = new PointLatLng(47.471154, 19.062481);

			plannedRoute.Stroke = new Pen(Color.MediumBlue, 2);

			gmap_plan.Overlays.Add(markerOverlay);
			gmap_plan.Overlays.Add(plannedRouteOverlay);

			plannedRouteOverlay.Routes.Add(plannedRoute);

			calculation2.AddReferencesToPlanningCalculation(ref plannedRoute, gmap_plan);
		}

		int numberOfPoints = 1;
		GMapOverlay markerOverlay = new GMapOverlay("markers");
		GMapOverlay plannedRouteOverlay = new GMapOverlay("route");

		GMapRoute plannedRoute = new GMapRoute("");
		private GMapMarker currentMarker;
		private bool isDraggingMarker;



		
		private void gmap_plan_MouseDoubleClick(object sender, MouseEventArgs e)
		{

			if (numberOfPoints <= MAX_POINTS)
			{
				PointLatLng currentPos = new PointLatLng(gmap_plan.FromLocalToLatLng(e.X, e.Y).Lat, gmap_plan.FromLocalToLatLng(e.X, e.Y).Lng);
				GMarkerGoogle marker = new GMarkerGoogle(currentPos, GMarkerGoogleType.blue_small);

				marker.ToolTipText = (numberOfPoints++).ToString();

				planeMarkerOverlay.Markers.Add(marker);
				markerOverlay.Markers.Add(marker);

				plannedRoute.Points.Add(currentPos);

				calculation2.Invalidate();

				gmap_plan.UpdateRouteLocalPosition(plannedRoute);
			}
		}

		private void calculation2_Load(object sender, EventArgs e)
		{

		}

		int indexOfCurrentPoint;

		private void gmap_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			AddCoordinate(new PointLatLng(47.473838, 19.056988));
		}

		private void gmap_plan_OnMarkerEnter(GMapMarker item)
		{
			if (!isDraggingMarker)
			{
				currentMarker = item; 
				indexOfCurrentPoint = plannedRoute.Points.FindIndex(point => point == currentMarker.Position);
			}
		}

		private void gmap_plan_MouseUp(object sender, MouseEventArgs e)
		{
			isDraggingMarker = false;
			currentMarker = null;
		}

	

		private void gmap_plan_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (currentMarker != null && currentMarker.IsMouseOver)
				{
					isDraggingMarker = true;				
				}
			}
		}

		private void gmap_plan_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && isDraggingMarker && currentMarker != null)
			{
				currentMarker.Position = gmap_plan.FromLocalToLatLng(e.X, e.Y);
				
				PointLatLng newPos = gmap_plan.FromLocalToLatLng(e.X, e.Y);
				
				plannedRoute.Points.RemoveAt(indexOfCurrentPoint);
				plannedRoute.Points.Insert(indexOfCurrentPoint, newPos);

				gmap_plan.UpdateRouteLocalPosition(plannedRoute);
				gmap_plan.Refresh();
				calculation2.Invalidate();

			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Console.WriteLine(plannedRoute.Points.Count+"-------------------");
			foreach (var item in plannedRoute.Points)
			{
				Console.WriteLine(item.ToString());
				
			}
		}
    }
}
