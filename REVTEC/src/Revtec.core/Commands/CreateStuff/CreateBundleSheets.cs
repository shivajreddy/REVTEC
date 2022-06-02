using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revtec.core.Commands.CreateStuff
{
    [Transaction(TransactionMode.Manual)]
    public class CreateBundleSheets : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            TaskDialog.Show("title", "button haha clicked");
            return Result.Succeeded;
        }
    }
}
