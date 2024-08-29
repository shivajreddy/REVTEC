using Autodesk.Revit.DB;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms; // Namespace for FolderBrowserDialog
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using System.Collections.ObjectModel;

namespace Revtec.core.Commands.FamilyStuff
{
    public partial class FamilyReloaderView : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // Private backing fields
        private string _sourceFolderPath;
        private string _targetFolderPath;
        //private List<string> _successResults;
        //private List<string> _failedResults;
        private ObservableCollection<string> _successResults;
        private ObservableCollection<string> _failedResults;
        private List<string> _sourceFamilyNames;
        private List<string> _targetFamilyNamesWithExtension;
        private Document _doc;

        // Public properties with backing fields
        public string SourceFolderPath
        {
            get { return _sourceFolderPath; }
            set
            {
                _sourceFolderPath = value;
                OnPropertyChanged(nameof(SourceFolderPath));
            }
        }

        public string TargetFolderPath
        {
            get { return _targetFolderPath; }
            set
            {
                _targetFolderPath = value;
                OnPropertyChanged(nameof(TargetFolderPath));
            }
        }

        public List<string> SourceFamilyNames
        {
            get { return _sourceFamilyNames; }
            set
            {
                _sourceFamilyNames = value;
                OnPropertyChanged(nameof(SourceFamilyNames));
            }
        }

        public List<string> TargetFamilyNamesWithExtension
        {
            get { return _targetFamilyNamesWithExtension; }
            set
            {
                _targetFamilyNamesWithExtension = value;
                OnPropertyChanged(nameof(TargetFamilyNamesWithExtension));
            }
        }
        public ObservableCollection<string> SuccessResults
        {
            get { return _successResults; }
            set
            {
                _successResults = value;
                OnPropertyChanged(nameof(SuccessResults));
            }
        }
        public ObservableCollection<string> FailedResults
        {
            get { return _failedResults; }
            set
            {
                _failedResults = value;
                OnPropertyChanged(nameof(FailedResults));
            }
        }
        public int SuccessCount => SuccessResults?.Count ?? 0;
        public int FailedCount => FailedResults?.Count ?? 0;




