using System;
using System.IO;
using System.Collections;
using System.Linq;

namespace ListContents
{
    class Program
    {
        static void Main(string[] args)
        {
            Process();
        }

        public static void Process()
        {
            Console.WriteLine("Please specify a directory to read (please include \\ at end of dir):");
            string directory = Console.ReadLine();
            StreamWriter sw = new StreamWriter(directory + "Content.txt", true);
            Console.WriteLine("Reading {0}. Please wait, this may take a few minutes...", directory);
            ProcessDirectory(directory);
            sw.Close();
            Console.WriteLine("Written to " + directory + "Content.txt - Press any key to exit");
            Console.ReadKey();


            void ProcessDirectory(string targetDirectory)
            {
                // Process the list of files found in the directory.
                try
                {
                    string[] fileEntries = Directory.GetFiles(targetDirectory);
                    foreach (string fileName in fileEntries)
                        ProcessFile(fileName);
                }
                catch
                {
                    return;
                }
                // Recurse into subdirectories of this directory.
                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory).OrderByDescending(x => DirSize(new DirectoryInfo(x))).ToArray();
                foreach (string subdirectory in subdirectoryEntries)
                {
                    try
                    {
                        sw.WriteLine("Folder: {0}, Size: {1} bytes", subdirectory, DirSize(new DirectoryInfo(subdirectory)));
                        ProcessDirectory(subdirectory);
                    }
                    catch
                    {
                        continue;
                    }
                }


            }

            // Insert logic for processing found files here.
            void ProcessFile(string path)
            {
                sw.WriteLine("Processed file '{0}'.", path);
            }
        }



        public static long DirSize(DirectoryInfo d)
        {

            long size = 0;
            // Add file sizes.
            try
            {
                FileInfo[] fis = d.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    size += fi.Length;
                }
                // Add subdirectory sizes.
                DirectoryInfo[] dis = d.GetDirectories();
                foreach (DirectoryInfo di in dis)
                {
                    size += DirSize(di);
                }
                return size;
            }
            catch
            {
                return 0;
            }
        }
    }
}
