using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace SkypeAdRmover
{
    static class Program
       {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void find_file()
        {
            System.IO.DirectoryInfo skype = new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Skype");
            foreach (System.IO.DirectoryInfo subDir1 in skype.GetDirectories())
            {
                if (File.Exists(System.IO.Path.Combine(subDir1.FullName, "config.xml")))
                {
                    Variables.filename = System.IO.Path.Combine(subDir1.FullName, "config.xml");
                    break;
                }
            }

          if (!File.Exists(Variables.filename))
            {
                MessageBox.Show("Cannot find file required, do you have Skype at all ? Try to run in Administrator rights!", "Error loading error message!", MessageBoxButtons.OK, MessageBoxIcon.Error);
               System.Environment.Exit(1);
           }
        }


        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            find_file();
            Application.Run(new MainForm());
        }
    }
}
