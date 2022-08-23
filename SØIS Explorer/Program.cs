using System.Diagnostics;
using NullUtils.ExtraUtils;
using NullUtils.ManagementUtils;

namespace NullUtils
{
    public static class Program 
    {
    public static class Viewing
    {
        public static void Open()
        {
            Console.WriteLine("Please write the Path of the File/Folder you want to use");
            string I_Path = Console.ReadLine() ?? "ERROR 01";
            Extra.ErrorCheck(I_Path);
            Console.WriteLine("Error 01: No Path was given");
            Console.WriteLine(@"
            Write the following commands for what you want to do:

            |1 or OPEN    - Open the file on a program of your choice.
            |2 or VIEW    - Show all the files on a folder.
            |3 or INSPECT - Get the properties of a file/folder.
            |4 or BACK    - Goes Back to Main Menu.
            |___________________________________________________");
            string Switch = Console.ReadLine().ToUpper() ?? "0";
            switch (Switch)
            {
                // with C#9 you can do relational pattern matching, this should work.
                case "1" or "OPEN":
                    Viewing.OpenFile(I_Path);
                break;

                case "2" or "VIEW":
                    Viewing.ViewFiles(I_Path);
                break;

                case "3" or "INSPECT":
                    Viewing.InspectFiles(I_Path, true);
                break;

                case "4" or "BACK":
                    Program.MainMenu();
                break;
                // Add a default, unsure what you want to be the default.
            }
        }

        public static void OpenFile(string rootPath)
        {
            Console.WriteLine("Write the path of the program you want to open your file/folder with");
            string programPath = Console.ReadLine() ?? "ERROR 01";
            Extra.ErrorCheck(programPath);
            Process.Start(programPath, rootPath);
        }

        public static void InspectFiles(string rootPath, bool SeeAll) //Get the properties of all files in a folder
        {                  
            Console.WriteLine("");
            bool Exists = Directory.Exists(rootPath);
            bool namingFormat = true;
            namingFormat    = rootPath.Contains(@":\");
            if (Exists == true) //guarantees the program receives a valid file
            {
                var files = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    var info = new FileInfo(file);
                    Console.WriteLine(@$"                        { Path.GetFileName(file) } 's Properties:
                    Name:              {info.Name}
                    Type:              {Extra.TypeCheck((Convert.ToString(info.Extension)))} ({info.Extension})
                    Directory:         {Path.GetDirectoryName(file)}");
                    if (SeeAll == true)
                    {
                        Console.WriteLine(@$"
                        Last Acessed At:   {info.LastAccessTime}
                        Last Modified At:  {info.LastWriteTimeUtc}
                        Creation Time:     {info.CreationTimeUtc}
                        Size:              { info.Length } Bytes 
                        Relative Path:     {Path.GetRelativePath(rootPath, file)} "); 
                    }
                    Console.WriteLine(" ");
                }      
            }
            else if (namingFormat == false) 
            { 
                Console.WriteLine("Invalid Path"); 
            }
            Console.ReadLine();                 
        }
        public static void ViewFiles(string I_Path) //Simplified InspectFiles()
        {
            InspectFiles(I_Path, false);
        }
    }

    public static void MainMenu()
    {
        bool whiler = true;

        while (whiler is true)
        {
            Console.WriteLine(@"
            Write the following commands For what you want to do:

            |1 or VIEW    - To Access Files.
            |2 or MANAGE  - To Manage Files.
            |3 or EXIT    - Exits the program.
            |_____________________________________________________________
        ");
            string MS = Console.ReadLine().ToUpper() ?? "BLANK";
            
            MS = MS.ToUpper(); 
            switch (MS)
            {
                case "1" or "VIEW":
                    Viewing.Open();
                break;

                case "2" or "MANAGE":
                    Management.Open();
                break;

                case "3" or "EXIT":
                    whiler = false;
                break;

                default:
                    Console.WriteLine("Invalid Command");
                break;
            
            }
        }       
    }
    public static void Main(string[] args)
    {
        Program.MainMenu();
    }  
    }
}
