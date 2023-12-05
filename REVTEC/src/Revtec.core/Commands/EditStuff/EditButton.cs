
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revtec.core.Commands.EditStuff
{
    [Transaction(TransactionMode.Manual)]
    public class EditButton : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            TaskDialog.Show("but2 title", "yolooo");
            return Result.Succeeded;
        }




        /// <summary>
        /// Get the namespace path of this command
        /// </summary>
        /// <returns></returns>
        public static string GetPath()
        {
            var commandName = typeof(EditButton).Namespace + "." + nameof(EditButton);
            return commandName;
        }
    }
}

