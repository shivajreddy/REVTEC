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

            if (doc.IsFamilyDocument)
                return true;
            return false;
        }
    }
}