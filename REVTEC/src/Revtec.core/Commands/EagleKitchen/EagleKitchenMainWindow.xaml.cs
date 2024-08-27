using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace Revtec.core.Commands.EagleKitchen
{
    /// <summary>
    /// Interaction logic for EagleKitchenMainWindow.xaml
    /// </summary>
    public partial class EagleKitchenMainWindow : Window
    {
        public UIDocument uiDoc;
        public Document doc;
        public bool AddToCurrentSelection;

        public EagleKitchenMainWindow(ExternalCommandData commandData)
        {
            uiDoc = commandData.Application.ActiveUIDocument;
            doc = uiDoc.Document;

            InitializeComponent();
        }


        public void UpdateSelection(IEnumerable<ElementId> selectElementIds, Document doc)
        {
            var elementIds = selectElementIds as ElementId[] ?? selectElementIds.ToArray();
            if (!elementIds.Any())
            {
                SelectedItems.Text = "";
            }
            else
            {
                // Convert each ElementId to a string and collect them in a list
                List<string> elementIdStrings = new List<string>();
                foreach (ElementId id in elementIds)
                {
                    elementIdStrings.Add(id.IntegerValue.ToString()); // Convert ElementId to string
                }

                // Join all string representations with a delimiter, e.g., ", "
                string concatenatedElementIds = string.Join(", ", elementIdStrings);

                //Element firstSelectElement = doc.GetElement(elementIds.First());
                SelectedItems.Text = $"Selected: {concatenatedElementIds}";
            }
        }

        // Helper function to select all instances of a given family
        //public void SelectAllInstances(string target_family_name)
        public void SelectAllInstances(string[] target_family_names)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance))
                .OfCategory(BuiltInCategory.OST_Casework);

            List<ElementId> elementsToSelect = new List<ElementId>();

            foreach (Element element in collector)
            {
                FamilyInstance instance = element as FamilyInstance;
                if (instance != null)
                {
                    // check if the family name matches the target family name
                    Family family = instance.Symbol.Family;

                    bool containsAll = target_family_names.All(name => family.Name.ToLower().Contains(name.ToLower()));
                    if (containsAll)
                    {
                        elementsToSelect.Add(element.Id);
                    }
                }
            }


            if (elementsToSelect.Count <= 0) return;

            if (AddToCurrentSelection)
            {
                //uiDoc.Selection.SetElementIds(new List<ElementId>());   // un select all
                ICollection<ElementId> currentSelection = uiDoc.Selection.GetElementIds();
                List<ElementId> updatedSelection = new List<ElementId>(currentSelection);
                updatedSelection.AddRange(elementsToSelect);

                uiDoc.Selection.SetElementIds(updatedSelection);
            }
            else
            {
                //uiDoc.Selection.SetElementIds(new List<ElementId>());   // un select all
                uiDoc.Selection.SetElementIds(elementsToSelect);
            }

        }

        // Select all families of given door configuration 
        private void Button_1Door(object sender, RoutedEventArgs e)
        {
            SelectAllInstances(new String[] {"OneDoor"});
        }

        private void Button_1Door_1Drawer(object sender, RoutedEventArgs e)
        {
            SelectAllInstances(new String[] {"OneDoor", "OneDrawer"});
        }

        private void Button_2Doors(object sender, RoutedEventArgs e)
        {
            SelectAllInstances(new String[] {"TwoDoor"});
        }

        private void Button_2Doors_1Drawer(object sender, RoutedEventArgs e)
        {
            SelectAllInstances(new String[] {"TwoDoor", "OneDrawer"});
        }

        private void Button_2Doors_2Drawers(object sender, RoutedEventArgs e)
        {
            SelectAllInstances(new String[] {"TwoDoor", "TwoDrawers"});
        }

        private void Add_to_current_selection(object sender, RoutedEventArgs e)
        {
            AddToCurrentSelection = true;
        }
        private void Dont_Add_to_current_selection(object sender, RoutedEventArgs e)
        {
            AddToCurrentSelection = false;
        }

        private void SetStyleForSelection(object sender, RoutedEventArgs e)
        {

            var btn = sender as Button;

            if (btn != null)
            {
                var newStyleName = btn.Tag.ToString();

                ICollection<ElementId> currCollection = uiDoc.Selection.GetElementIds();
                UpdateStylesOfElements(currCollection, newStyleName);
            }

            
        }

        private void UpdateStylesOfElements(ICollection<ElementId> elementIds, string targetStyleName)
        {
            using (Transaction trans = new Transaction(doc, "Update Style"))
            {
                trans.Start();

                foreach (var elementId in elementIds)
                {
                    var elem = doc.GetElement(elementId);
                    

                    if (elem is FamilyInstance)
                    {
                        var inst = (FamilyInstance) elem;
                        Parameter styleParameter = inst.LookupParameter("Casework_Style");
                        if (styleParameter != null)
                        {
                            styleParameter.Set(targetStyleName);
                        }
                    }
                }
                trans.Commit();
            }


        }
    }

}
