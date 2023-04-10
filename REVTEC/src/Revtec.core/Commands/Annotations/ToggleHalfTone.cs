using System;
using System.Collections.Generic;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revtec.core.Commands.Annotations
{

    [Transaction(TransactionMode.Manual)]
    public class ToggleHalfTone : IExternalCommand
    {
        // Toggle HalfTone for all selected elements
        public static void ApplyHalfTone(UIDocument uidoc, Document doc)
        {

        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiDoc = commandData.Application.ActiveUIDocument;
            var doc = uiDoc.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            using (Transaction tx = new Transaction(doc, "Toggle HalfTone"))
            {
                tx.Start();

                // Get the selected elements
                ICollection<ElementId> selectedIds = uiDoc.Selection.GetElementIds();

                // Loop through each selected element and apply the half-tone override graphic settings
                foreach (ElementId id in selectedIds)
                {

                    if (doc.ActiveView.GetElementOverrides(id).Halftone)
                    {
                        OverrideGraphicSettings halfToneSettings = new OverrideGraphicSettings();
                        halfToneSettings.SetHalftone(false);
                        doc.ActiveView.SetElementOverrides(id, halfToneSettings);
                    }
                    else
                    {
                        OverrideGraphicSettings halfToneSettings = new OverrideGraphicSettings();
                        //halfToneSettings.SetSurfaceTransparency(50);
                        halfToneSettings.SetHalftone(true);
                        doc.ActiveView.SetElementOverrides(id, halfToneSettings);
                    }

                }

                tx.Commit();
            }

            return Result.Succeeded;

        }

        public static string GetPath()
        {
            var commandName = typeof(ToggleHalfTone).Namespace + "." + nameof(ToggleHalfTone);
            return commandName;
        }
    }


}