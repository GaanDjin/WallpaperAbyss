using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperAbyss
{
    /// <summary>
    /// https://www.codeproject.com/Articles/31376/Making-a-C-screensaver
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].ToLower().Trim().Substring(0, 2) == "/s") //show screensaver
                {
                    //show the screen saver
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    ShowScreenSaver(); //this is the good stuff
                    Application.Run();
                }
                else if (args[0].ToLower().Trim().Substring(0, 2) == "/p") //preview screensaver inside handle
                {
                    //show the screen saver preview
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //args[1] is the handle to the preview window
                    Application.Run(new MainForm(new IntPtr(long.Parse(args[1]))));
                }
                else if (args[0].ToLower().Trim().Substring(0, 2) == "/c") //configure
                {
                    //configure the screen saver
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new ConfigureForm()); //this is the good stuff
                }
            }
            else
            {
                //no arguments were passed (we should probably show the screen saver)
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                ShowScreenSaver(); //this is the good stuff
                Application.Run();
            }
        }

        /// <summary>
        /// Create a new screensaver window for each screen.
        /// </summary>
        static void ShowScreenSaver()
        {
            int screenNo = 0;
            foreach (Screen screen in Screen.AllScreens)
            {
                MainForm screensaver = new MainForm(screen.Bounds, screenNo++);
                screensaver.Show();
            }
        }
    }
}
