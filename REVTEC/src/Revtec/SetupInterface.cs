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

            ////////////////////    "Create Stuff" Panel    //////////////////// 
            const string panel1PanelName = "Create.Stuff";
            var createStuffPanel = app.CreateRibbonPanel(tabName, panel1PanelName);

            //////////      Add buttons to Panel    //////////
            // Generate button data
            var buttonDataModel = new RevitPushButtonDataModel
            {
                Label = "Create.Bundle.Sheets",
                Panel = createStuffPanel,
                Tooltip = "Tool tip information goes here",
                IconImageName = "button1.ico",
                TooltipImageName = "button1.ico",
                CommandNamespacePath = Revtec.core.Commands.CreateStuff.CreateBundleSheets.GetPath()
            };
            // Add button to panel
            RevitPushButton.Create(buttonDataModel);


            ////////////////////    "Edit Stuff" Panel    //////////////////// 
            const string panel2PanelName = "Edit Stuff";
            var panel2Panel = app.CreateRibbonPanel(tabName, panel2PanelName);

            //////////      Add buttons to Panel    //////////
            // Generate button data
            var editButtonDataModel = new RevitPushButtonDataModel()
            {
                Label = "Edit but 1",
                Panel = panel2Panel,
                Tooltip = "tool tip for this",
                IconImageName = "button1.ico",
                TooltipImageName = "button1.ico",
                CommandNamespacePath = Revtec.core.Commands.EditStuff.EditButton.GetPath()
            };
            // Add button to panel
            RevitPushButton.Create(editButtonDataModel);


        }

        #endregion
    }
}
