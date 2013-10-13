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
    public partial class Altimeter : UserControl
    {
        private PointF center;
        private int size;
        private PointF position;

        //altitude
        private float maxAltitude;
		private float unit;
        public double currentAltitude;
        private float angle;
		private float angleAlt;
        

        public Altimeter()
        {
            InitializeComponent();

            DoubleBuffered = true;
            position = new PointF(10F, 10F);
            size = 150;
            center = new PointF(position.X + (size / 2), position.Y + size / 2);

            maxAltitude=10000;
			unit = 1000;
			
			currentAltitude = 0;            
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
            g.DrawEllipse(new Pen(Color.Red,3),position.X ,position.Y, size, size);
			g.DrawString("Magasság", new Font("Lucida Sans Unicode", 8), new SolidBrush(Color.Black),55F,70F);
                
        }

        private void DrawCursor(Graphics g)
        {
            CalculateAngle();
            g.DrawLine(new Pen(Color.Blue, 2), center.X, center.Y, center.X + (float)(size / 2) * (float)Math.Sin(angle), center.Y + (float)(size / 2) * (float)Math.Cos(angle));
			CalculateAngle2();
			g.DrawLine(new Pen(Color.Red, 2), center.X, center.Y, center.X + (float)(size / 4) * (float)Math.Sin(angleAlt), center.Y + (float)(size / 4) * (float)Math.Cos(angleAlt));
			
        }

        private void CalculateAngle()
        {
			float tempAngle = ((float)currentAltitude / unit) * 360;
			angle= -CalculateRadian(tempAngle);
        }
		private void CalculateAngle2()
		{
			float tempAngle = ((float)currentAltitude / maxAltitude) * 360;
			angleAlt = -CalculateRadian(tempAngle);
		}


        public float CalculateRadian(float angle)
        {
            return (float)Math.PI / 180 * angle;
        }


        public void UpdateAlt(double a)
        {
            currentAltitude = a;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            base.OnPaintBackground(e);
            DrawNumbers(e.Graphics);    
        }

        private void DrawNumbers(Graphics g)
        {
            for (float i = 0; i < 10; i++)
            {
                float angle2 = -CalculateRadian( (float)i*36 );
                g.DrawLine(new Pen(Color.Blue, 2), center.X + (float)((size / 2)-10 ) * (float)Math.Sin(angle2), center.Y + (float)((size / 2)-10 ) * (float)Math.Cos(angle2) , center.X + (float)(size / 2) * (float)Math.Sin(angle2), center.Y + (float)(size / 2) * (float)Math.Cos(angle2));
                g.DrawString(Math.Round(i,0).ToString(), new Font("Lucida Sans Unicode", 8), new SolidBrush(Color.Blue), center.X + (float)((size / 2)-20 ) * (float)Math.Sin(angle2)-6, center.Y + (float)((size / 2)-20 ) * (float)Math.Cos(angle2)-10);
                
            } 
        }
    }
}
