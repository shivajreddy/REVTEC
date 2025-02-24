﻿using Autodesk.Revit.UI;
using Revtec.ui.Revit;

namespace Revtec
{
	/// Setup the whole plugins interface -> tabs, panels, buttons
	public class SetupInterface
	{
		/// Default constructor
		public SetupInterface()
		{
		}

		#region pulic methods

		/// :: Initialize all the interface elements on custom created Revit Tab
		public void Initialize(UIControlledApplication app)
		{
			// Create Ribbon Tab
			const string tabName = "REVTEC-1.2.0";
			app.CreateRibbonTab(tabName);

			////////////////////////////////////////    "Create Stuff" Panel    //////////////////////////////////////// 
			const string panel1PanelName = "Create";
			var createStuffPanel = app.CreateRibbonPanel(tabName, panel1PanelName);

			//////////      Add buttons to Panel    //////////
			// Generate button data
			var buttonDataModel = new RevitPushButtonDataModel
			{
				Label = "Bundle Sheets",
				Panel = createStuffPanel,
				Tooltip = "Tool tip information goes here",
				IconImageName = "bundle_sheets.ico",
				IconLargeImageName = "bundle_sheets.ico",
				TooltipImageName = "bundle_sheets.ico",
				CommandNamespacePath = core.Commands.CreateStuff.CreateBundleSheets.GetPath()
			};
			// Add button to panel
			RevitPushButton.Create(buttonDataModel);


			// /*
			////////////////////////////////////////    "Families++" Panel    //////////////////////////////////////// 
			const string familiesPanelName = "Families";
			var familiesPanel = app.CreateRibbonPanel(tabName, familiesPanelName);

			//////////      Add buttons to Panel    //////////
			// Generate button data
			var familiesPanelFamilyReloaderDataModel = new RevitPushButtonDataModel
			{
				Label = "Family Reloader",
				Panel = familiesPanel,
				Tooltip = "Tool tip information goes here",
				IconImageName = "family_manager.ico",
				IconLargeImageName = "family_manager.ico",
				TooltipImageName = "family_manager.ico",
				CommandNamespacePath = Revtec.core.Commands.FamilyStuff.FamilyReloader.GetPath()
			};
			// Add button to panel
			RevitPushButton.Create(familiesPanelFamilyReloaderDataModel);
			// */


			////////////////////////////////////////    "Edit Stuff" Panel    //////////////////////////////////////// 
			//const string panel2PanelName = " % Edit %";
			//var panel2Panel = app.CreateRibbonPanel(tabName, panel2PanelName);

			////////////      Add buttons to Panel    //////////
			//// Generate button data
			//var editButtonDataModel = new RevitPushButtonDataModel()
			//{
			//    Label = "Edit but 1",
			//    Panel = panel2Panel,
			//    Tooltip = "tool tip for this",
			//    IconImageName = "button1.ico",
			//    TooltipImageName = "button1.ico",
			//    CommandNamespacePath = Revtec.core.Commands.EditStuff.EditButton.GetPath()
			//};
			//// Add button to panel
			//RevitPushButton.Create(editButtonDataModel);

			//////////////////////////////////////////    "Annotation" Panel    //////////////////////////////////////// 
			const string annotationPanelName = " Annotations ";
			var annotationPanel = app.CreateRibbonPanel(tabName, annotationPanelName);

			//////////      Add buttons to Panel    //////////

			// btn1. Generate button data
			var annotationButtonDataModel2 = new RevitPushButtonDataModel()
			{
				Label = "Toggle HalfTone",
				Panel = annotationPanel,
				Tooltip = "Toggle for current selected elements",
				IconImageName = "toggle_halftone.ico",
				IconLargeImageName = "toggle_halftone.ico",
				TooltipImageName = "toggle_halftone.ico",
				CommandNamespacePath = Revtec.core.Commands.Annotations.ToggleHalfTone.GetPath()
			};
			// btn1. Add button to panel
			RevitPushButton.Create(annotationButtonDataModel2);

			// btn2. ConvertCase - SplitButton
			var buttonGroup = new RevitSplitButton(annotationPanel, "n", "t");

			var annotationButtonSplitButtonData1 = new RevitPushButtonDataModel()
			{
				Label = "UPPER CASE",
				Panel = annotationPanel,
				Tooltip = "Convert all characters to capital letters",
				IconImageName = "all_caps.ico",
				IconLargeImageName = "all_caps.ico",
				TooltipImageName = "all_caps.ico",
				CommandNamespacePath = Revtec.core.Commands.Annotations.ConvertCaseUpper.GetPath()
			};
			var annotationButtonSplitButtonData2 = new RevitPushButtonDataModel()
			{
				Label = "Camel Case",
				Panel = annotationPanel,
				Tooltip = "Convert all characters to Camel Case",

				// TODO: fix the icons not showing in quick access toolbar
				IconImageName = "camel_case.ico",
				IconLargeImageName = "camel_case.ico",
				TooltipImageName = "camel_case.ico",

				CommandNamespacePath = Revtec.core.Commands.Annotations.ConvertCaseCamelCase.GetPath()
			};
			var annotationButtonSplitButtonData3 = new RevitPushButtonDataModel()
			{
				Label = "lower case",
				Panel = annotationPanel,
				Tooltip = "Convert all characters to lower Case",
				IconImageName = "lower_case.ico",
				IconLargeImageName = "lower_case.ico",
				TooltipImageName = "lower_case.ico",
				CommandNamespacePath = Revtec.core.Commands.Annotations.ConvertCaseLower.GetPath()
			};

			// btn2. Add PushButton's to SplitButton group
			buttonGroup.AddRevitPushButton(annotationButtonSplitButtonData1);
			buttonGroup.AddRevitPushButton(annotationButtonSplitButtonData2);
			buttonGroup.AddRevitPushButton(annotationButtonSplitButtonData3);


			//////////////////////////////////////////    "Parameters Panel"    //////////////////////////////////////// 
			const string eagleKitchenPanelName = "Parameters";
			var parametersPanel = app.CreateRibbonPanel(tabName, eagleKitchenPanelName);

			var linkSheetParamsToGlobalParamsButtonDataModel = new RevitPushButtonDataModel()
			{
				Label = "Fix SheetParams",
				Panel = parametersPanel,
				Tooltip = "Link All Sheet's parameters to the global parameters",
				IconImageName = "link_params.ico", // this is 16x16
				IconLargeImageName = "link_params.ico", // this is 32x32
				TooltipImageName = "link_params.ico", // this is 32x32
				CommandNamespacePath = Revtec.core.Commands.Parameters.LinkSheetParamsToGlobalParamsCommand.GetPath()
			};
			RevitPushButton.Create(linkSheetParamsToGlobalParamsButtonDataModel);

			//////////////////////////////////////////    "Selections" Panel    //////////////////////////////////////// 
			const string selectionPanelName = "Selection";
			var selectionPanel = app.CreateRibbonPanel(tabName, selectionPanelName);

			var selectionMainButtonDataModel = new RevitPushButtonDataModel()
			{
				Label = "All Sheets",
				Panel = selectionPanel,
				Tooltip = "Select all(only, not sheet category)sheets in project browser",
				IconImageName = "sheet.ico", // this is 16x16
				IconLargeImageName = "sheet.ico", // this is 32x32
				TooltipImageName = "sheet.ico", // this is 32x32
				CommandNamespacePath = Revtec.core.Commands.Selection.SelectAllSheets.GetPath()
			};
			RevitPushButton.Create(selectionMainButtonDataModel);


			//////////////////////////////////////////    "Dev" Panel    //////////////////////////////////////// 
			const string devPanelName = "DEV";
			var devPanel = app.CreateRibbonPanel(tabName, devPanelName);

			// Generate button data
			var devButtonDataModel1 = new RevitPushButtonDataModel()
			{
				Label = "Upgrade 24",
				Panel = devPanel,
				Tooltip = "Upgrade all files to 2024",
				IconImageName = "toggle_halftone.ico",
				IconLargeImageName = "toggle_halftone.ico",
				TooltipImageName = "toggle_halftone.ico",
				CommandNamespacePath = Revtec.core.Commands.Dev.Revit2024.Revit2024.GetPath()
			};

			// Add button to panel
			RevitPushButton.Create(devButtonDataModel1);

			//////////      Add buttons to Panel    //////////
			var devButton1 = RevitPushButton.Create(devButtonDataModel1) as RibbonItem;
			devButton1.Visible = false;
			// set this function based on the project
			devButton1.Enabled = false;


			var updateMirrorsModel = new RevitPushButtonDataModel()
			{
				Label = "Fix Mirror HandleSets",
				Panel = devPanel,
				Tooltip = "Update HandleSet Location for Mirrored instances",
				IconImageName = "mirror.ico",
				IconLargeImageName = "mirror.ico",
				TooltipImageName = "mirror.ico",
				CommandNamespacePath = Revtec.core.Commands.Dev.KitchenDesign.FixMirroredHandleSets.GetPath()
			};
			RevitPushButton.Create(updateMirrorsModel);
			//updateMirrors.Visible = false;
			//updateMirrors.Enabled = false;

			////////////////////////// Raw button -> Family only button///////////////////
			/*
            PushButtonData rawData = new PushButtonData("raw", "raw",
                Revtec.core.CoreAssembly.GetCoreAssemblyLocation(), Revtec.core.Commands.TestingLab.Test1.GetPath());
            rawData.LargeImage = Revtec.res.ResourceImage.GetIcon("button1.ico");
            rawData.AvailabilityClassName = "Revtec.core.Commands.TestingLab.CustomAvailability";

            testPanel.AddItem(rawData);
             *
             */

		}

		#endregion
	}
}
