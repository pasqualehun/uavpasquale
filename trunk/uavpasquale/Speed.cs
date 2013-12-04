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
    public partial class Speed : UserControl
    {

        private PointF center;
        private int size;
        private PointF position;

        //speed
        private float maxSpeed;
        private float minSpeed;        
        private float angle;



        private double currentSpeed;
        public double CurrentSpeed
        {
            get { return currentSpeed; }
            set { currentSpeed = value; }
        }        


        public Speed()
        {
            InitializeComponent();
            DoubleBuffered = true;


            position = new PointF(10F, 10F);
            size = 150;
            center = new PointF(position.X + (size / 2), position.Y + size / 2);

            maxSpeed = 100;
            
            minSpeed = 0;
            currentSpeed = 0;            
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
			g.DrawString("Sebesség", new Font("Lucida Sans Unicode", 8), new SolidBrush(Color.Black), 55F, 70F);
             
        }

        private void DrawCursor(Graphics g)
        {
            CalculateAngle();
            g.DrawLine(new Pen(Color.Blue, 2), center.X, center.Y, center.X + (float)(size / 2) * (float)Math.Sin(angle), center.Y + (float)(size / 2) * (float)Math.Cos(angle));

        }

        private void CalculateAngle()
        {
            float tempAngle = ((float)currentSpeed / (maxSpeed - minSpeed))*330 +15 ;
            angle= -CalculateRadian(tempAngle);
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
            for (float i = minSpeed; i <= maxSpeed; i += (maxSpeed - minSpeed) / 10)
            {
                float angle2 = -CalculateRadian((float)i / (maxSpeed - minSpeed) * 330 + 15F);
                g.DrawLine(new Pen(Color.Blue, 2), center.X + (float)((size / 2) - 10) * (float)Math.Sin(angle2), center.Y + (float)((size / 2) - 10) * (float)Math.Cos(angle2), center.X + (float)(size / 2) * (float)Math.Sin(angle2), center.Y + (float)(size / 2) * (float)Math.Cos(angle2));
                g.DrawString(Math.Round(i, 0).ToString(), new Font("Lucida Sans Unicode", 8), new SolidBrush(Color.Blue), center.X + (float)((size / 2) - 20) * (float)Math.Sin(angle2) - 10, center.Y + (float)((size / 2) - 20) * (float)Math.Cos(angle2) - 10);

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void UpdateSpeed(double speed)
        {
            currentSpeed = speed*3.6;
        }
    }
}
