using System;
using System.IO;
using System.Configuration;
using System.Xml.Linq;

namespace Validate
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo file;
            string directoryPath = ConfigurationManager.AppSettings["DefaultDirectoryPath"];
            string scrapefile = ConfigurationManager.AppSettings["scrapeFileName"];

            string[] files = Directory.GetFiles(directoryPath, scrapefile + "*.rip");

            int i = 1;
            foreach (string _file in files)
            {
                file = new FileInfo(_file);

                Common.RipFile = XElement.Load(_file);
                Common.Filename = _file;
                Common.ErrorList = new System.Collections.ArrayList();

                Console.WriteLine("**************" + file.Name + "**************\n");
                var errorList = ValidateFile.IsValid();
                Console.Write(Environment.NewLine);
                if (errorList.Count > 0)
                {
                    foreach (string str in errorList)
                    {
                        Console.WriteLine("* " + str);
                    }
                }

                i++;
                Console.Write("\n\n\n");
            }
            Console.WriteLine("Press enter to exit....");
            Console.ReadLine();
        }
    }
}
