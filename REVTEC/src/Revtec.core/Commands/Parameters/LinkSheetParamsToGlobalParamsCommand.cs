using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;


namespace Revtec.core.Commands.Parameters
{
	[Transaction(TransactionMode.Manual)]
	public class LinkSheetParamsToGlobalParamsCommand : IExternalCommand
	{
		// ALL DATA REQUIRED FOR UI
		public static int TotalViewSheets = 0;
		public static int SheetsThatHaveAllSheetParams = 0;
		public static int SheetsThatHaveSomeSheetParams = 0;
		public static int SheetsThatHaveNoneSheetParams = 0;

		public static int TotalPassSheets = 0; // Sheets we tried to map and were successful
		public static int TotalFailSheets = 0; // Sheets we tried to map and failed

		// This dictionary exists because, we want to save time by going over the params through global params, not just every global param
		// only the global params that the project has. This is because even if we find a sheet-param for a sheet, if that param doesn't have 
		// a valid global param to link to, then there was no point in finding if the sheet has a particular sheet-param or not.
		public static Dictionary<GlobalParameter, string> TotalMatchedGlobalParamNamesToProjectParams;
		public static int TotalGlobalParamNamesInTheProject = 0;

		// All the params that will be linked to the global param.
		// Item1 in the tuple is the sheet param
		// Item2 in the tuple is the global param
		public static List<(string SheetParam, string GlobalParam)> sheet_param_to_global_param = new List<(string, string)>
				{
					("SF - AS DESIGNED - BASEMENT", "SF_AS DESIGNED_BASEMENT"),
					("SF - AS DESIGNED - FIRST FLOOR", "SF_AS DESIGNED_FIRST FLOOR"),
					("SF - AS DESIGNED - FRONT PORCH", "SF_AS DESIGNED_FRONT PORCH"),
					("SF - AS DESIGNED - GARAGE", "SF_AS DESIGNED_GARAGE"),
					("SF - AS DESIGNED - REAR DECK", "SF_AS DESIGNED_REAR DECK"),
					("SF - AS DESIGNED - SCREENED PORCH", "SF_AS DESIGNED_SCREENED PORCH"),
					("SF - AS DESIGNED - SECOND FLOOR", "SF_AS DESIGNED_SECOND FLOOR"),
					("SF - AS DESIGNED - THIRD FLOOR", "SF_AS DESIGNED_THIRD FLOOR"),
					("SF - AS DESIGNED - TOTAL FIN", "SF_AS DESIGNED_TOTAL FINISHED"),
					("SF - AS DESIGNED - UNF BASEMENT", "SF_AS DESIGNED_UNF BASEMENT"),
					("SF - AS DESIGNED - UNFINISHED", "SF_AS DESIGNED_UNFINISHED"),
					("SF - STD - BASEMENT", "SF_STD_BASEMENT"),
					("SF - STD - FIRST FLOOR", "SF_STD_FIRST FLOOR"),
					("SF - STD - FRONT PORCH", "SF_STD_FRONT PORCH"),
					("SF - STD - GARAGE", "SF_STD_GARAGE"),
					("SF - STD - REAR DECK", "SF_STD_REAR DECK"),
					("SF - STD - SCREENED PORCH", "SF_STD_SCREENED PORCH"),
					("SF - STD - SECOND FLOOR", "SF_STD_SECOND FLOOR"),
					("SF - STD - THIRD FLOOR", "SF_STD_THIRD FLOOR"),
					("SF - STD - TOTAL FIN", "SF_STD_TOTAL FINISHED"),
					("SF - STD - UNF BASEMENT", "SF_STD_UNF BASEMENT"),
					("SF - STD - UNFINISHED", "SF_STD_UNFINISHED"),
					("Has_Basement", "Has_Basement"),
					("Has_FrontPorch", "Has_FrontPorch"),
					("Has_Garage", "Has_Garage"),
					("Has_RearDeck", "Has_RearDeck"),
					("Has_RearOption", "Has_RearOption"),
					("Has_SecondFloor", "Has_SecondFloor"),
					("Has_Unfinished", "Has_Unfinished"),
					("County", "COUNTY"),
					("Eagle Project Name", "EAGLE PROJECT NAME"),
					("Lot Code", "LOT CODE"),
					("Lot Number", "LOT NUMBER"),
					("Neighborhood", "NEIGHBORHOOD"),
					("State", "STATE"),
					("Building Code", "BUILDING CODE"),
					("Construction Type", "CONSTRUCTION TYPE"),
					("Drawn Date", "DRAWN DATE"),
					("Eagle Revision Date", "REVISION DATE"),
					("Eagle Project Issue Date", "ISSUE DATE"),
					("Eagle Office Address", "EAGLE OFFICE ADDRESS")
		};

		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			// Grab the required data from revit
			Document doc = commandData.Application.ActiveUIDocument.Document;

