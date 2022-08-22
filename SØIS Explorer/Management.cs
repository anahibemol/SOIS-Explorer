using NullUtils.ExtraUtils;

namespace NullUtils.ManagementUtils
{
    public static class Management
    {

        public static void Open()
        {
            Console.WriteLine("Please write the Path of the File/Folder you want to use");
            string I_Path = Console.ReadLine() ?? "ERROR 01";

            Extra.ErrorCheck(I_Path);

            Console.WriteLine(@"
            Write the following commands for what you want to do:
            |1 or CREATE  - Creates a new File/Folder.
            |2 or COPY    - Copy a File/Folder.
            |3 or MOVE    - Get the properties of a File/Folder.
            |4 or DELETE  - Deletes the File/Folder.
            |5 or RENAME  - Renames a File/Folder.
            |6 or BACK    - Goes Back to Main Menu.
            |___________________________________________________");
            // Since you are null checking, I would just ignore the (CS8602) warning if you see it.
            
            string Switch = Console.ReadLine().ToUpper() ?? "0"; //should use a different name for 'Switch', something like "Option"
            switch (Switch)
            {
                case "1" or "CREATE":
                    Management.CreateFiles(I_Path);
                break;
                case "2" or "COPY":
                    Management.CopyFiles(I_Path);
                break;
                case "3" or "MOVE":
                    Management.MoveFiles(I_Path);
                break;
                case "4" or "DELETE":
                    Management.DeleteFiles(I_Path);
                break;
                case "5" or "RENAME":
                    Management.RenameFiles(I_Path);
                break;
                case "6" or "BACK":
                    Program.MainMenu();
                break;
                // add default
            }
        
        }

        public static void CreateFiles(string rootPath) //Creates a file/folder
        {
            Console.WriteLine("Write the name of the new File/Directory");
            string I_Name = Console.ReadLine() ?? "ERROR 01";
            string newPath = "ERROR 01";
            bool Exists = true;
            bool FileOrFolder = Path.HasExtension(I_Name); //Diferentiates Files from Folders
                        
            Exists = Directory.Exists(rootPath); 

            if (Exists == true)
            {
                newPath = rootPath + @"\" + I_Name;

                Exists = Directory.Exists(newPath); //sorry for re-using the variable

                if (FileOrFolder == false && Exists == false)
                {
                    newPath = newPath + @"\";

                    Directory.CreateDirectory(newPath);
                }

                if (FileOrFolder == true && Exists == false)
                {
                    File.Create(newPath);
                }

                if (Exists == false)
                {
                    Console.WriteLine("Write a Valid Directory");
                }
            }               
        }
        public static void Copy(string root, string dest)
        {
            var dirSource = new DirectoryInfo(root);
            var dirTarget = new DirectoryInfo(dest);
            CopyAll(dirSource, dirTarget);
        }

        public static void CopyAll(DirectoryInfo root, DirectoryInfo dest)
        {
            int i = 0;
            Directory.CreateDirectory(dest.FullName);
            foreach (FileInfo file in root.GetFiles())
            {
                Console.WriteLine(@$"Copying {dest.FullName}\{file.Name}");
                file.CopyTo(Path.Combine(dest.FullName, file.Name), true);
            }

            foreach (DirectoryInfo dirRootSubDir in root.GetDirectories())
            {
                while (i < 1)
                {
                    DirectoryInfo nextDestSubDir = dest.CreateSubdirectory(dirRootSubDir.Name);
                    i++;
                    CopyAll(dirRootSubDir, nextDestSubDir);
                }
            }
        }

        public static void CopyFiles(string rootPath) //Copy a file/folder
        {
            Console.WriteLine("Write the Path of the target folder for the copied file/folder");
            string destPath = Console.ReadLine() ?? "ERROR 01";
            bool FileOrFolder = Path.HasExtension(rootPath); //Diferentiates Files from Folders

            Extra.ErrorCheck(destPath);

            if (FileOrFolder == false) 
            {
                string[] files = Directory.GetFiles(rootPath);                 

                foreach (string file in files)             
                {
                    var info = new FileInfo(file);
                    var typecheck = Extra.TypeCheck((Convert.ToString(info.Extension)));
                    
                    Management.Copy(rootPath, destPath);
                }                    
            }
            if (FileOrFolder == true) 
            {
                File.Copy(rootPath, $"{destPath}/{ Path.GetFileName(rootPath) }");              
            }
        }
                    
        public static void MoveFiles(string rootPath) //Move a file/folder
        {
            Console.WriteLine("Write the Path of the destination of your folder");
            string destPath = Console.ReadLine() ?? "ERROR 1";
            bool FileOrFolder = Path.HasExtension(rootPath); //Diferentiates Files from Folders

            Extra.ErrorCheck(rootPath);
            Extra.ErrorCheck(destPath);

            if (FileOrFolder == false)
            {

                string[] files = Directory.GetFiles(rootPath);

                foreach(string file in files)
                {
                    File.Move(rootPath, $"{destPath}/{ Path.GetFileName(rootPath) }"); 
                }                    
            }
            if (FileOrFolder == true)
            {
                File.Move(rootPath, $"{destPath}/{ Path.GetFileName(rootPath) }"); 
            }
        }
    
        public static void DeleteFiles(string rootPath) //Deletes a file/folder
        {
            bool FileOrFolder = Path.HasExtension(rootPath);

            if (FileOrFolder == true)
            {
                File.Delete(rootPath);
            }
            if (FileOrFolder == false)
            {
                Directory.Delete(rootPath, true);
            }
        }
    
        public static void RenameFiles(string rootPath) //Renames a file/folder
        {
            bool FileOrFolder = Path.HasExtension(rootPath); //Diferentiates Files from Folders
            if (FileOrFolder == true)
            {
                Console.WriteLine("Write the old name of the file");
                string oldNameFI = Console.ReadLine() ?? "ERROR 01";
                Console.WriteLine("Write the new name of the file");
                string newNameFI = Console.ReadLine() ?? "Unnamed";

                string newPath = rootPath.Replace(oldNameFI, newNameFI);
                File.Move(rootPath, newPath);                 
            }
            if (FileOrFolder == false)
            {
                Console.WriteLine("Write the old name of the folder");
                string oldNameFO = Console.ReadLine() ?? "ERROR 01";
                Console.WriteLine("Write the new name of the folder");
                string newNameFO = Console.ReadLine() ?? "Unnamed";
                string newPath = rootPath.Replace(oldNameFO, newNameFO);
                Directory.Move(rootPath, newPath);
            }
        }
    }
}
