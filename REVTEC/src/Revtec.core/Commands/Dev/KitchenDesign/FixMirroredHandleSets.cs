using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revtec.core.Commands.Dev.KitchenDesign
{
    [Transaction(TransactionMode.Manual)]
    public class FixMirroredHandleSets : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //TaskDialog.Show("Dev button 2", "Started");

            // Get the current document
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;

            // Get all casework instances
            var collector = new FilteredElementCollector(doc);
            var allCaseworkInstances = collector.OfCategory(BuiltInCategory.OST_Casework).WhereElementIsNotElementType().ToElements();


            using (var tx = new Transaction(doc))
            {
                tx.Start("Update Mirrored HandleSets");

                foreach (var element in allCaseworkInstances)
                {
                    if (!(element is FamilyInstance instance)) continue;

                    //if (!instance.Mirrored || !instance.Symbol.FamilyName.Contains("Base_Cabinet")) continue;

                    // Check if the instance is mirrored and has "Base_Cabinet" in its family name
                    if (instance.Mirrored && instance.Symbol.FamilyName.Contains("Base_Cabinet"))
                    {
                        var wasMirroredParam = instance.LookupParameter("Was_Mirrored");
                        wasMirroredParam?.Set(1); // 1 is true for Yes/No parameters in Revit API
                    }
                    else
                    {
                        var wasMirroredParam = instance.LookupParameter("Was_Mirrored");
                        wasMirroredParam?.Set(0); // 1 is true for Yes/No parameters in Revit API
                    }
                }

                tx.Commit();
            }



            //TaskDialog.Show("Dev button 2", "Finished");
            return Result.Succeeded;
        }


        // method to get the namespace path, for registering the button on Revtec UI
        public static string GetPath()
        {
            return typeof(FixMirroredHandleSets).Namespace + "." + nameof(FixMirroredHandleSets);
        }

    }
}
