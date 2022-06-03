using Autodesk.Revit.UI;
using Revtec.core.Commands.Type;

namespace Revtec.core.Commands.Annotations.Helpers

{
    /// <summary>
    /// Display messages helper methods.
    /// </summary>
    public static class Message
    {

        #region public methods

        public static void Display(string message, WindowType type)
        {
            string title = "";
            var icon = TaskDialogIcon.TaskDialogIconNone;


            // Customize the window based on the message
            switch (type)
            {
                case WindowType.Information:
                    title = "** Information **";
                    icon = TaskDialogIcon.TaskDialogIconInformation;
                    
                    break;
                case WindowType.Warning:
                    title = " Warning ";
                    icon = TaskDialogIcon.TaskDialogIconWarning;
                    break;
                case WindowType.Error:
                    title = " Error ";
                    icon = TaskDialogIcon.TaskDialogIconError;
                    break;
                default:
                    break;
                    
            }

            // The window to display
            var window = new TaskDialog(title)
            {
                MainContent = message,
                MainIcon = icon,
                CommonButtons = TaskDialogCommonButtons.Ok
            };
            window.Show();

        }

        #endregion

    }
}
