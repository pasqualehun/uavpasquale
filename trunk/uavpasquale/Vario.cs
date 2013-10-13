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
    public partial class Vario : UserControl
    {
        private PointF center;
        private int size;
        private PointF position;

        //climb


        public double currentClimb;
        private float maxClimb;
        private float angle;


        public Vario()
        {
            InitializeComponent();
            DoubleBuffered = true;

            position = new PointF(10F, 10F);
            size = 150;
            center = new PointF(position.X + (size / 2), position.Y + size / 2);

            currentClimb = 0;
            maxClimb = 5;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Draw(g);
            DrawCursor(g);
            // DrawNumbers(g);
        }

        private void Draw(Graphics g)
        {
            g.DrawEllipse(new Pen(Color.Red, 3), position.X, position.Y, size, size);
			g.DrawString("Emelkedés", new Font("Lucida Sans Unicode", 8), new SolidBrush(Color.Black), 55F, 70F);
             
        }

        private void DrawCursor(Graphics g)
        {
            CalculateAngle();
            g.DrawLine(new Pen(Color.Blue, 2), center.X, center.Y, center.X + (float)(size / 2) * (float)Math.Sin(angle), center.Y + (float)(size / 2) * (float)Math.Cos(angle));

        }

        private void CalculateAngle()
        {
            float tempAngle = ((float)currentClimb/(2*maxClimb))*300 + 90;
            angle = -CalculateRadian(tempAngle);
        }

        public float CalculateRadian(float angle)
        {
            return (float)Math.PI / 180 * angle;
        }

     

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            base.OnPaintBackground(e);
            DrawNumbers(e.Graphics);


        }
        private void DrawNumbers(Graphics g)
        {
            for (int i = 0; i <= maxClimb; i++)
            {
                float angle2 = -CalculateRadian((float)i /(2*maxClimb)*300 +90);
                g.DrawLine(new Pen(Color.Blue, 2), center.X + (float)((size / 2) - 10) * (float)Math.Sin(angle2), center.Y + (float)((size / 2) - 10) * (float)Math.Cos(angle2), center.X + (float)(size / 2) * (float)Math.Sin(angle2), center.Y + (float)(size / 2) * (float)Math.Cos(angle2));
                g.DrawString(i.ToString(), new Font("Lucida Sans Unicode", 8), new SolidBrush(Color.Blue), center.X + (float)((size / 2) - 20) * (float)Math.Sin(angle2) - 10, center.Y + (float)((size / 2) - 20) * (float)Math.Cos(angle2) - 10);
			}

			for (int i = 0; i <= maxClimb; i++)
			{
				float angle2 = CalculateRadian((float)i / (2 * maxClimb) * 300 -90);
				g.DrawLine(new Pen(Color.Blue, 2), center.X + (float)((size / 2) - 10) * (float)Math.Sin(angle2), center.Y + (float)((size / 2) - 10) * (float)Math.Cos(angle2), center.X + (float)(size / 2) * (float)Math.Sin(angle2), center.Y + (float)(size / 2) * (float)Math.Cos(angle2));
				g.DrawString(i.ToString(), new Font("Lucida Sans Unicode", 8), new SolidBrush(Color.Blue), center.X + (float)((size / 2) - 20) * (float)Math.Sin(angle2) - 10, center.Y + (float)((size / 2) - 20) * (float)Math.Cos(angle2) - 10);
			}
        }

        public void UpdateClimb(double a)
        {
            currentClimb = a;

        }

    }
}
