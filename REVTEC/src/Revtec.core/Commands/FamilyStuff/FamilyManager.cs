using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revtec.core.Commands.FamilyStuff
{
    [Transaction(TransactionMode.Manual)]
    public class FamilyManager : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            TaskDialog.Show("test title", "working for family manager button");
            return Result.Succeeded;
        }











        # region GetPath Method

        public static string GetPath()
        {
            var commandName = typeof(FamilyManager).Namespace + "." + nameof(FamilyManager);
            return commandName;
        }

        #endregion
    }
}
