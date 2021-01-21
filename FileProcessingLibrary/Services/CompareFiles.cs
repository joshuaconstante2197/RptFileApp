using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FileProcessingLibrary.Services
{
    
    public class CompareFiles
    {
        private static long CheckIfArExistsOnOldFile(string newFileLine, string pathToOldFile)
        {
            string oldFileLine = string.Empty;
            long position;
            using (StreamReader oldFile = new StreamReader(pathToOldFile))
            {
                while ((oldFileLine = oldFile.ReadLine()) != null )
                {
                    if (string.Equals(oldFileLine, "E"))
                    {
                        Console.WriteLine("a");
                    }
                    if (!char.IsWhiteSpace(oldFileLine[0]) && !string.Equals(oldFileLine, "E"))
                    {
                        string newAr = newFileLine.Substring(0, newFileLine.IndexOf(' '));
                        string oldAr;
                        try
                        {
                            oldAr = oldFileLine.Substring(0, oldFileLine.IndexOf(' '));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error on this line" + oldFileLine + ex);
                        }

                        if (string.Compare(newAr, oldAr) < 1)
                        {
                            if (string.Equals(newAr, oldAr))
                            {
                                position = oldFile.GetPosition();
                                oldFile.Close();
                                return position;
                            }
                        }
                    }
                }
            }
            return 0;
        }

        public static string Compare(string pathToNewFile, string pathToOldFile)
        {
            string outputFilePath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";

            StreamWriter outputFile = new StreamWriter(outputFilePath);

            string newFileLine;
            string newAr;
            string oldLine;

            long arCounter;

            List<string> arInfo;
            List<string> newArInfo;

            using (StreamReader newFile = new StreamReader(pathToNewFile))
            {
                while ((newFileLine = newFile.ReadLine()) != null)
                {
                    
                    if (!char.IsWhiteSpace(newFileLine[0]))
                    {
                        arInfo = new List<string>();
                       
                        if ((!string.Equals(newFileLine, "E")) && (arCounter = CheckIfArExistsOnOldFile(newFileLine, pathToOldFile)) > 0 )
                        {
                            newAr = newFileLine;
                            newArInfo = new List<string>();

                            using (StreamReader oldFile = new StreamReader(pathToOldFile))
                            {
                                oldFile.SetPosition(arCounter);
                                while ((oldLine = oldFile.ReadLine()) != null)
                                {
                                    
                                    if (!char.IsWhiteSpace(oldLine[0]))
                                    {
                                        break;
                                    }
                                    else if (oldLine.Contains("TOTAL Customer"))
                                    {
                                        arInfo.Add(oldLine.Replace(" ",""));
                                    }
                                    else
                                    {
                                        arInfo.Add(oldLine.Substring(0, 71));
                                    }
                                }
                            }
                            //making sure that I'm not encountering the next AR

                            while (newFile.Peek() != -1 && !char.IsLetterOrDigit(Convert.ToChar(newFile.Peek())) && newFile.Peek() != 27)
                            {
                                newFileLine = newFile.ReadLine();

                                string newFileLineSub;

                                    if (newFileLine.Contains("TOTAL Customer"))
                                    {
                                        newFileLineSub = newFileLine.Replace(" ", "");
                                    }
                                    else
                                    {
                                        newFileLineSub = newFileLine.Substring(0, 71);
                                    }

                                    bool checkIfInfoExists = false;

                                    foreach (var info in arInfo)
                                    {
                                        if (string.Equals(info, newFileLineSub))
                                        {
                                            checkIfInfoExists = true;
                                        }
                                    }
                                    if (!checkIfInfoExists)
                                    {
                                        newArInfo.Add(newFileLine);
                                    }
                            }


                            if (newArInfo.Count > 0)
                            {
                                outputFile.WriteLine(newAr);

                                foreach (var newInfo in newArInfo)
                                {
                                    outputFile.WriteLine(newInfo);
                                }
                            }

                        }
                        else
                        {
                            outputFile.WriteLine(newFileLine);
                            while (newFile.Peek() != -1 && !char.IsLetterOrDigit(Convert.ToChar(newFile.Peek())))
                            {
                                newFileLine = newFile.ReadLine();
                                outputFile.WriteLine(newFileLine);
                            }
                        }
                    }
                }

            }
            outputFile.Close();
            return outputFilePath;
        }
        
        
    }
    
}
