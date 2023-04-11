using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Document = Autodesk.Revit.Creation.Document;

namespace Revtec.core.Commands.Annotations
{
    [Transaction(TransactionMode.Manual)]
    public class ConvertCase: IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiDoc = commandData.Application.ActiveUIDocument;
            var doc = uiDoc.Document;

            using (Transaction tx = new Transaction(doc, "Convert Case"))
            {
                tx.Start();

                // Get the selected text
                // Get the IDs of the selected elements
                ICollection<ElementId> selectedIds = uiDoc.Selection.GetElementIds();

                foreach (ElementId id in selectedIds)
                {
                    // Get the corresponding Revit element
                    Element elem = doc.GetElement(id);

                    // Check if the element is a text element
                    if (elem is TextElement)
                    {
                        // Get the text note object
                        TextNote textNote = (TextNote) elem;

                    }
                }

                tx.Commit();
            }
            return Result.Succeeded;
        }


        public static string ToAllCaps(Document doc)
        {


            string inputString = "";
            return inputString.ToUpper();
        }

        public static string ToCamelCase(string inputString)
        {
            //string inputString = "TWIN I.L.O DOUBLE HUNG";
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string titleCaseString = textInfo.ToTitleCase(inputString.ToLower());
            //Console.WriteLine(titleCaseString); // Output: Twin I.L.O Double Hung

            return titleCaseString;
        }

        public static string GetPath()
        {
            var commandName = typeof(ToggleHalfTone).Namespace + "." + nameof(ToggleHalfTone);
            return commandName;
        }

    }
}
