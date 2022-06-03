using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Revtec.core.Commands.Annotations.Helpers;
using Revtec.core.Commands.Selection;
using Revtec.core.Commands.Type;

namespace Revtec.core.Commands.Annotations
{
    [Transaction(TransactionMode.Manual)]
    public class TagWallLayer : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Application data
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiApp.ActiveUIDocument.Document;


            // Check if family
            if (doc.IsFamilyDocument)
            {
                Message.Display("cant use command in family", WindowType.Warning);
                return Result.Cancelled;
            }

            // Get the current view
            var activeView = uiApp.ActiveUIDocument.ActiveView;

            bool canCreateView = false;
            switch (activeView.ViewType)
            {
                case ViewType.FloorPlan:
                    canCreateView = true;
                    break;
                case ViewType.CeilingPlan:
                    canCreateView = true;
                    break;
                case ViewType.Detail:
                    canCreateView = true;
                    break;
            }

            if (!canCreateView)
            {
                Message.Display("Text note element", WindowType.Error);
                return Result.Cancelled;
            }

            // Ask user to select one basic wall
            var selectionReference = uiApp.ActiveUIDocument.Selection.PickObject(ObjectType.Element,
                new SelectionFilterByCategory("Wall"), "select one basic wall");

            var selectionElement = doc.GetElement(selectionReference);

            var wall = selectionElement as Wall;

            // Check if wall is of type basic wall
            if (wall.IsStackedWall)
            {
                Message.Display("No stacked walls", WindowType.Warning);
            }


            // Ask user to pick location point
            var pt = uiDoc.Selection.PickPoint("Pick a point");

            // Access list of wall layers.
            var layers = wall.WallType.GetCompoundStructure().GetLayers();

            // Get layer information in structured string format for Text note.
            var msg = new StringBuilder();

            foreach (var layer in layers)
            {
                var material = doc.GetElement(layer.MaterialId) as Material;
                msg.AppendLine( layer.Function.ToString() + " " + material.Name + "" + layer.Width.ToString() );
            }

            // create text note options
            var textNoteOptions = new TextNoteOptions
            {
                VerticalAlignment = VerticalTextAlignment.Top,
                HorizontalAlignment = HorizontalTextAlignment.Left,
                TypeId = doc.GetDefaultElementTypeId(ElementTypeGroup.TextNoteType)
            };

            using (var tx = new Transaction(doc))
            {
                tx.Start("Tag the wall layer");
                var textNote = TextNote.Create(doc, activeView.Id, pt, msg.ToString(), textNoteOptions);
                tx.Commit();

            }



            return Result.Succeeded;
        }


        /// <summary>
        /// Gets the namespace path of this command
        /// </summary>
        /// <returns></returns>
        public static string GetPath()
        {
            var commandName = typeof(TagWallLayer).Namespace + "." + nameof(TagWallLayer);
            return commandName;
        }

    }
}
