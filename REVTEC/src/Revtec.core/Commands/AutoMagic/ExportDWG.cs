using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls.Primitives;

namespace Revtec.core.Commands.AutoMagic
{
	[Transaction(TransactionMode.Manual)]
	public class ExportDWG : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			// Command context
			var uiDoc = commandData.Application.ActiveUIDocument;
			var doc = uiDoc.Document;

			TaskDialog.Show("EXPORT DWG", "export dwg");

			// STEP 1: Export the DWG folder in to engineering folder
			// Zip all the .dwg files with correct folder name
			// Create Folder in Y drive
			// Copy Y drive files(pdf,dwg-zip,sor_cor) into Y drive folder

			// HELPER: Create folder name for zip file
			// HELPER: Create folder name for project folder in Y drive


			// Create Email
			// HELPER: Get the engineer name, email
			// HELPER: Get the plat engineer name, email

			return Result.Succeeded;
		}



		#region GetPath Method
		public static string GetPath()
		{
			var commandName = typeof(ExportDWG).Namespace + "." + nameof(ExportDWG);
			return commandName;
		}

		#endregion
	}
}
