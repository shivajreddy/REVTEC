using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
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

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            //ICollection<Element> Walls = collector.OfClass(typeof(Wall)).ToElements();

            //var selection = uiDoc.Selection;
            //ICollection<ElementId> selectedIds = uiDoc.Selection.GetElementIds();

            //if (0 == selectedIds.Count)
            //    TaskDialog.Show("Revit", "No items selected");
            //else
            //{
            //    String info = "Ids of selected elements in the document are: ";
            //    foreach (ElementId id in selectedIds)
            //    {
            //        info += "\n\t" + id;
            //    }
            //    TaskDialog.Show("Revit",info);
            //}

            using (System.Windows.Forms.Form form = new Form1(doc))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    TaskDialog.Show("asdhf", form.Location.ToString());
                    return Result.Succeeded;
                }
            }

            //var f1 = new Form1();
            //f1.Show();

            //TaskDialog.Show("User choice", f1.val1);






            //var win = new TaskDialog("info")
            //{
            //    MainContent = "Hi this is testing",
            //    MainIcon = TaskDialogIcon.TaskDialogIconShield,
            //    CommonButtons = TaskDialogCommonButtons.Ok
            //};
            //win.Show();

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
