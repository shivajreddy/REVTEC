using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revtec.core.Commands.CreateStuff
{
    public partial class NewBundleSheet : System.Windows.Forms.Form
    {
        // App Doc
        public Document Doc;
        public UIDocument uiDoc = null; 

        // Props 
        public int NumberOfSheets { get; set; }
        public string TitleBlockName { get; set; }
        public string PrefixSheetNumberPart1 { get; set; }
        public string PrefixSheetNumberPart2 { get; set; }
        public string PrefixSheetName { get; set; }
        public bool LinkChoice { get; set; }

        // Constructor
        public NewBundleSheet(Document doc)
        {
            InitializeComponent();
            Doc = doc;

            // Generic List of OST_TITLE_BLOCK elements
            IList<Element> titleBlocks = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_TitleBlocks)
                .WhereElementIsElementType().ToElements();

            // Generic List of names of the above List
            IList<string> titleBlocksNames = new List<string>();
            foreach (var titleBlock in titleBlocks)
            {
                titleBlocksNames.Add(titleBlock.Name.ToString());
            }

            // Set the drop down values
            this.dropDown.DataSource = titleBlocksNames;
        }

        // Methods
        public ViewSheet CreateSheet(Element titleBlock, string sheetNumber, string sheetName)
        {
            // Create sheet list to be used
            try
            {
                ViewSchedule testSheetList = ViewSchedule.CreateSheetList(Doc);
                ViewSheet newSheet = ViewSheet.Create(Doc, titleBlock.Id);
                newSheet.SheetNumber = sheetNumber;
                newSheet.Name = sheetName;
                return newSheet;
            }
            catch (Exception e)
            {
                TaskDialog.Show("Cant create sheet bundle", $"{sheetNumber} already exists");
                throw;
            }
        }

        public void CreateSheetBundle()
        {
            // 1. List of all title block elements
            IList<Element> titleBlocks = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_TitleBlocks)
                .WhereElementIsElementType().ToElements();

            // 2. Get the Title block that user chose
            Element chosenTitleBlock = null;
            foreach (var titleBlock in titleBlocks)
            {
                if (titleBlock.Name.ToString() == TitleBlockName)
                {
                    chosenTitleBlock= titleBlock;
                }
            }

            // 3. Create List of SheetNumbers 
            var sheetNumbers = GenerateSheetNumbers(PrefixSheetNumberPart1, PrefixSheetNumberPart2, NumberOfSheets);

            Console.WriteLine(sheetNumbers);
            // 4. Loop n times, inside the transaction. Each time -> Create sheet, set number, set name
            using (Transaction ts = new Transaction(Doc, "Create Bundle Sheets"))
            {
                ts.Start();

                // loop of creating multiple sheets
                for (int idx = 0; idx < NumberOfSheets; idx++)
                {
                    var newSheetNumber = sheetNumbers[idx];
                    var newSheetName = PrefixSheetName;
                    ViewSheet newSheet = CreateSheet( chosenTitleBlock, newSheetNumber, newSheetName);
                }

                ts.Commit();
            }
        }

        // Method to generate Numbers based on input
        public List<string> GenerateSheetNumbers(string part1, string part2, int count)
        {
            List<string> result = new List<string>();

            int startingNumber;
            try
            {
                bool flag = Int32.TryParse(part2, out startingNumber);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            for (int i = 0; i < count; i++)
            {
                int l = startingNumber.ToString().Length;

                string finalNumber ="";

                if (l == 1)
                {
                    finalNumber = part1 + "00" + startingNumber.ToString();
                    result.Add(finalNumber);
                } else if (l == 2)
                {
                    finalNumber = part1 + "0" + startingNumber.ToString();
                    result.Add(finalNumber);
                } else if (l == 3)
                {
                    finalNumber = part1 + startingNumber.ToString();
                    result.Add(finalNumber);
                }
                startingNumber++;
            }
            return result;
        }


        // OK button method
        private void btnOk_Click(object sender, EventArgs e)
        {
            // Set Properties
            NumberOfSheets = (int) this.userNumber.Value;
            TitleBlockName = this.dropDown.SelectedItem.ToString();
            PrefixSheetNumberPart1 = this.userSheetNumberPart1.Text;
            PrefixSheetNumberPart2 = this.userSheetNumberPart2.Text;
            PrefixSheetName = this.userSheetName.Text;
            LinkChoice = this.checkBox1.Checked;

            //string result = $"{NumberOfSheets} {TitleBlockName} {PrefixSheetNumberPart1}{PrefixSheetNumberPart2} {PrefixSheetName} {LinkChoice}";
            //TaskDialog.Show("creating sheet with these details", result);

            // Call the method to create sheet bundle
            CreateSheetBundle();

            this.DialogResult = DialogResult.OK;
            Close();
        }

        // CANCEL button method
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
