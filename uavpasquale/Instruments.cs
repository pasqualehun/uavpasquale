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
		Decode_serial decoder= new Decode_serial();


		static byte[] receivedBytesA = new byte[159];
		static byte[] receivedBytesB = new byte[159];

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
					(sender as System.IO.Ports.SerialPort).Read(receivedBytesA, 0, 159);
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
					(sender as System.IO.Ports.SerialPort).Read(receivedBytesB, 0, 159);
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

				if(serialPort1.IsOpen && serialPort2.IsOpen)
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
		
		static GMapRoute polygon = new GMapRoute("");

		static GMapMarkerImage marker;
		
		private void gMapControl1_Load(object sender, EventArgs e)
		{
			gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
			gmap.CacheLocation="map";
			gmap.DragButton = MouseButtons.Left;

			GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
			//GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.CacheOnly;

			gmap.SetPositionByKeywords("Budapest, Hungary");
			gmap.Position = new PointLatLng(47.471154, 19.062481);

			GMapOverlay markersOverlay = new GMapOverlay("markers");

			Bitmap btm = new Bitmap("png1t.png");
			if (btm == null)
				throw new NoNullAllowedException("as");

			marker = new GMapMarkerImage(new PointLatLng(47.471154, 19.062481), btm);
			markersOverlay.Markers.Add(marker);
			gmap.Overlays.Add(markersOverlay);


			GMapOverlay polyOverlay = new GMapOverlay("polygons");
			
	
			polygon.Stroke = new Pen(Color.Red, 2);
			polyOverlay.Routes.Add(polygon);
			gmap.Overlays.Add(polyOverlay);



			//points.Add(new PointLatLng(47.471154, 19.062481));
			//points.Add(new PointLatLng(47.473214, 19.059091));
			//points.Add(new PointLatLng(47.473925, 19.059219));
			//points.Add(new PointLatLng(47.474505, 19.05952));
			//points.Add(new PointLatLng(47.47436, 19.058876));
			//points.Add(new PointLatLng(47.47449, 19.058533));
			//points.Add(new PointLatLng(47.47449, 19.058533));
			//points.Add(new PointLatLng(47.475172, 19.056151));
			//points.Add(new PointLatLng(47.474867, 19.055378));
			//points.Add(new PointLatLng(47.474273, 19.054241));
			//points.Add(new PointLatLng(47.473939, 19.053512));
			//points.Add(new PointLatLng(47.473606, 19.052761));
			//points.Add(new PointLatLng(47.473142, 19.052417));
			//points.Add(new PointLatLng(47.472764, 19.052482));
			//points.Add(new PointLatLng(47.470792, 19.052932));
			//points.Add(new PointLatLng(47.471154, 19.053512));
			//points.Add(new PointLatLng(47.472387, 19.055293));
			//points.Add(new PointLatLng(47.473258, 19.056516));
			//points.Add(new PointLatLng(47.473838, 19.056988));


			
		}



		int aa = 0;	
		void AddCoordinate(PointLatLng a)
		{

			//polygon.Points.Add(points.ElementAt(aa));
			polygon.Points.Add(a);
			gmap.UpdateRouteLocalPosition(polygon);
			//marker.Position = points.ElementAt(aa);
			marker.Position = a;
			gmap.Invalidate();
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
    }
}
