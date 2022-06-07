using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revtec.core.Commands.CreateStuff
{
    /// <summary>
    /// All the code for the command button, should go here.
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class CreateBundleSheets : IExternalCommand
    {

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
            var commandName = typeof(CreateBundleSheets).Namespace + "." + nameof(CreateBundleSheets);
            return commandName;
        }

        #endregion
    }
}
