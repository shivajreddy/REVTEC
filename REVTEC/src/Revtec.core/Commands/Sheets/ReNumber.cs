using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls.Primitives;

namespace Revtec.core.Commands.Sheets
{
	[Transaction(TransactionMode.Manual)]
	public class ReNumber : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			// Command context
			var uiDoc = commandData.Application.ActiveUIDocument;
			var doc = uiDoc.Document;

			// Make sure the current view is a view sheet
			if (!(doc.ActiveView is ViewSheet sheet))
			{
				TaskDialog.Show("Renumber Details", "Please open a sheet view first.");
				return Result.Cancelled;
			}

			// Get all viewports (details) on this sheet
			var viewports = new FilteredElementCollector(doc)
				.OfClass(typeof(Viewport))
				.Cast<Viewport>()
				.Where(vp => vp.SheetId == sheet.Id)
				.ToList();

			if (viewports.Count == 0)
			{
				TaskDialog.Show("Renumber Details", "No details found on this sheet.");
				return Result.Cancelled;
			}

			// Log all the centers along with the detail name here
			string logMessage = "";
			foreach (var vp in viewports)
			{
				XYZ center = vp.GetBoxCenter();
				var detailParam = vp.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER);
				string detailName = detailParam != null ? detailParam.AsString() : "N/A";
				logMessage += $"Detail {detailName}: X={center.X:F3}, Y={center.Y:F3}\n";
			}
			Debug.Print(logMessage);

			// Group viewports into columns based on X coordinate (with tolerance)
			double columnTolerance = 0.5; // Adjust this value based on your sheet layout

			// First, sort all viewports by X coordinate to process right-to-left
			var sortedByX = viewports.OrderByDescending(vp => vp.GetBoxCenter().X).ToList();

			var columns = new List<List<Viewport>>();

			foreach (var vp in sortedByX)
			{
				XYZ center = vp.GetBoxCenter();

				// Find if this viewport belongs to an existing column
				var column = columns.FirstOrDefault(col =>
					Math.Abs(col[0].GetBoxCenter().X - center.X) < columnTolerance);

				if (column != null)
				{
					column.Add(vp);
				}
				else
				{
					// Create new column
					columns.Add(new List<Viewport> { vp });
				}
			}

			// Sort within each column from bottom-to-top (Y ascending)
			var sortedViewports = columns
				.SelectMany(col => col.OrderBy(vp => vp.GetBoxCenter().Y))  // Bottom to top in each column
				.ToList();

			// Start transaction for renumbering
			using (Transaction trans = new Transaction(doc, $"Renumber Details on {sheet.SheetNumber}"))
			{
				trans.Start();

				try
				{
					// First pass: Renumber to temporary letters to avoid conflicts
					char letter = 'A';
					foreach (var vp in sortedViewports)
					{
						var detailParam = vp.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER);
						if (detailParam != null && !detailParam.IsReadOnly)
						{
							detailParam.Set(letter.ToString());
							letter++;
						}
					}

					// Second pass: Renumber from 1 to n
					int detailNumber = 1;
					foreach (var vp in sortedViewports)
					{
						var detailParam = vp.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER);
						if (detailParam != null && !detailParam.IsReadOnly)
						{
							detailParam.Set(detailNumber.ToString());
							detailNumber++;
						}
					}

					trans.Commit();
					// TODO: Create a WPF notification window, show that on *top right* as a nice notification
					TaskDialog.Show("Renumber Details",
						$"Successfully renumbered {sortedViewports.Count} details on sheet {sheet.SheetNumber}.");
					return Result.Succeeded;
				}
				catch (Exception ex)
				{
					trans.RollBack();
					message = $"Failed to renumber details: {ex.Message}";
					return Result.Failed;
				}
			}
		}



		#region GetPath Method
		public static string GetPath()
		{
			var commandName = typeof(ReNumber).Namespace + "." + nameof(ReNumber);
			return commandName;
		}

		#endregion
	}
}
