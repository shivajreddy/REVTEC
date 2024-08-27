using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using System.Windows;
using System.Windows.Controls;


namespace Revtec.core.Commands.FamilyStuff
{
    [Transaction(TransactionMode.Manual)]
    public class FamilyReloader : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // App and Doc
            var uiDoc = commandData.Application.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Display the form

            /*
            using (System.Windows.Forms.Form winForm = new FamilyReloaderForm(doc))
            {
                winForm.ShowDialog();
                //winForm.Show();
                winForm.Activate();
            }
            */

            // Create a WPF Window to host the UserContorl
            var window = new Window
            {
                Title = "Family Reloader",
                Width = 600,
                Height = 1000,
                Content = new FamilyReloaderView(doc),
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            // Show the window as a dialog
            window.ShowDialog();

            return Result.Succeeded;
        }


        # region GetPath Method

        public static string GetPath()
        {
            var commandName = typeof(FamilyReloader).Namespace + "." + nameof(FamilyReloader);
            return commandName;
        }

        #endregion
    }
}


