
using System;
using Autodesk.Revit.UI;

//using Revtec.core;
//using Revtec.res;

namespace Revtec.ui.Revit
{
    /// <summary>
    /// The Revit push button methods
    /// </summary>
    public class RevitPushButton
    {
        #region public methods

        /// <summary>
        /// Create the push button based on data provided in <see cref="RevitPushButtonDataModel"/>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static PushButton Create(RevitPushButtonDataModel data)
        {
            // Create a name with a guid
            var btnDataName = Guid.NewGuid().ToString();

            // Sets the button data
            var btnData = new PushButtonData(btnDataName, data.Label,
                Revtec.core.CoreAssembly.GetCoreAssemblyLocation(), data.CommandNamespacePath)
            {
                LargeImage = Revtec.res.ResourceImage.GetIcon(data.IconImageName),
                ToolTipImage = Revtec.res.ResourceImage.GetIcon(data.IconImageName)
            };

            return data.Panel.AddItem(btnData) as PushButton;
        }

        #endregion
    }
}
