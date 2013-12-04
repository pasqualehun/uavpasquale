using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace WindowsFormsApplication1
{
	public partial class PlanView : UserControl
	{
		private GMapRoute route;
		private GMapControl gmap_plan;

		Font font;
		SolidBrush brushBlue;
		SolidBrush brushBlack;
		SolidBrush brushRed;


		public PlanView()
		{
			InitializeComponent();
			DoubleBuffered = true;
			font = new Font("Lucida Sans Unicode", 8);
			brushBlue = new SolidBrush(Color.Blue);
			brushBlack = new SolidBrush(Color.Black);
			brushRed = new SolidBrush(Color.Red);


		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			DrawStatus(g);
		}

		private void DrawStatus(Graphics g)
		{
			if (route != null)
			{
				g.DrawString("Összesen : " + String.Format("{0:0.00}", route.Distance) + "km", font, brushBlue, 0F, 0F);
				PointLatLng prevPoint = new PointLatLng(0,0);
				float numberOfPoints = 1;
				foreach (var item in route.Points)
				{
					if (!prevPoint.Equals(new PointLatLng(0, 0)))
					{
						g.DrawString(numberOfPoints + " -> " + (numberOfPoints+1) + "         " + String.Format("{0:0.00}", gmap_plan.MapProvider.Projection.GetDistance(prevPoint, item)) + " km", font, brushBlack, 0F, 12 * numberOfPoints++);
					}
					prevPoint = item;
				}
				if (numberOfPoints > 9)
				{
					g.DrawString("Max. 10 pont kijelölése lehetséges", font, brushRed, 0F, 12 * numberOfPoints);
				}
			}	
		}

		public void AddReferencesToPlanningCalculation(ref GMapRoute route, GMapControl gmap_plan)
		{
			this.route = route;
			this.gmap_plan = gmap_plan;
		}
	}
}
