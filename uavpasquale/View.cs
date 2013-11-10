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
	public partial class View : UserControl
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


		

		public View()
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
			DrawStatus(g);
		}






		private void Draw(Graphics g)
		{

			//g.DrawEllipse(new Pen(Color.Red, 3), position.X, position.Y, size, size);
			//g.DrawString("Sebesség   " + arrayA[75].ToString().Substring(0, 5), font, brushBlue, 10F, 70F);
			//g.DrawString("SebiB   " + arrayB[75].ToString(), font, brushBlack, 55F, 85F);


			//g.DrawString("Vario  " + arrayA[13].ToString().Substring(0,3), font, brushBlue, 10F, 90F);
			//g.DrawString("Vario Hiba!  " , new Font("Lucida Sans Unicode", 14), brushRed, 180F, 80F);

			//g.DrawString("Vario  " + elements[13].GetData().ToString().Substring(0,3).ToString(), font, brushBlue, 10F, 140F);

			for (int i = 0; i < 29; i++)
			{
				g.DrawString(elements[i].GetName(), font, brushBlue, 120F, i * 10F);
				g.DrawString(elements[i].fromA[9].ToString() + " " , font, brushBlue, 260F, i * 10F);
				g.DrawString(elements[i].faultA.ToString() + " " + elements[i].faultB.ToString(), font, brushBlue, 390F, i * 10F);

				
				g.DrawString(elements[i].GetName() + "\t" + elements[i].fromB[9].ToString(), font, brushBlue, 450F, i * 10F);
			}


		}

		private void DrawStatus(Graphics g)
		{
			int i=(int)arrayA[0];
			if (i >= 0 && i < error_imu_gps_ahrs.Length)
				g.DrawString("GPS :	" + error_imu_gps_ahrs[i].ToString(), font, brushRed, 10F, 10F);
			else
				g.DrawString("GPS :	-", font, brushBlue, 10F, 10F);


			if (i >= 0 && i < error_nav.Length)
				g.DrawString("NAV :	" + error_nav[i].ToString(), font, brushRed, 10F, 20F);
			else
				g.DrawString("NAV :	-", font, brushBlue, 10F, 20F);

			if (i >= 0 && i < error_cpu.Length)
				g.DrawString("CPU :	" + error_cpu[i].ToString(), font, brushRed, 10F, 30F);
			else
				g.DrawString("CPU :	-", font, brushBlue, 10F, 30F);


			if (i >= 0 && i < error_other_fcc.Length)
				g.DrawString("Other :	" + error_other_fcc[i].ToString(), font, brushRed, 10F, 40F);
			else
				g.DrawString("Other :	-", font, brushBlue, 10F, 40F);
		}
		

		public float CalculateRadian(float angle)
		{
			return (float)Math.PI / 180 * angle;
		}


		public void Update(double[] a, double[] b)
		{

			arrayA = a;
			arrayB = b;
			
		}


		public void Update(ref WindowsFormsApplication1.DataElement[] elements)
		{
			this.elements = elements;
		}
	}
}
