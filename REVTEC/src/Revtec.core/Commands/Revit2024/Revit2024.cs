using System;
using System.Diagnostics;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace Revtec.core.Commands.Revit2024
{
    [Transaction(TransactionMode.Manual)]
    public class Revit2024 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Debug.WriteLine("hello revit 2024 is executed");
            // search for all files 
            string rootPath = "\"C:\\Users\\sreddy\\Desktop\\2024\\AQT-S5_Lot 06_HTX-40-V3.rvt\""


            return Result.Succeeded;
        }





        // method to get the namespace path
        public static string GetPath()
        {
            return typeof(Revit2024).Namespace + "." + nameof(Revit2024);
        }

    }
}