using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public partial class ErrorOverview : UserControl
	{
		private PointF center;
		private int size;
		private PointF position;


		private DataElement[] elements;

		public double[] arrayA;
		public double[] arrayB;

		Font font;
		SolidBrush brushBlue;
		SolidBrush brushBlack;
		SolidBrush brushRed;

		//68
		private String[] error_imu_gps_ahrs	= {"IMU angvel range", "IMU angvel freeze", "IMU acc range", "IMU acc freeze", "IMU mag range", "IMU mag freeze", "IMU Ps range", "IMU Ps freeze", "IMU Pt range", "IMU Pt freeze", "GPS lat range", "GPS lat freeze", "GPS lon range", "GPS lon freeze", "GPS alt range", "GPS alt freeze", "GPS vn range", "GPS vn freeze", "GPS ve range", "GPS ve freeze", "GPS vd range", "GPS vd freeze", "GPS ITOW range", "GPS ITOW freeze", "AHRS bias range", "AHRS bias freeze", "AHRS Euler range", "AHRS Euler freeze"};
		//70
		private String[] error_nav			= { "Nav lat range", "Nav lat freeze", "Nav lon range", "Nav lon freeze", "Nav alt range", "Nav alt freeze", "Nav vn range", "Nav vn freeze", "Nav ve range", "Nav ve freeze", "Nav vd range", "Nav vd freeze" };
		//71
		private String[] error_control		= { "control da range", "control da freeze", "control de range", "control de freeze", "control dr range", "control dr freeze", "control dthr range", "control dthr freeze", "control da meas range", "control da meas freeze", "control de meas range", "control de meas freeze", "control dr meas range", "control dr meas freeze", "control dthr meas range", "control dthr meas freeze" };
		//72
		private String[] error_cpu			= { "block imu_daq", "block AHRS", "block nav", "block control", "block network_recv", "block network_send_diag", "block uplink", "block diag_self", "block_diag_other_fcc" };
		//73
		private String[] error_other_fcc	= { "nav lat diff", "nav lon diff", "nav alt diff", "nav vn diff", "nav ve diff", "nav vd diff", "AHRS bias diff", "AHRS Euler diff", "control da diff", "control de diff", "control dr diff", "control dthr diff" };


		

		public ErrorOverview()
		{
			InitializeComponent();
			DoubleBuffered = true;

			int ARRAYSIZE = 29;

			arrayA = new double[ARRAYSIZE];
			arrayB = new double[ARRAYSIZE];

			elements = new DataElement[ARRAYSIZE];


			for (int i = 0; i < elements.Length; i++)
			{
				elements[i] = new DataElement("Dummy,nincs adat");
				elements[i].AddA(0.0);
			}
		
			for (int i = 0; i < arrayA.Length; i++)
				arrayA[i] = arrayB[i] = -1;

			
				
			
			font = new Font("Lucida Sans Unicode", 8);
			brushBlue= new SolidBrush(Color.Blue);
			brushBlack = new SolidBrush(Color.Black);
			brushRed = new SolidBrush(Color.Red);

			//position = new PointF(10F, 10F);
			//size = 150;
			//center = new PointF(position.X + (size / 2), position.Y + size / 2);

			
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Draw(g);
		}

		private void Draw(Graphics g)
		{
			for (int i = 0; i < 29; i++)
			{
				g.DrawString(elements[i].GetName(), font, brushBlue, 120F, i * 12F);
				g.DrawString(String.Format("{0,15:0.00}" , elements[i].fromA[9]) + " " , font, brushBlue, 260F, i * 12F);
                g.DrawString(String.Format("{0:D3}", elements[i].faultA) + "    " + String.Format("{0:D3}", elements[i].faultB), font, brushBlue, 450F, i * 12F);

				
				g.DrawString(String.Format("{0,5:0.00}" , elements[i].fromB[9]), font, brushBlue, 350, i * 12F);
			}
       	}

		public void getReference(ref WindowsFormsApplication1.DataElement[] elements)
		{
			this.elements = elements;
		}
	}
}
