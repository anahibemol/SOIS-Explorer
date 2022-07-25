using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
                if (I_Path == "ERROR 01") 
                {
                    Console.WriteLine("Error 01: No Path was given");
                }
                else
                {
                    Console.WriteLine(@"
                    Write the following commands for what you want to do:

                    |1 or OPEN    - Open the file on a program of your choice.
                    |2 or VIEW    - Show all the files on a folder.
                    |3 or INSPECT - Get the properties of a file/folder.
                    |4 or BACK    - Goes Back to Main Menu.
                    |___________________________________________________");
                    string Switch = Console.ReadLine() ?? "0";
                    if (Switch == "1") {Switch = "OPEN"   ;}
                    if (Switch == "2") {Switch = "VIEW"   ;}
                    if (Switch == "3") {Switch = "INSPECT";}
                    if (Switch == "4") {Switch = "BACK";}
                    Switch = Switch.ToUpper();
                    switch (Switch)
                    {
                        case "OPEN":
                            Viewing.OpenFile();
                        break;

                        case "VIEW":
                            Viewing.ViewFiles();
                        break;

                        case "INSPECT":
                            Viewing.InspectFiles(true);
                        break;

                        case "BACK":
                            Program.MainMenu();
                        break;
                    }
                }
            }

           public static void OpenFile()
            {

            }

           public static void InspectFiles(bool SeeAll) //Get the properties of all files in a folder
            {
                   
                Console.WriteLine("Write the Path of the folder you want to inspect");
        
                // Example: "C:\Users\USUARIO\Documents\Programação\Sois File Manager\Programs"
                string rootPath = Console.ReadLine() ?? "ERROR 01";
                Console.WriteLine("");
                bool Exists = Directory.Exists(rootPath);
                bool namingFormat = true;
                if (rootPath is not null) { namingFormat    = rootPath.Contains(@":\"); }
                
                if (rootPath is not null && Exists == true) //guarantees the program receives a valid file
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
              { Console.WriteLine(@$"
                Last Acessed At:   {info.LastAccessTime}
                Last Modified At:  {info.LastWriteTimeUtc}
                Creation Time:     {info.CreationTimeUtc}
                Size:              { info.Length } Bytes 
                Relative Path:     {Path.GetRelativePath(rootPath, file)} "); }
                Console.WriteLine(" ");
                
          
                }      
        
              }
                
                if (namingFormat == false) { Console.WriteLine("Invalid Path"); }
                
                Console.ReadLine();                 
            }

           public static void ViewFiles() //Simplified InspectFiles()
            {
                InspectFiles(false);
            }

        }

        public static class Management
        {

            public static void Open()
            {
                Console.WriteLine("Please write the Path of the File/Folder you want to use");
                string I_Path = Console.ReadLine() ?? "ERROR 01";
                if (I_Path == "ERROR 01") 
                {
                    Console.WriteLine("Error 01: No Path was given");
                }
                else
                {
                    Console.WriteLine(@"
                    Write the following commands for what you want to do:

                    |1 or CREATE  - Creates a new File/Folder.
                    |2 or COPY    - Copy a File/Folder.
                    |3 or MOVE    - Get the properties of a File/Folder.
                    |4 or DELETE  - Deletes the File/Folder.
                    |5 or RENAME  - Renames a File/Folder.
                    |6 or BACK    - Goes Back to Main Menu.
                    |___________________________________________________");
                    string Switch = Console.ReadLine() ?? "0";
                    if (Switch == "1") {Switch = "CREATE" ;}
                    if (Switch == "2") {Switch = "COPY"   ;}
                    if (Switch == "3") {Switch = "MOVE"   ;}
                    if (Switch == "4") {Switch = "DELETE" ;}
                    if (Switch == "5") {Switch = "RENAME" ;}
                    if (Switch == "6") {Switch = "BACK"   ;}
                    Switch = Switch.ToUpper();
                    switch (Switch)
                    {
                        case "CREATE":
                            Management.CreateFiles(I_Path);
                        break;

                        case "COPY":
                            Management.CopyFiles(I_Path);
                        break;

                        case "MOVE":
                            Management.MoveFiles(I_Path);
                        break;

                        case "DELETE":
                            Management.DeleteFiles(I_Path);
                        break;

                        case "RENAME":
                            Management.RenameFiles(I_Path);
                        break;

                        case "BACK":
                            Program.MainMenu();
                        break;
                    }
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

            public static void CopyFiles(string rootPath) //Copy a file/folder
            {
                Console.WriteLine("Write the Path of the target folder for the copied file/folder");
                string destPath = Console.ReadLine() ?? "ERROR 01";
                bool FileOrFolder = Path.HasExtension(rootPath); //Diferentiates Files from Folders
 
                Extra.ErrorCheck();

                if (FileOrFolder == false) 
                {
                    string[] files = Directory.GetFiles(rootPath);                 

                    foreach (string file in files)             
                    {
                        var info = new FileInfo(file);
                        var typecheck = Extra.TypeCheck((Convert.ToString(info.Extension)));
                        
                        Extra.Copy(rootPath, destPath);
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
                Extra.ErrorCheck();
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

        public static void MainMenu()
        {
            bool whiler = true;

            while (whiler is true)
            {
             Console.WriteLine(@"
             Write the following commands For what you want to do:

             |VIEW    - To Access Files.
             |MANAGE  - To Manage Files.
             |EXIT    - Exits the program.
             |_____________________________________________________________
            ");
                string MainSwitcher = "BLANK";
                string? MS = Console.ReadLine();
                if (MS is not null) { MainSwitcher = MS.ToUpper(); }
                
                switch (MainSwitcher)
                {
                    case "VIEW":
                        Viewing.Open();
                    break;

                    case "MANAGE":
                        Management.Open();
                    break;

                    case "EXIT":
                        whiler = false;
                    break;

                    default:
                        Console.WriteLine("Invalid Message");
                    break;
                
                }
            }
                        
        }
        public static void Main(string[] args)
        {
            Program.MainMenu();
        }

        
        public static class Extra
        {

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

                public static string TypeCheck(params string [] file)
                {
                    var y = "";
                    string plang = "Unkown";
                                    
                    for (int i = 0;  ;i++) 
                    {
                        y = file[i];  

                        if 
                        (
                            y == ".png" ^ y == ".jpg" ^ y == ".jpeg" ^ y == ".gif" ^ y == ".bmp" ^
                            y == ".RAW" ^ y == ".tif" ^ y == ".tiff" ^ y == ".pdn" ^ y == ".xcf" ^
                            y == ".webp"
                        )

                        { return "Image"; }

                        if
                        (
                            y == ".mp3"  ^ y == ".wav" ^ y == ".aac" ^ y == ".flac" ^ y == ".mp2" ^
                            y == ".opus" ^ y == ".m4a" ^ y == ".wma" ^ y == ".aa"   ^ y == ".mid" ^
                            y == ".midi" ^ y == ".s3m"  ^ y == ".ftm" ^ y == ".xm"  ^ y == "it"
                        )

                        {return "Audio"; }

                        if
                        (
                            y == ".mus" ^ y == ".musx" ^ y == ".gp" ^ y == ".mxl"  ^ y == ".mei" ^
                            y == ".sib" ^ y == ".mscx" ^ y == ".ly" ^ y == ".mscz" ^ y == ".smdl"^
                            y == ".mscz,"
                        )

                        { return "Sheet Music"; }

                        if
                        (
                            y == ".als" ^ y == ".alc" ^ y == ".alp"  ^ y == ".aup" ^ y == ".aup3" ^ 
                            y == ".cpr" ^ y == ".drm" ^ y == ".flp"  ^ y == ".ptx" ^ y == ".logic" ^
                            y == ".mmr" ^ y == ".rpp" ^ y == ".band" ^ y == ".ptf" ^ y == ".namd"
                        )

                        { return "Audio Project"; }

                        if 
                        (
                           y == ".mp4" ^ y == ".aad" ^ y == ".webm" ^ y == ".m4v" ^ y == ".mkv" ^
                           y == ".avi" ^ y == ".3gp" ^ y == ".asf" ^  y == ".wmv" ^ y == ".flv" ^
                           y == ".mov"
                        )
                                                
                        { return "Video"; }

                        if
                        (
                            y == ".veg" ^ y == ".hfp"  ^ y == ".wfp"  ^ y == ".fcp"^ y == ".ppj" ^
                            y == ".bak" ^ y == ".braw" ^ y == ".wlmp" ^ y == ".imovieproj" ^ y == ".prproj"
                        )   

                        { return "Video Project";}  

                        if
                        (
                            y == ".apk" ^ y == ".deb" ^ y == ".rpm" ^ y == ".jar" ^ y == ".npm" ^ y == ".msi"
                        )   

                        { return "Package"; }   

                        if
                        (
                            y == ".zip" ^ y == ".RAR" ^ y == ".tar" ^ y == ".7z"
                        )

                        { return "Compressed File";}         

                        if
                        (
                            y == ".txt" ^  y ==".xml" ^ y == ".md" ^ y == ".rss"
                        )

                        { return "Markup Language";}



                        if (y == ".cbl" ^ y == ".cob") {plang = "COBOL";}
                        if (y == ".ps1" ^ y == ".psc1" ^ y == ".psm1" ^ y == ".psd1") {plang = "Powershell";}
                        if (y == ".pl" ^ y == ".pm") {plang = "Perl";} 
                        if (y == ".js") {plang = "JavaScript";} if (y == ".java") {plang = "Java";}   
                        if (y == ".html") {plang = "HTML";}     if (y == ".css") {plang = "CSS";}     
                        if (y == ".py") {plang = "Python";}     if (y == ".sql") {plang = "SQL";}     
                        if (y == ".cs") {plang = "C#";}         if (y == ".cpp") {plang = "C++";}     
                        if (y == ".php") {plang = "PHP";}       if (y == ".go")  {plang = "Go";}      
                        if (y == ".rs") {plang = "Rust";}       if (y == ".dart") {plang = "Dart";}   
                        if (y == ".asm") {plang = "Assembly";}  if (y == ".swift") {plang = "Swift";} 
                        if (y == ".bas") {plang = "VBA";}       if (y == ".m") {plang = "Mathlab";}
                        if (y == ".ts") {plang = "TypeScript";} if (y == ".lisp") {plang = "Lisp";} 
                        if (y == ".node") {plang = "NodeJS";}   if (y == ".r") {plang = "R";}
                        if (y == ".bat") {plang = "Batch";}     if (y == ".rb") {plang = "Ruby";}
                        if (y == ".sh") {plang = "Shell";}      if (y == ".c") {plang = "C";}
                        
                        if
                        (
                            y == ".js" ^ y == ".html" ^ y == ".css" ^ y == ".py" ^ y == ".sql" ^ y == ".java"  ^
                            y == ".ts" ^ y == ".node" ^ y == ".bat" ^ y == ".cs" ^ y == ".cpp" ^ y == ".psd1"  ^
                            y == ".sh" ^ y == ".psc1" ^ y == ".php" ^ y == ".go" ^ y == ".c"   ^ y == ".psm1"  ^
                            y == ".rs" ^ y == ".dart" ^ y == ".ps1" ^ y == ".rb" ^ y == ".asm" ^ y == ".swift" ^
                            y == ".r"  ^ y == ".bas"  ^ y == ".cob"  ^ y == ".pm" ^ y == ".m"   ^ y == ".lisp"  ^
                            y == ".pl" ^ y == ".cbl"
                        )                                              

                        { return $"{plang} Source Code"; }

                        if
                        (
                            y == ".bak" ^ y == ".bin" ^ y == ".dat" ^ y == ".dsk"
                        )

                        { return "Binary File";}

                        else
                        {
                            return "Unknown";
                        }

                    }
                    
                }
            
                public static void ErrorCheck()
                {

                }
        }

        
    }



}