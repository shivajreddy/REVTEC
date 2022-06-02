
using Autodesk.Revit.UI;
using Revtec.res;
using Revtec.ui.Revit;

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
        public void Initialize(UIControlledApplication app)
        {
            // Create Ribbon Tab
            const string tabName = "REVTEC-1.1.0";
            app.CreateRibbonTab(tabName);

            // "Create Stuff" panel
            const string panel1PanelName = "Create.Stuff";
            var createStuffPanel = app.CreateRibbonPanel(tabName, panel1PanelName);


            // Populate the button data model
            var buttonDataModel = new RevitPushButtonDataModel
            {
                Label = "Create.Bundle.Sheets",
                Panel = createStuffPanel,
                Tooltip = "Tool tip information goes here",
                IconImageName = "button1.ico",
                TooltipImageName = "button1.ico",
                CommandNamespacePath = Revtec.core.Commands.CreateStuff.CreateBundleSheets.GetPath()
            };

            // Create the button
            var buttonData = RevitPushButton.Create(buttonDataModel);
        }

        #endregion
    }
}
