using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            TaskDialog.Show("blast", "kaboom activated");

        }
    }
}
