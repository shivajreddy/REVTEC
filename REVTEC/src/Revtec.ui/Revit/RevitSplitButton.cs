using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace Revtec.ui.Revit
{
    public class RevitSplitButton
    {
        private SplitButton _group;
        private RibbonPanel _ribbonPanel;

        public RevitSplitButton(RibbonPanel panel, string name, string text)
        {
            _ribbonPanel = panel;
            var groupData = new SplitButtonData(name, text);
            _group = panel.AddItem(groupData) as SplitButton;
        }

        //private void AddSplitButtonGroup(RibbonPanel panel)
        public void AddRevitPushButton(RevitPushButtonDataModel data)
        {
            // copying following from RevitPushButton.cs
            // Create a name with a guid
            var btnDataName = Guid.NewGuid().ToString();
            // Set the button data
            var btnData = new PushButtonData(btnDataName, data.Label,
                Revtec.core.CoreAssembly.GetCoreAssemblyLocation(), data.CommandNamespacePath)
            {
                LargeImage = Revtec.res.ResourceImage.GetIcon(data.IconImageName),
                ToolTipImage = Revtec.res.ResourceImage.GetIcon(data.IconImageName)
            };

            _group.AddPushButton(btnData);
        }

    }
}
