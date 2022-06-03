using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Form = System.Windows.Forms.Form;
using TextBox = System.Windows.Forms.TextBox;

namespace Revtec.core.Commands.TestingLab
{
    public partial class Form1 : Form
    {

        #region Private members

        public Document Doc;
        private UIDocument uiDoc = null;

        #endregion

        #region props

        public string val1 { get; set; }

        #endregion

        #region Constructor

        public Form1(Document doc)
        {
            InitializeComponent();
            Doc = doc;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // 

            IList<Element> titleBlockTypes = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_TitleBlocks)
                .WhereElementIsElementType().ToElements();

            // Set the drop down menu 
            IList<string> projectTitleBlocks = new List<string>();

            foreach (Element titleBlock in titleBlockTypes)
            {
                projectTitleBlocks.Add(titleBlock.Name);
            }

            this.usrTitleBlock.DataSource = projectTitleBlocks;

            //this.usrTitleBlock.DataSource = por

        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            //Close();

            //TaskDialog.Show("Ok Task", "button is clicked");
            
            // usrNumber
            val1 = usrNumber.Text;

            IList<Element> titleBlockTypes = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_TitleBlocks)
                .WhereElementIsElementType().ToElements();

            // Set the drop down menu 
            IList<string> projectTitleBlocks = new List<string>();

            foreach (Element titleBlock in titleBlockTypes)
            {
                projectTitleBlocks.Add(titleBlock.Name);
            }

            this.usrTitleBlock.DataSource = projectTitleBlocks;



            string selectedTblock = this.usrTitleBlock.SelectedItem.ToString();

            TaskDialog.Show("final", selectedTblock);



        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private  void label2_Click(object sender, EventArgs e){}

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
