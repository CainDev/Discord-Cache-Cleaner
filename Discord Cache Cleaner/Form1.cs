using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Discord_Cache_Cleaner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            discDir.ShowNewFolderButton = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo cacheFolder = new DirectoryInfo(textBox1.Text);
                FileInfo[] cacheFolderSize = cacheFolder.GetFiles();
                float fileFolderTotSize = 0;

                string fileCount = Convert.ToString(cacheFolder.GetFiles().Length);
                label1.Text = $"{fileCount} Files Found!";

                // Calculates Total Folder Size
                foreach (FileInfo file in cacheFolderSize)
                {
                    fileFolderTotSize += file.Length;
                }

                // Converts to MBs
                fileFolderTotSize = fileFolderTotSize / 1024f / 1024f;

                // Sets Colour Output
                var color = Functions.colorResponse(fileFolderTotSize);

                // Updates Text Label
                label2.Text = Convert.ToString(fileFolderTotSize) + " MB";
                label2.ForeColor = color;
            }
            catch { MessageBox.Show("Please make sure the directory is correct!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DirectoryInfo cacheFolder = new DirectoryInfo(textBox1.Text);
            DialogResult confirmation = MessageBox.Show($"This will delete {cacheFolder.GetFiles().Length} files in your directory.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (confirmation == DialogResult.Yes)
            {
                Functions.removeFiles(textBox1.Text);
            }
            else if (confirmation == DialogResult.No)
            {
                // Do Nothing
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If you can't find the Discord folder. Make sure you have 'Show Hidden Folders' Enabled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (discDir.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = discDir.SelectedPath;
            }
        }

        FolderBrowserDialog discDir = new FolderBrowserDialog();
    }
}
