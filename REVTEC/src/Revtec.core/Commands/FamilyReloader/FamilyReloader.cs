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

            // Create an instance of the FamilyReloaderView window and pass the document
            var window = new FamilyReloaderView(doc);

            // Show the window as a dialog
            var result = window.ShowDialog();

            return result == true ? Result.Succeeded : Result.Cancelled;
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


