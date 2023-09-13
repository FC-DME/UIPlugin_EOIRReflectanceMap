using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using AGI.STKUtil;
using stdole;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace FC.UIPlugin.EOIRReflectanceMap
{
    public partial class UserInterface : UserControl, IAgUiPluginEmbeddedControl
    {

        #region Class Variables

        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private EOIRReflectanceMap m_uiplugin;
        private AgStkObjectRoot m_root;

        #endregion
        public UserInterface()
        {
            InitializeComponent();
        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiplugin = m_pEmbeddedControlSite.Plugin as EOIRReflectanceMap;
            m_root = m_uiplugin.STKRoot;
        }

        public void OnClosing()
        {
            
        }

        public void OnSaveModified()
        {
        }

        public IPictureDisp GetIcon()
        {
            return null;
        }

       

        private void radioButton_ShowAssets_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton_ShowAssets.Checked)
            {
                foreach (IAgStkObject asset in m_root.CurrentScenario.Children)
                {
                    string name = asset.InstanceName;
                    string classType = asset.ClassName;

                    m_root.ExecuteCommand($"Graphics */{classType}/{name} Show On");

                }
            }
            else if (radioButton_HideAssets.Checked)
            {
                foreach (IAgStkObject asset in m_root.CurrentScenario.Children)
                {
                    string name = asset.InstanceName;
                    string classType = asset.ClassName;

                    try
                    {
                        m_root.ExecuteCommand($"Graphics */{classType}/{name} Show Off");
                    }
                    catch
                    {
                        Console.WriteLine("Error");
                    }
                    

                }
            }

        }

        private void radioButton_ShowLines_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_ShowLines.Checked)
            {
                m_root.ExecuteCommand("MapDetails * LatLon Lat On");
            }
            else if (radioButton_HideLines.Checked)
            {
                m_root.ExecuteCommand("MapDetails * LatLon Lat Off");
            }
        }

        private void textBox_ImageName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only allow letters, numbers, hyphens and underscores (and allow user to use backspace key)
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '_' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void button_Browse_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox_Path.Text = folderDialog.SelectedPath;
            }
        }

        private void button_Run_Click(object sender, EventArgs e)
        {
            string imageName = textBox_ImageName.Text;
            string path = textBox_Path.Text;

            // Take screenshot
            m_root.ExecuteCommand($"MapSnap * ToFile \"{path}\\{imageName}.png\" 1");
            string a = $"MapSnap * ToFile \"{path}\\{imageName}.png\" 1";
            string b = $@"{path}\{imageName}.png";

            // Convert image
            Bitmap image = new Bitmap($@"{path}\{imageName}.png");
            pictureBox1.Image = image;

            float[,] meanImage = new float[image.Width, image.Height];
            Color pixelColor;

            for (int i = 0; i< image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    pixelColor = image.GetPixel(i, j);
                    meanImage[i,j] = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                }
            }

            float max = meanImage.Cast<float>().Max();

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    meanImage[i, j] = meanImage[i, j] / max;
                }
            }

            // Write to CSV
            using (StreamWriter outfile = new StreamWriter($@"{path}\{imageName}.csv"))
            {
                for (int i = 0; i < image.Width; i++)
                {
                    string content = "";
                    for (int j = 0; j < image.Height; j++)
                    {
                        content += meanImage[i, j].ToString() + ",";
                    }
                    outfile.WriteLine(content);
                }
            }

            // Get the corner coordinates
            IAgExecCmdResult result = m_root.ExecuteCommand("Zoom_R * CoordAdjust");
            string[] coordinates;
            if (result.IsSucceeded)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    coordinates = result[i].Split(' ');
                    string replacement = Regex.Replace(coordinates[3], @"\t|\n|\r", "");
                    coordinates[3] = replacement;

                    //m_psite.LogMessage(AgEUiPluginLogMsgType.eUiPluginLogMsgInfo, "MyPluginName: This message is for your information");

                    // Add to EOIR config if required
                    if (checkBox_AddToConfig.Checked)
                    {
                        m_root.ExecuteCommand($"EOIR */ PropertyMapData NewPropertyMap Earth {imageName}");

                        
                        m_root.ExecuteCommand($"EOIR */ PropertyMapData SetMapType Earth {imageName} ReflectanceMap");


                        m_root.ExecuteCommand($"EOIR */ PropertyMapData SetFile Earth {imageName} \"{path}\\{imageName}.csv\"");

                        m_root.ExecuteCommand($"EOIR */ PropertyMapData SetWavelengthRange Earth {imageName} 0.28 28.0");

                        // NW
                        m_root.ExecuteCommand($"EOIR */ PropertyMapData SetCoordinateP1Lon Earth {imageName} {coordinates[2]}");

                        m_root.ExecuteCommand($"EOIR */ PropertyMapData SetCoordinateP1Lat Earth {imageName} {coordinates[1]}");

                        // NE
                        m_root.ExecuteCommand($"EOIR */ PropertyMapData SetCoordinateP2Lon Earth {imageName} {coordinates[3]}");
                        m_root.ExecuteCommand($"EOIR */ PropertyMapData SetCoordinateP2Lat Earth {imageName} {coordinates[1]}");

                        // SE
                        m_root.ExecuteCommand($"EOIR */ PropertyMapData SetCoordinateP3Lon Earth {imageName} {coordinates[3]}");
                        m_root.ExecuteCommand($"EOIR */ PropertyMapData SetCoordinateP3Lat Earth {imageName} {coordinates[0]}");

                        // SW
                        m_root.ExecuteCommand($"EOIR */ PropertyMapData SetCoordinateP4Lon Earth {imageName} {coordinates[2]}");
                        m_root.ExecuteCommand($"EOIR */ PropertyMapData SetCoordinateP4Lat Earth {imageName} {coordinates[0]}");
                    }
                }
                

            }

        }


       

    }
}
