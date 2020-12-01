using ConsoleApp2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            string pathToRptFile = @"C:\Users\Ibiley Uniforms\source\repos\ConsoleApp2\ConsoleApp1\Data\10-28-20.txt";
            string pathToTempFile = @"C:\Users\Ibiley Uniforms\source\repos\ConsoleApp2\ConsoleApp1\Data\testFullAr.txt";
            System.IO.File.WriteAllText(pathToTempFile, string.Empty);
            string line;
            string firstRows;
            bool count = false;
            bool wordToEx = true;
            string[] wordsToExclude = { "T R I A L", "Tran    Tran", "Date    Descr", "----    -----", "Time", "---------" };
            StreamReader rptFile = new StreamReader(pathToRptFile);
            firstRows = rptFile.ReadLine();

            
            int j;
            int k;
            while ((j = rptFile.Peek()) >= 0)
            {
                k = rptFile.Read();
                if (k == 10)
                {
                    if(rptFile.Peek() == 65)
                    {
                        break;
                    }
                }
                
            }

            using (StreamWriter streamWriter = new StreamWriter(pathToTempFile))
            {
                while ((line = rptFile.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        foreach (var word in wordsToExclude)
                        {
                            if (line.Contains(word))
                            {
                                wordToEx = false;
                                break;
                            }
                            wordToEx = true;
                        }
                        if (wordToEx)
                        {
                            if (!char.IsDigit(line[0]) && !char.IsWhiteSpace(line[0]) && line[0] != 'E')
                            {
                                streamWriter.WriteLine(line);
                                count = true;
                            }
                            else if (char.IsWhiteSpace(line[0]) && count)
                            {
                                streamWriter.WriteLine(line);
                            }
                            else if (char.IsDigit(line[0]) || line[0] == 'E')
                            {
                                count = false;
                            }

                        }
                        else
                        {
                            continue;
                        }
                    }

                }
            }

        }
    }
}
