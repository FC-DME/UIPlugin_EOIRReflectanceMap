#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGI.Ui.Application;
using AGI.Ui.Core;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using stdole;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace FC.UIPlugin.EOIRReflectanceMap

{

    #region Class Attributes

    [Guid("1EFB22DB-5F64-4067-886B-EC033A63B81C")]
    [ProgId("FC.UIPlugin.EOIRReflectanceMap")]
    [ClassInterface(ClassInterfaceType.None)]

    #endregion

 
    public class EOIRReflectanceMap: IAgUiPlugin, IAgUiPluginCommandTarget
    {

        #region Variable Declaration

        private IAgUiPluginSite m_psite;
        
        private AgStkObjectRoot m_root;
        public AgStkObjectRoot STKRoot
        {
            get { return m_root; }
        }
        #endregion

        #region Plugin Interface

        /// <summary>
        /// Execute on plugin startup
        /// </summary>
        public void OnStartup(IAgUiPluginSite PluginSite)
        {
            m_psite = PluginSite;
            IAgUiApplication AgUiApp = m_psite.Application;
            m_root = AgUiApp.Personality2 as AgStkObjectRoot;
        }

        /// <summary>
        /// Execute on plugin shutdown
        /// </summary>
        public void OnShutdown()
        {
            m_psite = null;
        }

        public void OnDisplayConfigurationPage(IAgUiPluginConfigurationPageBuilder ConfigPageBuilder)
        {
            throw new NotImplementedException();
        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Execute on toolbar initialization
        /// </summary>
        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            ToolbarBuilder.AddButton("FC.UIPlugin.EOIRReflectanceMap.LaunchUI", "EOIR Reflectance Map Generator", "Launch the EOIR Reflectance Map Generator", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, null);
        }

        #endregion

        #region Plugin Command Target Interface
        public AgEUiPluginCommandState QueryState(string CommandName)
        {
            if (string.Compare(CommandName, "FC.UIPlugin.EOIRReflectanceMap.LaunchUI", true) == 0)
            {
                return AgEUiPluginCommandState.eUiPluginCommandStateEnabled | AgEUiPluginCommandState.eUiPluginCommandStateSupported;
            }
            return AgEUiPluginCommandState.eUiPluginCommandStateNone;
        }

        /// <summary>
        /// Called when toolbar button or menu item is clicked
        /// </summary>
        public void Exec(string CommandName, IAgProgressTrackCancel TrackCancel, IAgUiPluginCommandParameters Parameters)
        {
            if (string.Compare(CommandName, "FC.UIPlugin.EOIRReflectanceMap.LaunchUI", true) == 0)
            {
                MessageBox.Show("Hello World!");
            }
        }
    }

    #endregion
}

