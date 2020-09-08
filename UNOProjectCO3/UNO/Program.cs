using System;
using System.Windows.Forms;

namespace UNOProjectCO3
{
    static class Program
    {
        public static Form MainForm { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(MainForm = new Games.Servers());
        }
    }
}
