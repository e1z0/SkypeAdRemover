using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace SkypeAdRmover
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://wiki.eofnet.lt/");
            System.Diagnostics.Process.Start("https://github.com/e1z0/SkypeAdRemover");

        }

        private void do_patch()
        {
            try
            {
                // force close the skype client
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c taskkill /F /im skype.exe";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                // make the backup
                if (checkBox1.Checked & !File.Exists(Variables.filename + ".bak")) File.Copy(Variables.filename, Variables.filename + ".bak");
                // remove file read-only attributes
                File.SetAttributes(Variables.filename, FileAttributes.Normal);
                // replace text with enabled advertising to disabled one
                string str = File.ReadAllText(Variables.filename);
                str = str.Replace("<AdvertEastRailsEnabled>1</AdvertEastRailsEnabled>", "<AdvertEastRailsEnabled>0</AdvertEastRailsEnabled>");
                str = str.Replace("<AdvertPlaceholder>1</AdvertPlaceholder>", "<AdvertPlaceholder>0</AdvertPlaceholder>");
                File.WriteAllText(Variables.filename, str);
                // set file attribute back to read-only
                File.SetAttributes(Variables.filename, File.GetAttributes(Variables.filename) | FileAttributes.ReadOnly);
                MessageBox.Show("Done, now start a new Skype session!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button1.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot make changes! Maybe file is open by another app or already patched ? Try in Administrator rights!", "Administrator rights required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            do_patch();
            
        }
    }
}
