using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FileProcessingLibrary.Services
{
    
    public class CompareFiles
    {
        public static string Compare(string pathToNewFile, string pathToOldFile)
        {
            StreamReader newFile = new StreamReader(pathToNewFile);
            string outputFilePath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
            StreamWriter outputFile = new StreamWriter(outputFilePath);
            string newFileLine = string.Empty;
            long arCounter;
            string oldLine = string.Empty;
            List<string> arInfo = new List<string>();

            while ((newFileLine = newFile.ReadLine()) != null)
            {
                if (!char.IsWhiteSpace(newFileLine[0]))
                {
                    arInfo = new List<string>();
                    if ((arCounter = CheckIfArExistsOnOldFile(newFileLine,pathToOldFile)) > 0)
                    {
                        outputFile.WriteLine(newFileLine);
                        using (StreamReader oldFile = new StreamReader(pathToOldFile))
                        {
                            oldFile.SetPosition(arCounter);
                            while ((oldLine = oldFile.ReadLine()) != null)
                            {
                                if (!char.IsWhiteSpace(oldLine[0]))
                                {
                                    break;
                                }
                                arInfo.Add(oldLine);
                            }
                           
                            while ((newFileLine = newFile.ReadLine()) != null )
                            {
                                if (!char.IsWhiteSpace(newFileLine[0]))
                                {
                                    break;
                                }
                                bool checkIfInfoExists = false;
                                foreach (var info in arInfo)
                                {
                                    if (!string.Equals(info,newFileLine))
                                    {
                                        checkIfInfoExists = true;
                                    }
                                }
                                if (checkIfInfoExists)
                                {
                                    outputFile.WriteLine(newFileLine);
                                }
                            }
                        }
                    }
                    else
                    {
                        outputFile.WriteLine(newFileLine);
                    }
                }
            }
            outputFile.Close();
            return outputFilePath;
        }
        private static long CheckIfArExistsOnOldFile(string newFileLine, string pathToOldFile)
        {
            string oldFileLine = string.Empty;
            using (StreamReader oldFile = new StreamReader(pathToOldFile))
            {
                while ((oldFileLine = oldFile.ReadLine()) != null)
                {
                    if (!char.IsWhiteSpace(oldFileLine[0]))
                    {
                        string newAr = newFileLine.Substring(0, newFileLine.IndexOf(' '));
                        string oldAr = oldFileLine.Substring(0, oldFileLine.IndexOf(' '));
                        if (string.Compare(newAr, oldAr) < 1)
                        {
                            if (string.Equals(newAr, oldAr))
                            {
                                return oldFile.GetPosition();
                            }
                        }
                    }
                }
            }
            return 0;
        }
        
    }
    
}
