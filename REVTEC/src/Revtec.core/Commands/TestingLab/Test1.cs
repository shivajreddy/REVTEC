using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revtec.core.Commands.TestingLab
{
    [Transaction(TransactionMode.Manual)]
    public class Test1 : IExternalCommand
    {

        /// <summary>
        /// The code for command button goes in this execute method.
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Command context
            var uiDoc = commandData.Application.ActiveUIDocument;
            var doc = uiDoc.Document;

            //////////      Testing for Family Manager      //////////
            var path = @"C:\Users\sreddy\Desktop\TestingFiles\TestFam1.rfa";

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Load Fam");


                doc.LoadFamily(path, new MyFamilyLoadOptions(), out Family loadedFamily);

                // TODO Load the window



                // TODO Register a new service using IUpdater


                tx.Commit();
            }


            //var loadedString = loadedFamily.Name;

            //TaskDialog.Show("title", loadedString);




            return Result.Succeeded;
        }

        public class MyFamilyLoadOptions : IFamilyLoadOptions
        {
            public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
            {
                //throw new NotImplementedException();

                if (familyInUse)
                {
                    overwriteParameterValues = true;
                    TaskDialog.Show("Found", "Family is found and replaced");
                    return true;
                }

                overwriteParameterValues = false;
                return false;
            }

            public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source,
                out bool overwriteParameterValues)
            {
                throw new NotImplementedException();
            }
        }



        # region GetPath Method

        public static string GetPath()
        {
            var commandName = typeof(Test1).Namespace + "." + nameof(Test1);
            return commandName;
        }

        #endregion
    }
}
