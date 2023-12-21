#region References

using System;
using AGI.Ui.Application;
using AGI.Ui.Core;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using stdole;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection;
using DataImporter;
using System.Drawing;

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

        private IPictureDisp menuPicture;

        public static IAgUiPluginSite m_psite;
        
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
            //throw new NotImplementedException();
        }

        public void OnDisplayContextMenu(IAgUiPluginMenuBuilder MenuBuilder)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Execute on toolbar initialization
        /// </summary>
        public void OnInitializeToolbar(IAgUiPluginToolbarBuilder ToolbarBuilder)
        {
            Image menuImage;



            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            menuImage = Image.FromStream(currentAssembly.GetManifestResourceStream("EOIRReflectanceMap.Resources.Sun.png"));
            menuPicture = OlePictureHelper.OlePictureFromImage(menuImage);
            ToolbarBuilder.AddButton("FC.UIPlugin.EOIRReflectanceMap.LaunchUI", "EOIR Reflectance Map Generator", "Launch the EOIR Reflectance Map Generator", AgEToolBarButtonOptions.eToolBarButtonOptionAlwaysOn, menuPicture);


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
            OpenUserInterface();

        }

        #endregion

        /// <summary>
        /// Launch the UI
        /// </summary>
        public void OpenUserInterface()
        {
            IAgUiPluginWindowSite windows = m_psite as IAgUiPluginWindowSite;

            if (windows == null)
            {
                MessageBox.Show("Host application is unable to open windows.");
            }
            else
            {
                IAgUiPluginWindowCreateParameters winParams = windows.CreateParameters();
                winParams.AllowMultiple = false;
                winParams.AssemblyPath = this.GetType().Assembly.Location;
                winParams.UserControlFullName = typeof(UserInterface).FullName;
                winParams.Caption = "EOIR Reflectance Map Generator";
                winParams.DockStyle = AgEDockStyle.eDockStyleFloating;
                winParams.Width = 355;
                winParams.Height = 250;
                winParams.X = 0;
                winParams.Y = 0;
                object obj = windows.CreateNetToolWindowParam(this, winParams);
            }
        }

    }

}

