using System;
using System.Security.Cryptography.X509Certificates;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revtec.core.Commands.TestingLab
{
    [Transaction(TransactionMode.Manual)]
    public class Test1 : IExternalCommand
    {

        /// <summary>
        /// The code for command button goes in this execute method.
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Command context
            var uiDoc = commandData.Application.ActiveUIDocument;
            var doc = uiDoc.Document;


            // Check if we are in revit project, not revit family
            if (doc.IsFamilyDocument)
            {

                TaskDialog.Show("cant use in family", "no family only project");

                return Result.Failed;
            }




            var win = new TaskDialog("info")
            {
                MainContent = "Hi this is testing",
                MainIcon = TaskDialogIcon.TaskDialogIconShield,
                CommonButtons = TaskDialogCommonButtons.Ok
            };
            win.Show();

            return Result.Succeeded;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetPath()
        {
            var commandName = typeof(Test1).Namespace + "." + nameof(Test1);
            return commandName;
        }

    }
}
