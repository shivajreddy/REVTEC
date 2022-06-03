using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Revtec.core.Commands.CreateStuff
{
    [Transaction(TransactionMode.Manual)]
    public class CreateBundleSheets : IExternalCommand
    {



        public List<Wall> GetWalls(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> Walls = collector.OfClass(typeof(Wall)).ToElements();
            List<Wall> AllWalls = new List<Wall>();

            foreach (Wall wall in Walls)
            {
                AllWalls.Add(wall);
            }


            return AllWalls;
        }

        public List<Element> GetTitleBlocks(Document doc)
        {
            FilteredElementCollector titleBlocksElementCollector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_TitleBlocks);
            ICollection<Element> allTitleBlocks = titleBlocksElementCollector.ToElements();

            List<Element> AllTitleBlocks = new List<Element>();

            foreach (Element titleBlock in allTitleBlocks)
            {
                AllTitleBlocks.Add(titleBlock);
            }
            return AllTitleBlocks;
        }




        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application documents
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Application app = uiApp.Application;
            Document doc = uiDoc.Document;


            Console.WriteLine("Hello world!!!!");

            var result = GetWalls(doc);
            var sb = SB(result);
            TaskDialog.Show("hi", sb.ToString());

            //var result = GetTitleBlocks(doc);
            //var sb2 = new StringBuilder();
            //foreach ( Element item in result)
            //{
            //    sb2.Append(item.Name + " " + "\n");
            //}

            //TaskDialog.Show("result", sb2.ToString());
            //var result = GetWalls(doc);
            //var sb = SB(result);
            //TaskDialog.Show("hi", sb.ToString());

            //var result = GetTitleBlocks(doc);

            //var sb2 = new StringBuilder();
            //foreach (Element item in result)
            //{
            //    sb2.Append(item.Name + " " + "\n");
            //}

            //TaskDialog.Show("result", sb2.ToString());

            return Result.Succeeded;
        }


        public static StringBuilder SB(List<Wall> walls)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var wall in walls)
            {
                sb.Append(wall.Name);
            }

            return sb;
        }


        /// <summary>
        /// Gets the namespace path of this command
        /// </summary>
        /// <returns></returns>
        public static string GetPath()
        {
            var commandName = typeof(CreateBundleSheets).Namespace + "." + nameof(CreateBundleSheets);
            return commandName;
        }
    }
}
