using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Serial a;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Instruments i = new Instruments();


			Thread UpdateView_ = new Thread(i.UpdateView);
			UpdateView_.Name = "update";
			UpdateView_.Start();
			UpdateView_.IsBackground = true;

			

            //a = new Serial(i);

            Application.Run(i);
        }
    }
}
