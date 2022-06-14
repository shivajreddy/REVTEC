using System;
using System.Collections.Generic;
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

            // add a checkbox to the form
            CheckBox box1 = new CheckBox();
            //box1.Appearance = Appearance.Button;
            box1.Text = "yoooolo";
            //this.Controls.Add(box1);
            groupBox1.Controls.Add(box1);

            this.dataGridView1.DataSource = names;

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
