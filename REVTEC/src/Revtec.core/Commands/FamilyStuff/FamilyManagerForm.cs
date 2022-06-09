using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Form = System.Windows.Forms.Form;

namespace Revtec.core.Commands.FamilyStuff
{
    // WindowsForm for Family Manager command
    public partial class FamilyManagerForm : Form
    {

        // App & Doc
        public Document Doc;
        public UIDocument uiDoc = null;     // Initiate uiDoc object with null


        // Constructor
        public FamilyManagerForm(Document doc)
        {
            InitializeComponent();
            Doc = doc;

            // Show the existing families
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            

            collector.OfClass(typeof(Family));

            IList<Element> FamilySymbols = collector.ToElements();

            List<string> names = new List<string>();
            names.Add(FamilySymbols.Count.ToString());

            List<string> catNames = new List<string>();

            foreach (var familySymbol in FamilySymbols)
            {
                names.Add(familySymbol.Name.ToString());
            }
            this.listBox1.DataSource = names;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            TaskDialog.Show("blast", "kaboom activated");

        }
    }
}
