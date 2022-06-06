
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revtec.core.Commands.CreateStuff
{
    [Transaction(TransactionMode.Manual)]
    public class CBSShiva : IExternalCommand
    {
        // Button Command code 
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            // App and Doc
            var uiDoc = commandData.Application.ActiveUIDocument;
            var doc = uiDoc.Document;

            // Display the form
            using (System.Windows.Forms.Form form = new NewBundleSheet(doc))
            {
                form.ShowDialog();
            }

            return Result.Succeeded;
        }


        #region GetPath

        public static string GetPath()
        {
            var commandName = typeof(CBSShiva).Namespace + "." + nameof(CBSShiva);
            return commandName;
        }

        #endregion
    }
}
