using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revtec.core.Commands.Annotations
{
    [Transaction(TransactionMode.Manual)]
    public class ConvertCaseCamelCase : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var uiDoc = commandData.Application.ActiveUIDocument;
            var doc = uiDoc.Document;

            using (Transaction tx = new Transaction(doc, "Convert Case to Lower"))
            {
                tx.Start();

                // Get the selected text i.e,. Get the IDs of the selected elements
                ICollection<ElementId> selectedIds = uiDoc.Selection.GetElementIds();

                foreach (var elementId in selectedIds)
                {
                    Element elem = doc.GetElement(elementId);

                    if (elem is TextElement)
                    {
                        // Get the text note object
                        TextNote textNote = (TextNote) elem;

                        string text = textNote.Text;

                        // call the helper method
                        string result = ConvertCase.ToCamelCase(text);

                        FormattedText s = new FormattedText(result);
                        textNote.SetFormattedText(s);
                    }
                }

                tx.Commit();
            }
            return Result.Succeeded;
        }
        public static string GetPath()
        {
            var commandName = typeof(ConvertCaseCamelCase).Namespace + "." + nameof(ConvertCaseCamelCase);
            return commandName;
        }





    }
}
