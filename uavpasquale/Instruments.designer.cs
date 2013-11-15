namespace WindowsFormsApplication1
{
    partial class Instruments
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
			serialPort1.Close();
			serialPort2.Close();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.uploadButton1 = new System.Windows.Forms.Button();
            this.gmap_plan = new GMap.NET.WindowsForms.GMapControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.baudCombo1 = new System.Windows.Forms.ComboBox();
            this.baudCombo2 = new System.Windows.Forms.ComboBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.speed1 = new WindowsFormsApplication1.Speed();
            this.vario1 = new WindowsFormsApplication1.Vario();
            this.altimeter1 = new WindowsFormsApplication1.Altimeter();
            this.compass1 = new WindowsFormsApplication1.Compass();
            this.calculation2 = new WindowsFormsApplication1.Calculation();
            this.view1 = new WindowsFormsApplication1.View();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM21";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 9);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 24);
            this.comboBox1.TabIndex = 5;
            // 
            // gmap
            // 
            this.gmap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gmap.AutoSize = true;
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(484, 7);
            this.gmap.Margin = new System.Windows.Forms.Padding(4);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaximumSize = new System.Drawing.Size(1067, 985);
            this.gmap.MaxZoom = 20;
            this.gmap.MinZoom = 1;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.ViewCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(468, 453);
            this.gmap.TabIndex = 6;
            this.gmap.Zoom = 14D;
            this.gmap.Load += new System.EventHandler(this.gMapControl1_Load);
            this.gmap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseDoubleClick);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(298, 9);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(160, 24);
            this.comboBox2.TabIndex = 7;
            // 
            // serialPort2
            // 
            this.serialPort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort2_DataReceived);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 40);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(970, 502);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.speed1);
            this.tabPage1.Controls.Add(this.vario1);
            this.tabPage1.Controls.Add(this.altimeter1);
            this.tabPage1.Controls.Add(this.gmap);
            this.tabPage1.Controls.Add(this.compass1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(962, 473);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "1.";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.uploadButton1);
            this.tabPage3.Controls.Add(this.gmap_plan);
            this.tabPage3.Controls.Add(this.calculation2);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(962, 473);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "3.";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // uploadButton1
            // 
            this.uploadButton1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.uploadButton1.Location = new System.Drawing.Point(770, 217);
            this.uploadButton1.Margin = new System.Windows.Forms.Padding(4);
            this.uploadButton1.Name = "uploadButton1";
            this.uploadButton1.Size = new System.Drawing.Size(100, 28);
            this.uploadButton1.TabIndex = 9;
            this.uploadButton1.Text = "Feltöltés";
            this.uploadButton1.UseVisualStyleBackColor = true;
            this.uploadButton1.Click += new System.EventHandler(this.uploadButton1_Click);
            // 
            // gmap_plan
            // 
            this.gmap_plan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gmap_plan.AutoSize = true;
            this.gmap_plan.Bearing = 0F;
            this.gmap_plan.CanDragMap = true;
            this.gmap_plan.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap_plan.GrayScaleMode = false;
            this.gmap_plan.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap_plan.LevelsKeepInMemmory = 5;
            this.gmap_plan.Location = new System.Drawing.Point(0, 0);
            this.gmap_plan.Margin = new System.Windows.Forms.Padding(4);
            this.gmap_plan.MarkersEnabled = true;
            this.gmap_plan.MaximumSize = new System.Drawing.Size(1067, 985);
            this.gmap_plan.MaxZoom = 20;
            this.gmap_plan.MinZoom = 1;
            this.gmap_plan.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.ViewCenter;
            this.gmap_plan.Name = "gmap_plan";
            this.gmap_plan.NegativeMode = false;
            this.gmap_plan.PolygonsEnabled = true;
            this.gmap_plan.RetryLoadTile = 0;
            this.gmap_plan.RoutesEnabled = true;
            this.gmap_plan.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap_plan.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap_plan.ShowTileGridLines = false;
            this.gmap_plan.Size = new System.Drawing.Size(468, 453);
            this.gmap_plan.TabIndex = 7;
            this.gmap_plan.Zoom = 14D;
            this.gmap_plan.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.gmap_plan_OnMarkerEnter);
            this.gmap_plan.Load += new System.EventHandler(this.gmap_plan_Load);
            this.gmap_plan.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gmap_plan_MouseClick);
            this.gmap_plan.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gmap_plan_MouseDoubleClick);
            this.gmap_plan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gmap_plan_MouseDown);
            this.gmap_plan.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gmap_plan_MouseMove);
            this.gmap_plan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gmap_plan_MouseUp);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.view1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(962, 473);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "2.";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // baudCombo1
            // 
            this.baudCombo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baudCombo1.FormattingEnabled = true;
            this.baudCombo1.Location = new System.Drawing.Point(170, 9);
            this.baudCombo1.Name = "baudCombo1";
            this.baudCombo1.Size = new System.Drawing.Size(121, 24);
            this.baudCombo1.TabIndex = 9;
            this.baudCombo1.SelectedIndexChanged += new System.EventHandler(this.baudCombo1_SelectedIndexChanged);
            // 
            // baudCombo2
            // 
            this.baudCombo2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baudCombo2.FormattingEnabled = true;
            this.baudCombo2.Location = new System.Drawing.Point(465, 9);
            this.baudCombo2.Name = "baudCombo2";
            this.baudCombo2.Size = new System.Drawing.Size(121, 24);
            this.baudCombo2.TabIndex = 10;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(592, 9);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(111, 23);
            this.connectButton.TabIndex = 11;
            this.connectButton.Text = "Kapcsolódás";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // speed1
            // 
            this.speed1.CurrentSpeed = 0D;
            this.speed1.Location = new System.Drawing.Point(4, 0);
            this.speed1.Margin = new System.Windows.Forms.Padding(5);
            this.speed1.Name = "speed1";
            this.speed1.Size = new System.Drawing.Size(240, 208);
            this.speed1.TabIndex = 2;
            // 
            // vario1
            // 
            this.vario1.Location = new System.Drawing.Point(237, 0);
            this.vario1.Margin = new System.Windows.Forms.Padding(5);
            this.vario1.Name = "vario1";
            this.vario1.Size = new System.Drawing.Size(239, 208);
            this.vario1.TabIndex = 1;
            // 
            // altimeter1
            // 
            this.altimeter1.Location = new System.Drawing.Point(4, 204);
            this.altimeter1.Margin = new System.Windows.Forms.Padding(5);
            this.altimeter1.Name = "altimeter1";
            this.altimeter1.Size = new System.Drawing.Size(240, 207);
            this.altimeter1.TabIndex = 3;
            // 
            // compass1
            // 
            this.compass1.Location = new System.Drawing.Point(237, 204);
            this.compass1.Margin = new System.Windows.Forms.Padding(5);
            this.compass1.Name = "compass1";
            this.compass1.Size = new System.Drawing.Size(239, 207);
            this.compass1.TabIndex = 4;
            // 
            // calculation2
            // 
            this.calculation2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.calculation2.Location = new System.Drawing.Point(706, 0);
            this.calculation2.Margin = new System.Windows.Forms.Padding(5);
            this.calculation2.Name = "calculation2";
            this.calculation2.Size = new System.Drawing.Size(253, 199);
            this.calculation2.TabIndex = 8;
            // 
            // view1
            // 
            this.view1.Location = new System.Drawing.Point(0, 0);
            this.view1.Margin = new System.Windows.Forms.Padding(5);
            this.view1.Name = "view1";
            this.view1.Size = new System.Drawing.Size(936, 446);
            this.view1.TabIndex = 1;
            // 
            // Instruments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 543);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.baudCombo2);
            this.Controls.Add(this.baudCombo1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.comboBox2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(965, 494);
            this.Name = "Instruments";
            this.Text = "GroundControl";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public Vario vario1;
        public Speed speed1;
        public Altimeter altimeter1;
        public Compass compass1;
        private System.IO.Ports.SerialPort serialPort1;
		private System.Windows.Forms.ComboBox comboBox1;
		private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.IO.Ports.SerialPort serialPort2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private View view1;
		private System.Windows.Forms.TabPage tabPage3;
		private GMap.NET.WindowsForms.GMapControl gmap_plan;
		private Calculation calculation2;
		private System.Windows.Forms.Button uploadButton1;
        private System.Windows.Forms.ComboBox baudCombo1;
        private System.Windows.Forms.ComboBox baudCombo2;
        private System.Windows.Forms.Button connectButton;

        

    }
}

