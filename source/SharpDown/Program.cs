using System;
using System.IO;


namespace SharpDown
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = string.Empty;

            if (args == null || args.Length == 0)
            {
                //  Search for .xml file in local directory
                var files = Directory.GetFiles(Environment.CurrentDirectory, "*.xml");
                if (files == null || files.Length == 0)
                {
                    throw new Exception($"No file given and unable to locate a .xml file in the directory {Environment.CurrentDirectory}");
                }
                else
                {
                    filepath = files[0];
                }
            }
            else
            {
                filepath = args[0];
            }


            var doc = Importer.Run(filepath);
            var assembly = Processor.Run(doc);

            Console.ReadLine();


        }
    }
}
