using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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

            // get the category names
            List<string> categoryNames = Custom.Greet(doc);

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


            // set the category names 
            DataTable dt = new DataTable();
            dt.Columns.Add("CategoryName", typeof(string));
            dt.Columns.Add("FamilyName", typeof(string));
            dt.Columns.Add("ElementId", typeof(int));
            dt.Columns.Add("Selection", typeof(bool));

            DataRow row = dt.NewRow();

            row["CategoryName"] = "test cat1";
            row["FamilyName"] = "test fam1";
            row["ElementId"] = 123;
            row["Selection"] = false;

            dt.Rows.Add(row);


            this.dataGridView1.DataSource = dt;

        }

        public class Custom
        {
            public static List<string> Greet(Document doc)
            {
                // Get the Category Names
                FilteredElementCollector collector = new FilteredElementCollector(doc);

                ICollection<Element> elements = collector.OfClass(typeof(Family)).ToElements();

                List<string> familyNames = new List<string>();
                List<string> categoryNames = new List<string>();

                foreach (var element in elements)
                {
                    Family family = element as Family;
                    Category category = family.FamilyCategory;

                    familyNames.Add(family.Name.ToString());
                    categoryNames.Add(category.Name.ToString());
                }

                return categoryNames;
            }

        }


        private void btnOk_Click(object sender, EventArgs e)
        {

            TaskDialog.Show("blast", "kaboom activated");

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
