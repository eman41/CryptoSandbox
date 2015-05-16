// Program.cs,  5/16/2015
// Author: Eric S Policaro

using System;
using System.Windows.Forms;

namespace CryptoSandbox
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
