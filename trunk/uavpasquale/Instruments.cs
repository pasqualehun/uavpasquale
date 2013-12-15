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

		const int SIZE = 75;

		static byte[] receivedBytesA = new byte[SIZE];
		static byte[] receivedBytesB = new byte[SIZE];

		static double[] decodedFromA = new double[29];
		static double[] decodedFromB = new double[29];

		static DataElement[] elements = new DataElement[29];

		static String[] names = { "idő", "magasság", "sebesség", "smart_imu->gyr1[0]", "smart_imu->gyr1[1]", "smart_imu->gyr1[2]", "tmp_P", "sebesség", "ahrs->psi", "ahrs->theta", "ahrs->phi", "control_cm->dr", "control_cm->de", "control_cm->da", "control_cm->dthr", "GPS health", "lon", "lat", "height", "smart_imu->acc1[0]", "smart_imu->acc1[1]", "smart_imu->acc1[2]", "smart_imu->mag[0]", "smart_imu->mag[1]", "smart_imu->mag[2]", "flightmode", "nextwaypoint*10+lc", "state->ms", "EKF status" };

		static object lockObj = new object();

	
        public Instruments()
        {
			InitializeComponent();

			addItemsToComboboxes();

			for (int i = 0; i < elements.Length; i++)
			{
				elements[i] = new DataElement(names[i]);
			}

        }

		public void addItemsToComboboxes()
		{			
			string[] ports = SerialPort.GetPortNames();
            
			foreach (string port in ports)
			{
				comboBox1.Items.Add(port);
				comboBox2.Items.Add(port);
			}

            object [] baudRates = {1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200};
            baudCombo1.Items.AddRange(baudRates);
            baudCombo2.Items.AddRange(baudRates);
            baudCombo1.SelectedIndex = baudCombo2.SelectedIndex = 3;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;

		}

        private List<byte> pufferA = new List<byte>();
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    byte[] data = new byte[serialPort1.BytesToRead];
                    (sender as System.IO.Ports.SerialPort).Read(data, 0, data.Length);

                    pufferA.AddRange(data);

                    if (pufferA.Count > SIZE * 2)
                    {
                        int i = 0;
                        for (; i < pufferA.Count; i++)
                        {
                            if (pufferA[i] == 'U' && pufferA[i + 1] == 'U' && pufferA[i + 2] == 'T')
                            {
                                int k = 0;
                                for (int j = i; j < i + SIZE; j++)
                                {
                                    receivedBytesA[k++] = pufferA[i];
                                    pufferA.RemoveAt(i);
                                }
                                decodedFromA = SerialUtil.Decode(receivedBytesA);
                                updateElementsA();
                                writeToTerminalA(receivedBytesA);
                                //pufferA.RemoveRange(0, i);
                                break;
                            }
                           
                        }
                    }
                    if (pufferA.Count > 2)
                    {

                        int i=0;
                        for (; i < pufferA.Count; i++)
                        {
                            if (pufferA[i] == 'A' && pufferA[i + 1] == 'C' && pufferA[i + 2] == 'K')
                            {
                                int k = 0;
                                for (int j = i; j < i + 3; j++)
                                {
                                    receivedBytesA[k++] = pufferA[i];
                                    pufferA.RemoveAt(i);
                                }
                                pufferA.RemoveRange(0, i);
                                writeToTerminalA(receivedBytesA);
                                MessageBox.Show("Sikeres feltöltés: " + serialPort1.PortName, "Feltöltés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    serialPort2.Close();
                }
            }
        }

        private List<byte> pufferB = new List<byte>();

		private void serialPort2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
            if (serialPort2.IsOpen)
            {
                try
                {
                    byte[] data = new byte[serialPort2.BytesToRead];
                    (sender as System.IO.Ports.SerialPort).Read(data, 0, data.Length);

                    pufferB.AddRange(data);

                    if (pufferB.Count > SIZE * 2)
                    {
                        int i = 0;
                        for (; i < pufferB.Count; i++)
                        {
                            if (pufferB[i] == 'U' && pufferB[i + 1] == 'U' && pufferB[i + 2] == 'T')
                            {
                                int k = 0;
                                for (int j = i; j < i + SIZE; j++)
                                {
                                    receivedBytesB[k++] = pufferB[i];
                                    pufferB.RemoveAt(i);
                                }
                                decodedFromB = SerialUtil.Decode(receivedBytesB);
                                updateElementsB();
                                writeToTerminalB(receivedBytesB);
                                pufferB.RemoveRange(0, i);
                                break;
                            }
                            if (pufferB[i] == 'A' && pufferB[i + 1] == 'C' && pufferB[i + 2] == 'K')
                            {
                                int k = 0;
                                for (int j = i; j < i + 5; j++)
                                {
                                    receivedBytesB[k++] = pufferB[i];
                                    pufferB.RemoveAt(i);
                                }
                                pufferB.RemoveRange(0, i);
                                //uint calculatedCheckSum = 0;
                                ////checksum számolás
                                //for (int j = 0; j < 5; j++)
                                //{
                                //    calculatedCheckSum += receivedBytesB[j];
                                //}
                                MessageBox.Show("Sikeres feltöltés: " + serialPort2.PortName, "Feltöltés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }
                    }
                 }
                
                catch (Exception)
                {
                    serialPort2.Close();
                }                
            }

		}

        void updateElementsA()
        {
            lock (lockObj)
            {
                for (int i = 0; i < decodedFromA.Length; i++)
                {
                    elements[i].AddA(decodedFromA[i]);
                    elements[i].Calculate();
                }
            }
        }

        void updateElementsB()
        {
            lock (lockObj)
            {
                for (int i = 0; i < decodedFromB.Length; i++)
                {
                    elements[i].AddB(decodedFromB[i]);
                    elements[i].Calculate();
                }
            }
        }

        PointLatLng previousPoint = new PointLatLng(0.0, 0.0);
        public void UpdateView()
		{
			while (true)
			{
				Thread.Sleep(500);

				if (serialPort1.IsOpen || serialPort2.IsOpen)
				{
                    PointLatLng point = new PointLatLng(elements[17].GetData(), elements[16].GetData());
                    
                    if(!previousPoint.Equals(point))
                    {
                        AddCoordinate(point);
                        previousPoint=point;
                    }

					view1.getReference(ref elements);
                    view1.CheckFaults();
				
					speed1.UpdateSpeed((float)elements[7].GetData());
                    vario1.UpdateClimb((float)elements[18].getDelta()/2);
                    altimeter1.UpdateAlt((float)elements[18].GetData());
                    compass1.UpdateHeading((float)elements[8].GetData() * 2 - 180);

					invalidateViews();				
				}
			}
		}
        void invalidateViews()
        {
            vario1.Invalidate();
            altimeter1.Invalidate();
            speed1.Invalidate();
            compass1.Invalidate();
            view1.Invalidate(); 
        }

		void writeToTerminalA(byte[] array)
		{
			String returnArray = "";

            String format = "";

            if (hexCheckBox.Checked)
            {
                format = "{0:X2}";
            }
            else
            {
                format = "{0:D3}";
            }

			foreach (byte item in array)
			{
				returnArray +=  String.Format(format, item) + " ";
			}

			if (InvokeRequired)
			{
				this.BeginInvoke(new Action<byte[]>(writeToTerminalA), new object[] { array });
				return;
			}
            richTextBox1.Text = returnArray;
		}

		void writeToTerminalB(byte[] array)
		{
			String returnArray = "";

            String format = "";

            if (hexCheckBox.Checked)
            {
                format = "{0:X2}";
            }
            else
            {
                format = "{0:D3}";
            }

			foreach (byte item in array)
			{
                returnArray += String.Format(format, item) + " ";
			}

			if (InvokeRequired)
			{
				this.BeginInvoke(new Action<byte[]>(writeToTerminalB), new object[] { array });
				return;
			}
            richTextBox2.Text = returnArray;
		}
		static List<PointLatLng> points = new List<PointLatLng>();
		
		static GMapRoute flightRoute = new GMapRoute("");

		static GMapMarkerImage planeMarker;
        GMarkerGoogle gPlaneMarker;

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

           
            try
            {
                Bitmap btm = new Bitmap("planeicon.png");
                planeMarker = new GMapMarkerImage(new PointLatLng(47.471154, 19.062481), btm);
                planeMarkerOverlay.Markers.Add(planeMarker);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A planeicon.PNG erőforrás nem található", "Repülőgép ikon hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                gPlaneMarker = new GMarkerGoogle(new PointLatLng(47.471154, 19.062481), GMarkerGoogleType.green);
                planeMarkerOverlay.Markers.Add(gPlaneMarker);
            }


   
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
		}

		void AddCoordinate(PointLatLng receivedCoordinate)
		{			
			flightRoute.Points.Add(receivedCoordinate);
			gmap.UpdateRouteLocalPosition(flightRoute);
            try
            {
                planeMarker.Position = receivedCoordinate;
            }
            catch 
            {
                gPlaneMarker.Position = receivedCoordinate;
            }
			gmap.Invalidate();
		}

		public class GMapMarkerImage : GMap.NET.WindowsForms.GMapMarker
		{
			private Image img;
			public GMapMarkerImage(PointLatLng p, Image image)
				: base(p)
			{
				img = image;
				Size = new Size(30, 30);
				Offset = new System.Drawing.Point(-15, -15);
			}

			public override void OnRender(Graphics g)
			{
                g.DrawImage(RotateImage(img, (float)elements[8].GetData()*2 - 180 ), LocalPosition.X, LocalPosition.Y, Size.Height, Size.Width);
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

			planView.AddReferencesToPlanningCalculation(ref plannedRoute, gmap_plan);
		}

		int numberOfPoints = 1;
		GMapOverlay markerOverlay = new GMapOverlay("markers");
		GMapOverlay plannedRouteOverlay = new GMapOverlay("route");

		GMapRoute plannedRoute = new GMapRoute("");
		private GMapMarker currentMarker;
		private bool isDraggingMarker;


		private void gmap_plan_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (plannedRoute.Points.Count < MAX_POINTS && e.Button == MouseButtons.Left)
			{
				PointLatLng currentPos = new PointLatLng(gmap_plan.FromLocalToLatLng(e.X, e.Y).Lat, gmap_plan.FromLocalToLatLng(e.X, e.Y).Lng);
				GMarkerGoogle marker = new GMarkerGoogle(currentPos, GMarkerGoogleType.blue_small);

				marker.ToolTipText = (numberOfPoints++).ToString();

				planeMarkerOverlay.Markers.Add(marker);
				markerOverlay.Markers.Add(marker);

				plannedRoute.Points.Add(currentPos);

				planView.Invalidate();

				gmap_plan.UpdateRouteLocalPosition(plannedRoute);
			}
		}
		int indexOfCurrentPoint;


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
				planView.Invalidate();

			}
		}

		private void uploadButton1_Click(object sender, EventArgs e)
		{
			double[] points = new double[86];
			int i = 0;
			points[i++] = plannedRoute.Points.Count;

			foreach (var item in plannedRoute.Points)
			{
				points[i++] = item.Lat;
				points[i++] = item.Lng;
			}

			try
			{
				serialPort1.Write(SerialUtil.Code(points,plannedRoute.Points.Count),0,86);
			}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                serialPort2.Write(SerialUtil.Code(points, plannedRoute.Points.Count), 0, 86);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

			
		}

		private void gmap_plan_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && currentMarker != null)
			{
				planeMarkerOverlay.Markers.Remove(currentMarker);
				markerOverlay.Markers.Remove(currentMarker);

				PointLatLng newPos = gmap_plan.FromLocalToLatLng(e.X, e.Y);

				plannedRoute.Points.RemoveAt(indexOfCurrentPoint);
				
				gmap_plan.UpdateRouteLocalPosition(plannedRoute);
				gmap_plan.Refresh();
				planView.Invalidate();
			}
		}

        private void baudCombo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baudCombo2.SelectedIndex = baudCombo1.SelectedIndex;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen || serialPort1.IsOpen)
            {
                serialPort1.Close();
                serialPort2.Close();
                enableComboboxes(true);
            }
            else
            {
                try
                {
                    serialPort1.PortName = comboBox1.SelectedItem.ToString();
                    serialPort1.WriteTimeout = 500;
                    serialPort1.BaudRate = (int)baudCombo1.SelectedItem;
                    serialPort1.Open();

                    serialPort2.PortName = comboBox2.SelectedItem.ToString();
                    serialPort2.WriteTimeout = 500;
                    serialPort2.BaudRate = (int)baudCombo2.SelectedItem;
                    serialPort2.Open();
                    enableComboboxes(false);
                }
                // give a message, if the port is not available:
                catch (NullReferenceException eex)
                {
                    MessageBox.Show("Nincs jó érték kiválasztva ", "Soros port hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(),"Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }            
        }

        void enableComboboxes(bool flag)
        {
            if (flag)
            {
                comboBox1.Enabled   = true;
                comboBox2.Enabled   = true;
                baudCombo1.Enabled  = true;
                baudCombo2.Enabled  = true;
                connectButton.Text = "Kapcsolódás";
            }
            else
            {
                comboBox1.Enabled   = false;
                comboBox2.Enabled   = false;
                baudCombo1.Enabled  = false;
                baudCombo2.Enabled  = false;
                connectButton.Text = "Lekapcsolódás";
            }
        }

    }
}
