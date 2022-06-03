using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace Revtec.core.Commands.Selection
{
    public class SelectionFilterByCategory : ISelectionFilter
    {

        #region private members

        private string mCategory;

        public SelectionFilterByCategory(string category)
        {
            mCategory = category;
        }

        #endregion


        public bool AllowElement(Element elem)
        {
            if (elem.Category.Name == mCategory)
            {
                return true;
            }

            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
