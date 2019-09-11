using System;
using System.Windows.Forms;
using System.DirectoryServices.AccountManagement;
using System.IO;

namespace Discord_Cache_Cleaner
{
    public partial class Form1 : Form
    {
        public bool firstFolderCheck = false;
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
                label3.Text = $"Total Files: {fileCount}";

                // Calculates Total Folder Size
                foreach (FileInfo file in cacheFolderSize)
                {
                    fileFolderTotSize += file.Length;
                }

                // Converts to MBs
                fileFolderTotSize = fileFolderTotSize / 1024f / 1024f;
                fileFolderTotSize = (float)Math.Round(fileFolderTotSize, 2);

                // Sets Colour Output
                var color = Functions.colorResponse(fileFolderTotSize);

                // Updates Text Label
                label4.Text = "Space Taken: " + Convert.ToString(fileFolderTotSize) + " MB";
                label4.ForeColor = color;
            }
            catch
            {
                MessageBox.Show("Please make sure the directory is correct!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo cacheFolder = new DirectoryInfo(textBox1.Text);
                DialogResult confirmation = MessageBox.Show($"This will delete {cacheFolder.GetFiles().Length} files in your directory.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (confirmation == DialogResult.Yes)
                {
                    Functions.removeFiles(textBox1.Text);
                }
            }
            catch
            {
                MessageBox.Show("Please make sure the directory is correct!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!firstFolderCheck)
            {
                MessageBox.Show("If you can't find the Discord folder. Make sure you have 'Show Hidden Folders' Enabled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (discDir.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = discDir.SelectedPath;
                }

                firstFolderCheck = true;
            }
            else
            {
                if (discDir.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = discDir.SelectedPath;
                }
            }
        }

        FolderBrowserDialog discDir = new FolderBrowserDialog();

        private void Form1_Load(object sender, EventArgs e)
        {
            UserPrincipal user = UserPrincipal.Current;
            string currentUser = user.SamAccountName;

            if (string.IsNullOrEmpty(currentUser))
            {
                currentUser = user.DisplayName;
                textBox1.Text = $@"C:\Users\{currentUser}\AppData\Roaming\discord\Cache";
            }
            else
            {
                textBox1.Text = $@"C:\Users\{currentUser}\AppData\Roaming\discord\Cache";
            }
        }
    }
}
