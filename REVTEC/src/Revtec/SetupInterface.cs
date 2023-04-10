using Autodesk.Revit.DB;
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
        #region SetupInterface Constructor

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
            const string tabName = "REVTEC-1.2.0";
            app.CreateRibbonTab(tabName);

            ////////////////////////////////////////    "Create Stuff" Panel    //////////////////////////////////////// 
            const string panel1PanelName = "+ Create +";
            var createStuffPanel = app.CreateRibbonPanel(tabName, panel1PanelName);

            //////////      Add buttons to Panel    //////////
            // Generate button data
            var buttonDataModel = new RevitPushButtonDataModel
            {
                Label = "Bundle Sheets",
                Panel = createStuffPanel,
                Tooltip = "Tool tip information goes here",
                IconImageName = "bundle_sheets.ico",
                TooltipImageName = "bundle_sheets.ico",
                CommandNamespacePath = Revtec.core.Commands.CreateStuff.CreateBundleSheets.GetPath()
            };
            // Add button to panel
            RevitPushButton.Create(buttonDataModel);


            ////////////////////////////////////////    "Families++" Panel    //////////////////////////////////////// 
            const string familiesPanelName = "Families++";
            var familiesPanel = app.CreateRibbonPanel(tabName, familiesPanelName);

            //////////      Add buttons to Panel    //////////
            // Generate button data
            var familiesPanelFamilyManagerDataModel = new RevitPushButtonDataModel
            {
                Label = "Family Manager",
                Panel = familiesPanel,
                Tooltip = "Tool tip information goes here",
                IconImageName = "family_manager.ico",
                TooltipImageName = "family_manager.ico",
                CommandNamespacePath = Revtec.core.Commands.FamilyStuff.FamilyManager.GetPath()
            };
            // Add button to panel
            RevitPushButton.Create(familiesPanelFamilyManagerDataModel);


            ////////////////////////////////////////    "Edit Stuff" Panel    //////////////////////////////////////// 
            //const string panel2PanelName = " % Edit %";
            //var panel2Panel = app.CreateRibbonPanel(tabName, panel2PanelName);

            ////////////      Add buttons to Panel    //////////
            //// Generate button data
            //var editButtonDataModel = new RevitPushButtonDataModel()
            //{
            //    Label = "Edit but 1",
            //    Panel = panel2Panel,
            //    Tooltip = "tool tip for this",
            //    IconImageName = "button1.ico",
            //    TooltipImageName = "button1.ico",
            //    CommandNamespacePath = Revtec.core.Commands.EditStuff.EditButton.GetPath()
            //};
            //// Add button to panel
            //RevitPushButton.Create(editButtonDataModel);

            //////////////////////////////////////////    "Annotation" Panel    //////////////////////////////////////// 
            const string annotationPanelName = " Annotations ";
            var annotationPanel = app.CreateRibbonPanel(tabName, annotationPanelName);

            //////////      Add buttons to Panel    //////////
            // btn1. Generate button data
            var annotationButtonDataModel1 = new RevitPushButtonDataModel()
            {
                Label = "Annotation1",
                Panel = annotationPanel,
                Tooltip = "tool tip for this",
                IconImageName = "button1.ico",
                TooltipImageName = "button1.ico",
                CommandNamespacePath = Revtec.core.Commands.Annotations.TagWallLayer.GetPath()
            };
            // btn1. Add button to panel
            RevitPushButton.Create(annotationButtonDataModel1);


            // btn2. Generate button data
            var annotationButtonDataModel2 = new RevitPushButtonDataModel()
            {
                Label = "Toggle HalfTone",
                Panel = annotationPanel,
                Tooltip = "Toggle for current selected elements",
                IconImageName = "toggle_halftone.ico",
                TooltipImageName = "toggle_halftone.ico",
                CommandNamespacePath = Revtec.core.Commands.Annotations.ToggleHalfTone.GetPath()
            };
            // btn2. Add button to panel
            RevitPushButton.Create(annotationButtonDataModel2);




            //////////////////////////////////////////    "Test" Panel    //////////////////////////////////////// 
            const string testPanelName = " && Testing &&";
            var testPanel = app.CreateRibbonPanel(tabName, testPanelName);

            //////////      Add buttons to Panel    //////////
            // Generate button data
            var test1ButtonDataModel = new RevitPushButtonDataModel()
            {
                Label = "Test 1",
                Panel = testPanel,
                Tooltip = "tool tip for this",
                IconImageName = "button1.ico",
                TooltipImageName = "button1.ico",
                //CommandNamespacePath = Revtec.core.Commands.TestingLab.Test1.GetPath()
                CommandNamespacePath = Revtec.core.Commands.Annotations.ToggleHalfTone.GetPath()
            };
            // Add button to panel

            RevitPushButton.Create(test1ButtonDataModel);
            //var test1Button = RevitPushButton.Create(test1ButtonDataModel) as RibbonItem;
            //test1Button.Visible = false;
            // set this function based on the project
            //test1Button.Enabled = false;

            ////////////////////////// Raw button -> Family only button///////////////////
            PushButtonData rawData = new PushButtonData("raw", "raw",
                Revtec.core.CoreAssembly.GetCoreAssemblyLocation(), Revtec.core.Commands.TestingLab.Test1.GetPath());
            rawData.LargeImage = Revtec.res.ResourceImage.GetIcon("button1.ico");
            rawData.AvailabilityClassName = "Revtec.core.Commands.TestingLab.CustomAvailability";

            testPanel.AddItem(rawData);

        }

        #endregion
    }
}