			// Update the Data for the UI to use it
			// Filter to get all ViewSheet elements in the document
			FilteredElementCollector collector = new FilteredElementCollector(doc);
			ICollection<ViewSheet> viewSheets = collector
				.OfClass(typeof(ViewSheet))
				.Cast<ViewSheet>()
				.ToList();
			//Debug.WriteLine("hi");

			// Must reset these values before running since they are static
			TotalViewSheets = 0;
			SheetsThatHaveAllSheetParams = 0;
			SheetsThatHaveSomeSheetParams = 0;
			SheetsThatHaveNoneSheetParams = 0;
			TotalMatchedGlobalParamNamesToProjectParams = get_matching_global_param_map(doc);
			TotalGlobalParamNamesInTheProject = TotalMatchedGlobalParamNamesToProjectParams.Count;
			TotalPassSheets = 0;
			TotalFailSheets = 0;

			using (Transaction tx = new Transaction(doc, "Linking All SheetParams to GlobalParams"))
			{
				tx.Start();

				// Iterate through each sheet
				foreach (ViewSheet view_sheet in viewSheets)
				{
					TotalViewSheets++;

					string sheetName = view_sheet.Name;
					string sheetNumber = view_sheet.SheetNumber;
					//TaskDialog.Show("View Sheet Info", $"Sheet Name: {sheetName}\nSheet Number: {sheetNumber}");

					// Check if this sheet has:  all, none, some
					{
						int result_has_all_sheet_params = has_all_sheet_params(doc, view_sheet);
						if (result_has_all_sheet_params == 1) SheetsThatHaveAllSheetParams++;
						else if (result_has_all_sheet_params == 0) SheetsThatHaveNoneSheetParams++;
						else SheetsThatHaveSomeSheetParams++;
					}

					// Try linking all sheet params to global params
					link_all_sheet_params_to_global_params(doc, view_sheet, out TotalPassSheets, out TotalFailSheets);
				}
				tx.Commit();
			}
			Debug.WriteLine("hi");

			// Show the Window (The constructor handles the logic of populating the UI with the above data)
			Window window = new LinkSheetParamsToGlobalParamsView();

			window.Show();

			return Result.Succeeded;
		}

		// Returns 1: has all sheet params we look for. 0: has none of the sheet params we
		// looking for. -1: has some of the sheet params we are looking for
		public static int has_all_sheet_params(Document doc, ViewSheet viewSheet)
		{
			int totalParams = sheet_param_to_global_param.Count;
			int matchedParams = 0;

			foreach (var (SheetParam, _) in sheet_param_to_global_param)
			{
				Parameter param = viewSheet.LookupParameter(SheetParam);
				if (param != null && param.HasValue)
				{
					matchedParams++;
				}
				else
				{
					//Id = {3330816}
					//SheetParam = "SF - AS DESIGNED - FRONT PORCH"
					Debug.WriteLine("hi");
				}
			}

			if (matchedParams == totalParams)
			{
				return 1; // Has all required sheet parameters
			}
			else if (matchedParams == 0)
			{
				return 0; // Has none of the required sheet parameters
			}
			else
			{
				return -1; // Has some of the required sheet parameters
			}
		}


