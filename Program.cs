using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

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

            // Add a call to the CalculateSalesTotal function
            var salesTotal = CalculateSalesTotal(salesFiles); 

            // Use "File.AppendAllText" so nothing in the file gets overwritten.
            // Write the value of the "salesTotal" variable to the "totals.txt" file
            File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");
        }//Main

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

        // Create a new function that will calculate the sales total. 
        // This method should take an IEnumerable<string> of file paths 
        // that it can iterate over.
        static double CalculateSalesTotal(IEnumerable<string> salesFiles) 
        {
            double salesTotal = 0;

            // READ FILES LOOP
            // Loop over each file path in "salesFiles"
            foreach (var file in salesFiles)
            {
                // Read the contents if the file
                string salesJson = File.ReadAllText(file);

                // Parse the contents as JSON
                SalesData data = JsonConvert.DeserializeObject<SalesData>(salesJson);

                // Add the amount found in the "Total" field to the salesTotal variable
                salesTotal += data.Total;
            }//foreach

            return salesTotal;
        }

        class SalesData 
        {
            public double Total {get; set;}
        }

    }//Program
}//files_module
