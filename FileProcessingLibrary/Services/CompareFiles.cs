using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileProcessingLibrary.Services
{
    public class CompareFiles
    {
        public static File Compare(string pathToNewFile, string pathToOldFile)
        {
            StreamReader newFile = new StreamReader(pathToNewFile);
            string newFileLine = string.Empty;

            while ((newFileLine = newFile.ReadLine()) != null)
            {
                if (!char.IsWhiteSpace(newFileLine[0]))
                {
                    if
                }
            }
        }
        private static bool CheckIfLineExistsOnOldFile(string newFileLine, string pathToOldFile)
        {
            StreamReader oldFile = new StreamReader(pathToOldFile);
            string oldFileLine = string.Empty;
            while ((oldFileLine = oldFile.ReadLine()) != null)
            {
                if (!char.IsWhiteSpace(oldFileLine[0]))
                {
                    if (string.Equals(newFileLine.Substring(0, newFileLine.IndexOf(' ')), oldFileLine.Substring(0, oldFileLine.IndexOf(' '))))
                    {

                    }
                }
            }
        }
    }
}
