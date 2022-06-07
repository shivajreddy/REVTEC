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
            // App and Doc
            var uiDoc = commandData.Application.ActiveUIDocument;
            var doc = uiDoc.Document;


            // Display the form
            using (System.Windows.Forms.Form winForm = new FamilyManagerForm(doc))
            {
                winForm.ShowDialog();
            }



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