        private void Source_Button(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.Description = "Select the source folder";
                dialog.ShowNewFolderButton = false;

                var result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    SourceFolderPath = dialog.SelectedPath;
                }
            }
        }

        private void Target_Button(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.Description = "Select the target folder";
                dialog.ShowNewFolderButton = false;

                var result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    TargetFolderPath = dialog.SelectedPath;
                }
            }
        }

        private void FetchSourceFamilies_Button(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SourceFolderPath))
            {
                SourceFamilyNames = GetRFAFiles(SourceFolderPath)
                    .Select(f => Path.GetFileNameWithoutExtension(f))
                    .ToList();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a source folder.");
            }
        }



        private void FetchTargetFamilies_Button(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TargetFolderPath))
            {
                if (RfaRadioButton.IsChecked == true)
                {
                    // Fetch RFA files
                    TargetFamilyNamesWithExtension = GetRFAFiles(TargetFolderPath);
                }
                else if (RvtRadioButton.IsChecked == true)
                {
                    // Fetch RVT files
                    TargetFamilyNamesWithExtension = GetRVTFiles(TargetFolderPath);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a target folder.");
            }
        }


        private void Reload_Button(object sender, RoutedEventArgs e)
        {
            //// Testing
            //SourceFolderPath = "C:\\Users\\sreddy\\Desktop\\Test Script\\Panel Styles";
            //TargetFolderPath = "C:\\Users\\sreddy\\Desktop\\Test Script\\Panel Families";

            // Implement the reloading logic
            if (!string.IsNullOrEmpty(SourceFolderPath) && !string.IsNullOrEmpty(TargetFolderPath))
            {
                ////sourceFamilyNames;
                //TargetFamilyNamesWithExtension;


                if (SourceFamilyNames.Count == 0)
                {
                    System.Windows.MessageBox.Show("There are no families in Source folder.");
                    return;
                }
                if (TargetFamilyNamesWithExtension.Count == 0)
                {
                    System.Windows.MessageBox.Show("There are no families in Target folder.");
                    return;
                }

                // Step 3: Reload the matching families
                Tuple<List<string>, List<string>> allResults = ReloadFamilies();

                // Update SuccessResults
                SuccessResults.Clear();
                foreach (var success in allResults.Item1)
                {
                    SuccessResults.Add(success);
                }

                // Update FailedResults
                FailedResults.Clear();
                foreach (var failure in allResults.Item2)
                {
                    FailedResults.Add(failure);
                }

            }
            else
            {
                System.Windows.MessageBox.Show("Please select both source and target folders.");
            }
        }


        private List<string> GetRFAFiles(string folder)
        {
            var rfaFiles = new List<string>();

            // Regex pattern to ignore backup files
            //var backupFilePattern = new Regex(@"^.+\.\d{4}\.rfa$");
            var backupFilePattern = new Regex(@"^.+\.\d{4}\..+$");

            // Ignore Backup Files
            foreach (var file in Directory.GetFiles(folder, "*.rfa"))
            {
                if (!backupFilePattern.IsMatch(file))
                {
                    rfaFiles.Add(Path.GetFileName(file));
                }
            }
            return rfaFiles;
        }
        private List<string> GetRVTFiles(string folder)
        {
            var rvtFiles = new List<string>();

            // Regex pattern to ignore backup files
            //var backupFilePattern = new Regex(@"^.+\.\d{4}\.rfa$");
            var backupFilePattern = new Regex(@"^.+\.\d{4}\..+$");

            // Ignore Backup Files
            foreach (var file in Directory.GetFiles(folder, "*.rvt"))
            {
                if (!backupFilePattern.IsMatch(file))
                {
                    rvtFiles.Add(Path.GetFileName(file));
                }
            }
            return rvtFiles;
        }


        private Tuple<List<string>, List<string>> ReloadFamilies()
        {
            var successResults = new List<string>();
            var failedResults = new List<string>();

            foreach (var targetFamily in TargetFamilyNamesWithExtension)
            {
                string targetFamilyPath = Path.Combine(TargetFolderPath, targetFamily);

                // Attempt to open the target family as a temporary document
                Document targetFamilyDoc = null;
                try
                {
                    //_doc is the project that you are working on, so using that get the current active Revit process
                    targetFamilyDoc = _doc.Application.OpenDocumentFile(targetFamilyPath);
                }
                catch (Exception ex)
                {
                    // If the target family fails to open, log the error and continue to the next one
                    failedResults.Add($"{targetFamily}:: Error opening family: {ex.Message}");
                    continue;
                }

                List<string> loadedFamiliesToReupload = FilterLoadedFamiliesForGivenDocument(targetFamilyDoc);

                /* // Testing
                foreach (var loadedFamily in loadedFamiliesToReupload)
                {
                    successResults.Add($"{targetFamily}::{loadedFamily}: this will be reloaded");
                }
                continue;
                */

                using (Transaction t = new Transaction(targetFamilyDoc, "Auto ReLoad Family"))
                {
                    t.Start();

                    foreach (var loadedFamily in loadedFamiliesToReupload)
                    {
                        string sourceFamilyPath = Path.Combine(SourceFolderPath, $"{loadedFamily}.rfa");

                        FamilyLoadOptionsHandler familyOptionsHandler = new FamilyLoadOptionsHandler();
                        Family loadedFamilyInstance;
                        bool loadResult = targetFamilyDoc.LoadFamily(sourceFamilyPath, familyOptionsHandler, out loadedFamilyInstance);

                        if (loadResult)
                        {
                            successResults.Add($"{targetFamily}::{loadedFamilyInstance.Name}: Successfully reloaded");
                        }
                        else
                        {
                            failedResults.Add($"{targetFamily}::{loadedFamily}: Failed to reload");
                        }
                    }

                    t.Commit();
                }

                // Save the temporary doc
                //targetFamilyDoc.Save();

                // Close the temporary document after processing
                bool isDocumentSaved = targetFamilyDoc.Close(true);
                if (isDocumentSaved)
                {
                    successResults.Add($"{targetFamily}:: Saved Successfully");
                }
                else
                {
                    failedResults.Add($"{targetFamily}:: Failed to Save");

                }
            }

            return new Tuple<List<string>, List<string>>(successResults, failedResults);
        }
        private List<string> FilterLoadedFamiliesForGivenDocument(Document doc)
        {
            var filteredFamilies = new HashSet<string>();


            // Collect family symbols from the document
            var collector = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol));

            foreach (FamilySymbol familySymbol in collector)
            {
                Family family = familySymbol.Family;
                // Filter for family symbols that we are looking for i.e., source-families & Dont add duplicates
                if (SourceFamilyNames.Contains(family.Name) && !filteredFamilies.Contains(family.Name))
                {
                    filteredFamilies.Add(family.Name);
                }
            }


            return filteredFamilies.ToList();
        }


        // :: constructor ::
        public FamilyReloaderView(Document doc)
        {
            InitializeComponent();

            // Set the DataContext to the current instance of FamilyReloaderView
            DataContext = this;

            // Initialize the ObservableCollections
            SuccessResults = new ObservableCollection<string>();
            FailedResults = new ObservableCollection<string>();

            // Assign the passed document to the private field
            _doc = doc;
        }


    }

    public class FamilyLoadOptionsHandler : IFamilyLoadOptions
    {
        public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
        {
            overwriteParameterValues = true;
            return true;
        }

        public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source, out bool overwriteParameterValues)
        {
            familyInUse = true;
            overwriteParameterValues = true;
            source = FamilySource.Family;
            return true;
        }

    }
}

