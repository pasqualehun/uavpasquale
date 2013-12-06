using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Compass : UserControl
    {
        private PointF center;
        private int size;
        private PointF position;
        private float angle;

        private double heading;
        private String degree;

        public Compass()
        {
            InitializeComponent();
            DoubleBuffered = true;

            position = new PointF(10F, 10F);
            size = 150;
            center = new PointF(position.X + (size / 2), position.Y + size / 2);
			angle = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Draw(g);
           // DrawCursor(g);
            // DrawNumbers(g);
        }

        private void Draw(Graphics g)
        {
            g.DrawEllipse(new Pen(Color.Red, 3), position.X, position.Y, size, size);
            DrawNumbers(g);
        }


        private void DrawCursor(Graphics g)
        {
            //CalculateAngle();
            g.DrawLine(new Pen(Color.Blue, 2), center.X, center.Y, center.X + (float)(size / 2) * (float)Math.Sin(angle), center.Y + (float)(size / 2) * (float)Math.Cos(angle));
            //CalculateAngle2();
            //g.DrawLine(new Pen(Color.Red, 2), center.X, center.Y, center.X + (float)(size / 4) * (float)Math.Sin(angleAlt), center.Y + (float)(size / 4) * (float)Math.Cos(angleAlt));

        }

        public float CalculateRadian(float angle)
        {
            return (float)Math.PI / 180 * angle;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            base.OnPaintBackground(e);


            PointF[] points = { new PointF(30F,0F), new PointF(25F, 25F), new PointF(0F, 60F), new PointF(0F, 75F),
                                new PointF(25F,60F),  new PointF(25F,90F),  new PointF(15F,105F),  new PointF(30F,100F),

                                new PointF(45F,105F),new PointF(35F,90F),new PointF(35F,60F),new PointF(60F, 75F),
                                new PointF(60F, 60F),new PointF(35F, 25F),new PointF(30F,0F)
                              };

            e.Graphics.TranslateTransform(55F, 30F);
            e.Graphics.DrawLines(new Pen(Color.Red, 3), points);
            e.Graphics.ResetTransform();
            //e.Graphics.DrawLine(new Pen(Color.Yellow, 2), center.X, center.Y, center.X + (float)(size / 2) * (float)Math.Sin(CalculateRadian(180)), center.Y + (float)(size / 2) * (float)Math.Cos(CalculateRadian(180)));
                  

        }

        private void DrawNumbers(Graphics g)
        {
            for (float i = 0; i < 12; i++)
            {
                degree = Math.Round(i * 3, 0).ToString();
                switch (degree)
                {
                    case "0" :degree="N";break;
                    case "9": degree = "E"; break;
                    case "18": degree = "S"; break;
                    case "27": degree = "W"; break;
                    default:break;
                }
				
				//measure the size of the degree, which is written
                SizeF stringSize = g.MeasureString(degree, this.Font);

				//calculate the radian of i.th iteration's degree
                float angle2 = -CalculateRadian((float)i * 30+(float)heading +180);
				
                g.DrawLine(new Pen(Color.Blue, 2), center.X + (float)((size / 2) - 10) * (float)Math.Sin(angle2), center.Y + (float)((size / 2) - 10) * (float)Math.Cos(angle2), center.X + (float)(size / 2) * (float)Math.Sin(angle2), center.Y + (float)(size / 2) * (float)Math.Cos(angle2));
                
				//translate to the proper position
                g.TranslateTransform(center.X + (float)((size / 2) - 20) * (float)Math.Sin(angle2) - 0, center.Y + (float)((size / 2) - 20) * (float)Math.Cos(angle2) - 0);

				//rotate with the needed degree
				g.RotateTransform(i * 30 + (float)heading + 180 + 180);
				
				//translate to the center of the string
                g.TranslateTransform(-stringSize.Width / 2, -stringSize.Height / 2);

				//draw the string
                g.DrawString(degree, new Font("Arial", 8), new SolidBrush(Color.Blue), 0F, 0F);
				
				//reset the matrices
                g.ResetTransform();
            }
        }
		public void UpdateHeading(double a)
		{
			heading = -a;

		}
    }
}
