
using Autodesk.Revit.UI;

namespace Revtec
{
    /// <summary>
    /// Setup the whole plugins interface -> tabs, panels, buttons
    /// </summary>
    public class SetupInterface
    {
        #region SetupInterface

        /// <summary>
        /// Default constructor
        /// </summary>
        public SetupInterface()
        {


        }

        #endregion

        #region pulic methods

        /// <summary>
        /// Initialize all the interface elements on custom created Revit Tab
        /// </summary>
        /// <param name="app"></param>
        public void Initialize(UIApplication app)
        {
            // Create Ribbon Tab
            const string tabName = "REVTEC-1.1.0";
            app.CreateRibbonTab(tabName);

            // "Create Stuff" panel
            const string panelName = "";
            app.CreateRibbonPanel(tabName, panelName);


        }

        #endregion
    }
}
