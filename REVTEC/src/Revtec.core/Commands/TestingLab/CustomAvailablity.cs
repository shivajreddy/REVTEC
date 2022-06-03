using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revtec.core.Commands.TestingLab
{
    /// <summary>
    /// Implement this class on Commands, to control their visibility
    /// </summary>
    public class CustomAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            Document doc = applicationData.ActiveUIDocument.Document;

            Categories allWalls = applicationData.ActiveUIDocument.Document.Settings.Categories;

            Category wallCat = allWalls.get_Item(BuiltInCategory.OST_Walls);
            if (selectedCategories.Contains(wallCat))
            {
                return true;
            }

            // Family
            if (doc.IsFamilyDocument)
            {
                return false;
            }

            // Project
            return false;
        }
    }
}