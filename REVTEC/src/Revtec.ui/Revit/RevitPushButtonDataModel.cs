using Autodesk.Revit.UI;

namespace Revtec.ui.Revit
{
    /// <summary>
    /// Represents Revit push button data model
    /// </summary>
    public class RevitPushButtonDataModel
    {
        #region public methods

        public string Label { get; set; }
        
        public RibbonPanel Panel { get; set; }

        public string CommandNamespacePath { get; set; }

        public string Tooltip { get; set; }

        public string LongDescription { get; set; } // long description of command tool tip

        public string TooltipImageName { get; set; }
        
            
        public string IconImageName { get; set; }   // 16x16 .ico image
        public string IconLargeImageName { get; set; }  // 32x32 .ico image



        #endregion

        #region constructor


        /// <summary>
        /// Default constructor
        /// </summary>
        public RevitPushButtonDataModel()
        {

        }
        

        #endregion
    }
}
