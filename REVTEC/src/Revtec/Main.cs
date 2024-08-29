using System;
using Autodesk.Revit.UI;


namespace Revtec
{

    // RevTec main entry point
    public class Main : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {

            // Subscribe to the Idling event
            //application.Idling += OnIdling;

            // Subscribe to the View event
            //application.ViewActivated += OnViewChanged;


            // Initiate the application using revtec ui
            var ui = new SetupInterface();
            ui.Initialize(application);

            return Result.Succeeded;
        }

        private static void OnIdling(object sender, EventArgs e)
        {
            var uiApplication = sender as UIApplication;

            // Check if there is an active document
            uiApplication?.ActiveUIDocument?.Selection.GetElementIds();
        }


        public Result OnShutdown(UIControlledApplication application)
        {

            // Unsubscribe from the Idling event
            //application.Idling -= OnIdling;


            return Result.Succeeded;
        }
    }
}
