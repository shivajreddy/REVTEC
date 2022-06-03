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

            // Initiate the application using revtec ui
            var ui = new SetupInterface();
            ui.Initialize(application);


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
            return Result.Succeeded;
        }

        #endregion
    }
}
