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
using stdole;

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_root.CurrentScenario == null)
            {
                MessageBox.Show("I know that no scenario is open.");
            }
            else
            {
                MessageBox.Show("I know your scenario's name is " + m_root.CurrentScenario.InstanceName);
            }
        }
    }
}
