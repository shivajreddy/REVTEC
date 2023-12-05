
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
            var btnData = new PushButtonData(btnDataName, data.Label, Revtec.core.CoreAssembly.GetCoreAssemblyLocation(), data.CommandNamespacePath)
            {
                // 16x16 .icon image
                Image = Revtec.res.ResourceImage.GetIcon(data.IconImageName),   
                // 32x32 .icon image
                LargeImage = Revtec.res.ResourceImage.GetIcon(data.IconLargeImageName),
                // 32x32 .icon image, same as above should be fine
                ToolTipImage = Revtec.res.ResourceImage.GetIcon(data.IconLargeImageName),

                ToolTip = data.Tooltip,

                LongDescription = data.LongDescription,


            };

            // Set the availability class here
            //btnData.AvailabilityClassName = "Revtec.ui.Revit.CustomAvailability";

            return data.Panel.AddItem(btnData) as PushButton;
        }

        #endregion
    }
}
