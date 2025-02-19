using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.Linq;

namespace Revtec.core.Commands.Selection
{
	[Transaction(TransactionMode.Manual)]
	public class SelectAllSheets : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			//TaskDialog.Show("Hello", "There");
			UIDocument uidoc = commandData.Application.ActiveUIDocument;
			Document doc = uidoc.Document;

			// Goal is to set the current selection as if the user selected each and every single sheet.
			// not the sheet category, only the sheets
			// Create a filtered element collector to get all sheets in the document
			FilteredElementCollector collector = new FilteredElementCollector(doc);
			ICollection<Element> sheets = collector.OfClass(typeof(ViewSheet)).ToElements();

			// Convert the collection of sheets to a list of ElementIds
			IList<ElementId> sheetIds = sheets.Select(sheet => sheet.Id).ToList();

			// There is no way of updating the REVIT-UI to show the current selection.
			// User should know that this button actually selects all the sheet's, to confirm it look at show selections id's
			// and it will show all the id's of the current selection, which are the all the sheets

			// Set the current selection to the sheets
			uidoc.Selection.SetElementIds(sheetIds);

			// Notify the user that the sheets have been selected
			//TaskDialog.Show("Success", $"{sheetIds.Count} sheets have been selected.");

			return Result.Succeeded;
		}


		// method to get the namespace path, for registering the button on Revtec plugin
		public static string GetPath()
		{
			return typeof(SelectAllSheets).Namespace + "." + nameof(SelectAllSheets);
		}

	}

}
