
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;


namespace Revtec
{
    /// <summary>
    /// RevTec main entry point
    /// </summary>
    public class Main :IExternalApplication
    {
        #region external application public methods

        /// <summary>
        /// Called when Revit Starts
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>

        public Result OnStartup(UIControlledApplication application)
        {

            const string tabname = "REVTEC-1.1.0";
            application.CreateRibbonTab(tabname);

            const string panelName = "Create Stuff";
            var createPanel = application.CreateRibbonPanel(tabname, panelName);


            // Create push button data

            var CreateBundleSheetsPushButtonData = new PushButtonData("create_bundle_sheets", "Create Bundle Sheets",
                Assembly.GetExecutingAssembly().Location, "s")
            {
                ToolTip = "Create a bundle of sheets",
                ToolTipImage = new BitmapImage(new Uri(@"C:\Users\sreddy\Desktop\button1.ico"))
                
            };

            var createBundleSheetsPushButton = createPanel.AddItem(CreateBundleSheetsPushButtonData) as PushButton;

            createBundleSheetsPushButton.LargeImage = new BitmapImage(new Uri(@"C:\Users\sreddy\Desktop\button1.ico"));



            return Result.Succeeded;
        }

        /// <summary>
        /// Called when revit shutsdown
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Result OnShutdown(UIControlledApplication application)
        {
            throw new System.NotImplementedException();
        }


        #endregion

    }
}
