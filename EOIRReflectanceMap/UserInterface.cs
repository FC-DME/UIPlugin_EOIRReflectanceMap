#region References

using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using AGI.STKUtil;
using stdole;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using DataImporter;

#endregion

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
            System.Drawing.Image menuImage;
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            menuImage = System.Drawing.Image.FromStream(currentAssembly.GetManifestResourceStream("EOIRReflectanceMap.Resources.PluginImage.png"));
            return OlePictureHelper.OlePictureFromImage(menuImage);
        }

        /// <summary>
        /// Show / hide assets depending on user selction
        /// </summary>
        private void radioButton_ShowAssets_CheckedChanged(object sender, EventArgs e)
        {
            if (m_root.CurrentScenario == null)
            {
                EOIRReflectanceMap.m_psite.LogMessage(AgEUiPluginLogMsgType.eUiPluginLogMsgWarning, "Please ensure a scenario is loaded.");
                return;
            }

            if (radioButton_ShowAssets.Checked)
            {
                foreach (IAgStkObject asset in m_root.CurrentScenario.Children)
                {
                    string name = asset.InstanceName;
                    string classType = asset.ClassName;

                    try
                    {
                        m_root.ExecuteCommand($"Graphics */{classType}/{name} Show On");
                    }
                    catch
                    {
                        Console.WriteLine("Error");
                    }

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

        /// <summary>
        /// Show / hide Lat/Lon lines depending on user selction
        /// </summary>
        private void radioButton_ShowLines_CheckedChanged(object sender, EventArgs e)
        {
            if (m_root.CurrentScenario == null)
            {
                EOIRReflectanceMap.m_psite.LogMessage(AgEUiPluginLogMsgType.eUiPluginLogMsgWarning, "Please ensure a scenario is loaded.");
                return;
            }
            if (radioButton_ShowLines.Checked)
            {
                m_root.ExecuteCommand("MapDetails * LatLon Lat On");
            }
            else if (radioButton_HideLines.Checked)
            {
                m_root.ExecuteCommand("MapDetails * LatLon Lat Off");
            }
        }

        /// <summary>
        /// Ensure proper formatting of image name
        /// </summary>
        private void textBox_ImageName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only allow letters, numbers, hyphens and underscores (and allow user to use backspace key)
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '_' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Browse for image save location
        /// </summary>
        private void button_Browse_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox_Path.Text = folderDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Create reflectance map
        /// </summary>
        private void button_Run_Click(object sender, EventArgs e)
        {

            // Error handling
            if (m_root.CurrentScenario == null)
            {
                EOIRReflectanceMap.m_psite.LogMessage(AgEUiPluginLogMsgType.eUiPluginLogMsgWarning, "Please ensure a scenario is loaded.");
                return;
            }
           
            if (m_root.GetLicensingReport().Contains("ExcludedProduct Name=\"stk_mission_level2\""))
            {
                EOIRReflectanceMap.m_psite.LogMessage(AgEUiPluginLogMsgType.eUiPluginLogMsgWarning, "Please ensure you are using a Premium or Enterprise license.");
                return;
            }
            try
            {
                m_root.ExecuteCommand("EOIR_R */ PropertyMapData GetNumberOfMaps Earth");
            }
            catch
            {
                EOIRReflectanceMap.m_psite.LogMessage(AgEUiPluginLogMsgType.eUiPluginLogMsgWarning, "Please ensure you have STK EOIR installed.");
                return;
            }
            string imageName = textBox_ImageName.Text;
            string path = textBox_Path.Text;
            if (imageName == "" || path == "")
            {
                EOIRReflectanceMap.m_psite.LogMessage(AgEUiPluginLogMsgType.eUiPluginLogMsgWarning, "Please enter a valid image name and path.");
                return;
            }

            // Take screenshot
            m_root.ExecuteCommand($"MapSnap * ToFile \"{path}\\{imageName}.png\" 1");
            string a = $"MapSnap * ToFile \"{path}\\{imageName}.png\" 1";
            string b = $@"{path}\{imageName}.png";

            // Convert image
            Bitmap image = new Bitmap($@"{path}\{imageName}.png");

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

            // Transpose matrix to rotate image correctly
            float[,] map = Transpose(meanImage);

            // Write to CSV
            using (StreamWriter outfile = new StreamWriter($@"{path}\{imageName}.csv"))
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    string content = "";
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        content += map[i, j].ToString() + ",";
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

                    EOIRReflectanceMap.m_psite.LogMessage(AgEUiPluginLogMsgType.eUiPluginLogMsgInfo, $"Successfully created {imageName} reflectance map and saved at {path}. The corner coordinates are: minimum latitude: {coordinates[0]}, maximum latitude: {coordinates[1]}, minimum longitude: {coordinates[2]}, maximum longitude: {coordinates[3]}.");

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

                        EOIRReflectanceMap.m_psite.LogMessage(AgEUiPluginLogMsgType.eUiPluginLogMsgInfo, $"Successfully added {imageName} reflectance map to the EOIR configuration.");
                    }
                }
                

            }

        }
        
        /// <summary>
        /// Transpose a matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public float[,] Transpose(float[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            float[,] result = new float[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }
        
        /// <summary>
        /// Open the plugin help
        /// </summary>
        private void button_Help_Click(object sender, EventArgs e)
        {
            
            try
            {
                string fileName = "Help.htm";
                string ex = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/Resources/{fileName}";
                string s = $"OpenHtmlViewer / \"{path}\"";
                m_root.ExecuteCommand(s);

            }
            catch
            {
                MessageBox.Show("Help file not found. Please check it is present at \"Plugin Install Folder\"\\Resources\\Help.html.");
            }
        }
    }
}
