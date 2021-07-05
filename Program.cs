using System;
using System.IO;
using System.Collections.Generic;

namespace files_module
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var storesDirectory = Path.Combine(currentDirectory, "stores");
            
            // Add path to the "salesTotals" directory
            var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
            
            // Create the directory "salesTotalDir"
            Directory.CreateDirectory(salesTotalDir); 
            
            var salesFiles = FindFiles(storesDirectory);

            // Create an empty file called "totals.txt" inside 
            // the newly created "salesTotalsDir" directory.
            // NB we use an empty string for the file's contents for now.
            File.WriteAllText(Path.Combine(salesTotalDir, "totals.txt"), String.Empty);

        }

        // Create a new function called FindFiles that takes a folderName parameter.
        static IEnumerable<string> FindFiles(string folderName) 
        {
            // Create a new list of type strings named "salesFiles"
            List<string> salesFiles = new List<string>();

            // Searches the directory location specified and returns the full file names 
            // and the relevant file paths
            var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);
            
            // Search and check each file in "foundFiles" against parameter
            foreach (var file in foundFiles) 
            {
                // Get the extension of each file
                var extension = Path.GetExtension(file);

                // The file name will contain the full path so only check the end of it and if a match
                // add this file to "salesFile"
                if (file.EndsWith(".json")) 
                {
                    salesFiles.Add(file);
                }
            }
            return salesFiles; // Return the contents of "salesfile" to memory
        }//findFiles
    }//Program
}//files_module