		public Dictionary<GlobalParameter, string> get_matching_global_param_map(Document doc)
		{
			var resultMap = new Dictionary<GlobalParameter, string>();

			// Get all global parameters in the document
			IList<GlobalParameter> globalParams = GlobalParametersManager.GetAllGlobalParameters(doc)
				.Select(id => doc.GetElement(id) as GlobalParameter)
				.Where(gp => gp != null)
				.ToList();

			// Iterate over the global parameters in the map and check for matches
			foreach (var pair in sheet_param_to_global_param)
			{
				// Find the global parameter in the document
				GlobalParameter globalParam = globalParams
					.FirstOrDefault(gp => gp.Name == pair.GlobalParam);

				// If the global parameter is found, add it to the result map
				if (globalParam != null)
				{
					resultMap[globalParam] = pair.SheetParam;
				}
			}

			return resultMap;
		}


		public void link_all_sheet_params_to_global_params(Document doc, ViewSheet viewSheet, out int total_pass, out int total_fail)
		{
			total_pass = 0;
			total_fail = 0;
			// The matching global parameters map is 'TotalMatchedGlobalParamNamesToProjectParams', which will be populated before
			// this function was called

			// Iterate over each pair in the matchingParams dictionary
			foreach (var pair in TotalMatchedGlobalParamNamesToProjectParams)
			{
				GlobalParameter globalParameter = pair.Key;
				string globalParamName = pair.Key.Name;
				string sheetParamName = pair.Value;

				// Try to find the sheet parameter on the ViewSheet
				Parameter sheetParam = viewSheet.LookupParameter(sheetParamName);

				// If the sheet parameter exists, try to link it to the global parameter
				// NOTE: params that are already associated to globla params show ReadyOnly as true.
				if (sheetParam != null && sheetParam.CanBeAssociatedWithGlobalParameters())
				{
					//TaskDialog.Show("Match Found", $"Matching Parameters:\nGlobal Param: {globalParamName}\nSheet Param: {sheetParamName}");

					// Try to link the sheet parameter to the global parameter (this part will depend on your specific logic for linking)
					try
					{
						sheetParam.AssociateWithGlobalParameter(globalParameter.Id);
						total_pass++;
					}
					catch (Exception ex)
					{
						//Id = {6570553}
						//sheetParamName = "State"
						//globalParamName = "STATE"
						//Id = {13734}
						Debug.WriteLine($"Error Failed to link parameters: {ex.Message}");
						//TaskDialog.Show("Error", $"Failed to link parameters: {ex.Message}");
						total_fail++;
					}
				}
				else
				{
					Debug.WriteLine($"No Match The sheet parameter '{sheetParamName}' was not found in this view sheet.");
					//TaskDialog.Show("No Match", $"The sheet parameter '{sheetParamName}' was not found in this view sheet.");
					total_fail++;
				}
			}
		}

		public void link_all_sheet_params_to_global_params_old(Document doc, ViewSheet viewSheet)
		{

			// Go over every global param that we found in this project
			// and for this global param(string), the sheet param name is the value in the hashmap
			// we first find if the given viewSheet has the sheet param, if not we skip this entry
			// if viewSheet has a param with name as the value in the entry, then we just show a task dialog box
			// tuple, and try linking that sheet param to its matching global param
			foreach (var sheet_param_global_param_tuple in sheet_param_to_global_param)
			{
				string sheet_param_name = sheet_param_global_param_tuple.Item1;
				string global_param_name = sheet_param_global_param_tuple.Item2;

				// Check if the ViewSheet  has a parameter with this name
				Parameter sheet_param = viewSheet.LookupParameter(sheet_param_name);
				if (sheet_param == null || sheet_param.AsValueString() == null) continue;

				// Check if project has a global parameter with this name
				ElementId global_param_eid = GlobalParametersManager.FindByName(doc, global_param_name);
				if (global_param_eid == null) continue;

			}
		}


		// method to get the namespace path, for registering the button on Revtec plugin
		public static string GetPath()
		{
			return typeof(LinkSheetParamsToGlobalParamsCommand).Namespace + "." + nameof(LinkSheetParamsToGlobalParamsCommand);
		}

	}
}

