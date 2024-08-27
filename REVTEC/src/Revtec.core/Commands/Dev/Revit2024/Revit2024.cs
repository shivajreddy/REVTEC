using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revtec.core.Commands.Revit2024;
using Application = Autodesk.Revit.ApplicationServices.Application;


namespace Revtec.core.Commands.Dev.Revit2024
{
    [Transaction(TransactionMode.Manual)]
    public class Revit2024 : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elments)
        {
            var uiApp = commandData.Application;
            var uiDoc = uiApp.ActiveUIDocument;
            var app = uiApp.Application;

            string logFileName = $"UpgradeLog_{DateTime.Now:M-d}.txt";
            Logger logger = new Logger(logFileName);


            // Open a dialog box to check all the folders
            //string[] userSelectedFolderPaths = new[] {""};
            List<string> userSelectedFolderPaths = GetUserSelectedFolders();
            // Assuming userSelectedFolderPaths is a List<string> containing the folder paths
            foreach (string folderPath in userSelectedFolderPaths)
            {
                Debug.WriteLine($"Chosen Folder: {folderPath}");
            }
            //Debug.WriteLine($"CHOSEN FILES::::: {userSelectedFolderPaths.ToString()}");

            //return Result.Succeeded;

            // User selected start
            logger.AddDataToLogFile($"SCRIPT STARTED AT : {DateTime.Now}");
            foreach (var folderPath in userSelectedFolderPaths)
            {
                string[] filePathsToUpdate = SearchForRevitFiles(folderPath);
                logger.AddDataToLogFile($"Total Files found in {folderPath}: {filePathsToUpdate.Length}");
                //logger.AddDataToLogFile($"File paths:: {filePathsToUpdate.ToString()}");

                int count = 1;
                foreach (var path in filePathsToUpdate)
                {
                    logger.AddDataToLogFile($"{count} TARGET | {DateTime.Now} | {path}");
                    OpenRevitFile(app, path, logger);
                    count++;
                }
            }
            logger.AddDataToLogFile($"SCRIPT ENDED AT : {DateTime.Now}");

            return Result.Succeeded;
        }

        private List<string> GetUserSelectedFolders()
        {
            List<string> folders = new List<string>();
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select folders for processing";
                folderBrowserDialog.ShowNewFolderButton = false;

                while (true)
                {
                    DialogResult result = folderBrowserDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        folders.Add(folderBrowserDialog.SelectedPath);
                        // Ask the user if they want to add more folders
                        DialogResult continueResult = MessageBox.Show("Do you want to add another folder?", "Add Folder", MessageBoxButtons.YesNo);
                        if (continueResult != DialogResult.Yes)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return folders;
        }




        static void OpenRevitFile(Application application, String filePath, Logger logger)
        {
            try
            {
                ModelPath modelPath = new FilePath(filePath);

                // /*
                // Set up options to handle warnings during upgrade
                var options = new OpenOptions();
                options.AllowOpeningLocalByWrongUser = false;

                var newDoc = application.OpenDocumentFile(modelPath, options);

                if (newDoc != null)
                {
                    logger.AddDataToLogFile($"OPENED | {DateTime.Now} | {filePath}");
                    Debug.WriteLine($"OPENED | {DateTime.Now} | {filePath}");

                    newDoc.Save();
                    logger.AddDataToLogFile($"SAVED  | {DateTime.Now} | {filePath}");
                    Debug.WriteLine($"SAVED | {DateTime.Now}");

                    newDoc.Close(true);
                    logger.AddDataToLogFile($"CLOSED | {DateTime.Now} | {filePath}");
                    logger.AddDataToLogFile("------------------------------------------------------");
                    Debug.WriteLine($"CLOSED::{DateTime.Now}");
                }
            }
            catch (Exception e)
            {
                logger.AddDataToLogFile($"ERROR  | {DateTime.Now} | {filePath} |");
                logger.AddDataToLogFile("------------------------------------------------------");
                logger.AddDataToLogFile(e.Message);
                Debug.WriteLine(e);
            }
        }

        static string[] SearchForRevitFiles(string parentFolderPath)
        {
            Debug.WriteLine("::SearchForRevitFiles method started::");

            try
            {
                List<string> rvtFilesList = new List<string>();

                string[] subdirectories = Directory.GetDirectories(parentFolderPath);
                //var filteredSubdirectories = subdirectories.Skip(22).ToArray();

                //string[] subdirectories = Directory.GetDirectories(parentFolderPath, "[c-t]*");
                foreach (var subDirectory in subdirectories)
                {
                    string[] allRvtFiles = Directory.GetFiles(subDirectory, "*.rvt");

                    // Exclude files with the pattern "<name>.<number>.rvt"
                    string[] filteredRvtFiles = allRvtFiles
                        .Where(file => !Regex.IsMatch(Path.GetFileName(file), @"\.\d+\.rvt$"))
                        .ToArray();
                    rvtFilesList.AddRange(filteredRvtFiles);
                }

                return rvtFilesList.ToArray();
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine($"Access to folder {parentFolderPath} is denied");
                return Array.Empty<string>();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"An error occurred: {e.Message}");
                return Array.Empty<string>();
            }
        }


        // method to get the namespace path, for registering the button on Revtec plugin
        public static string GetPath()
        {
            return typeof(Revit2024).Namespace + "." + nameof(Revit2024);
        }
    }
}