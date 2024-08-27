using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revtec;

namespace Revtec.core.Commands.EagleKitchen
{
    [Transaction(TransactionMode.Manual)]
    public class Main: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // original that works
            var eagleKitchenWindow = new EagleKitchenMainWindow(commandData);
            eagleKitchenWindow.Show();


            /*
            string path = Assembly.GetExecutingAssembly().Location;
            string execConfigPath = Path.GetDirectoryName(path) + "..\\..\\..\\..\\" + "myModule\\AddIn\\myModule.dll";
            string strCommandName = "This Application";
            byte[] assemblyBytes = File.ReadAllBytes(execConfigPath);
            Assembly objAssembly = Assembly.Load(assemblyBytes);
            IEnumerable<System.Type> myIEnumerableType = GetTypesSafely(objAssembly);
            foreach (var VARIABLE in COLLECTION)
            {
                k
            }
            */




            return Result.Succeeded;
        }


        // method to get the namespace path, for registering the button on Revtec plugin
        public static string GetPath()
        {
            return typeof(Main).Namespace + "." + nameof(Main);
        }

    }


}
