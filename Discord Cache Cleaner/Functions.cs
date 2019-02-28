using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Discord_Cache_Cleaner
{
    class Functions
    {

        public static Color colorResponse(float size)
        {
            // 0 = Green, 1 = Orange, 2 = Red

            Color color = new Color();

            if (size <= 100) { color = Color.Green; }
            if (size >= 100) { color = Color.Orange; }
            if (size >= 500) { color = Color.Red; }

            return color;
        }

        public static void removeFiles(string directory)
        {
            try
            {
                DirectoryInfo folderStorage = new DirectoryInfo(directory);

                foreach (FileInfo file in folderStorage.GetFiles())
                {
                    file.Delete();
                }
                MessageBox.Show("All files deleted succesfully!");
            } catch { MessageBox.Show("Please make sure Discord is closed before trying again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }    
    }
}
