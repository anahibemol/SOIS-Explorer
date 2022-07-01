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


        public class SoisTask
        {

            
            public static void InspectFiles(bool SeeAll) //Get the properties of all files in a folder
            {
                   
                Console.WriteLine("Write the Path of the folder you want to inspect");
        
                // Example: "C:\Users\USUARIO\Documents\Programação\Sois File Manager\Programs"
                string? rootPath = Convert.ToString(Console.ReadLine());
                Console.WriteLine("");
                bool dirExists = Directory.Exists(rootPath);
                bool namingFormat = true;
                if (rootPath is not null) { namingFormat    = rootPath.Contains(@":\"); }
                
                if (rootPath is not null && dirExists == true) //guarantees the program receives a valid file
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
                Console.WriteLine(" ")
                
               
        
                ;       
                }      
        
              }
                
                if (namingFormat == false) { Console.WriteLine("Invalid Path"); }
                
                Console.ReadLine();                 
            }

            public static void ViewFiles() //Simplified InspectFiles()
            {
                InspectFiles(false);
            }

            public static void CreateFiles() //Creates a file/folder
            {
                Console.WriteLine("Type the Path of the desired new File/Directory");
                string? rootPath = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Write the name of the new File/Directory");
                string? dirName = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Is it a File or a Directory ?");
                string? createFilesSwitch = Convert.ToString(Console.ReadLine());
                if (createFilesSwitch is not null) {createFilesSwitch.ToUpper();}


                string newPath = rootPath + @"\" + dirName;
                if (createFilesSwitch == "DIRECTORY") {newPath = newPath + @"\"; }
 
                bool dirExists = Directory.Exists(newPath);
                
                if (dirExists == false) 
                {
                    if (createFilesSwitch == "DIRECTORY" ^ createFilesSwitch == "FOLDER")
                     { Directory.CreateDirectory(newPath); }
                    if (createFilesSwitch == "FILE")
                     { File.Create(newPath); }

                }
                else  { Console.WriteLine("This Directory already exists"); }


            }

            public static void RenameFiles() //Rebanes a file/folder
            {

                Console.WriteLine("Write the Path of the Folder/File You want to Rename");
                string? rootPath = string.Empty;
                rootPath = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Is it a File or a Folder?");
                string? FileOrFolder = Convert.ToString(Console.ReadLine());
                if (FileOrFolder is not null) { FileOrFolder.ToUpper(); }
                switch (FileOrFolder)
                {
                    case "FILE":
                        Console.WriteLine("Write the old name of the file");
                        string? oldNameFI = Console.ReadLine();
                        Console.WriteLine("Write the new name of the file");
                        string? newNameFI = Console.ReadLine();

                        if (oldNameFI is not null && newNameFI is not null && rootPath is not null)
                        {
                            string newPath = rootPath.Replace(oldNameFI, newNameFI);

                            File.Move(rootPath, newPath);
                        }
                    break;

                    case "FOLDER":
                        Console.WriteLine("Write the old name of the folder");
                        string? oldNameFO = Console.ReadLine();
                        Console.WriteLine("Write the new name of the folder");
                        string? newNameFO = Console.ReadLine();

                        if (oldNameFO is not null && newNameFO is not null && rootPath is not null)
                        {
                            string newPath = rootPath.Replace(oldNameFO, newNameFO);

                            Directory.Move(rootPath, newPath);
                        }
                    break;
                }
                

            }

            public static void CopyFiles() //Copy a file/folder
            {
                Console.WriteLine("Type the Path of the file or folder to be copied");
                string? rootPath = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Write the Path of the target folder for the copied file/folder");
                string? destPath = Convert.ToString(Console.ReadLine());
                bool dotException = false;
                if (rootPath is not null) { dotException = rootPath.Contains("."); }
                
                //Copies Files and Folders with "."
                if (rootPath is not null && destPath is not null && dotException == true)
               { 
                    string[] files = Directory.GetFiles(rootPath);                 

                    foreach (string file in files)             
                    {
                        var info = new FileInfo(file);
                        var typecheck = Extra.TypeCheck((Convert.ToString(info.Extension)));

                        if (typecheck is "Unknown") //Exception for Folders with "."
                        {
                            Extra.Copy(rootPath, destPath);
                        }
                        else //Case for files
                        {
                            Console.WriteLine(file);
                            Console.WriteLine(" ");
                            Console.WriteLine($"The Path is {destPath}/{ Path.GetFileName(file) }");
                            File.Copy(file, $"{destPath}/{ Path.GetFileName(file) }");                            
                        }
                    }
                   
                }
                //Copies Folders without "."
                if (rootPath is not null && destPath is not null && dotException == false)
                {
                    string[] dirs = Directory.GetFiles(rootPath);

                    foreach (string file in dirs)             
                    {

                        Extra.Copy(rootPath, destPath);

                    }                  
                }
                


               
            }
        
            public static void MoveFiles() //Move a file/folder
            {
                Console.WriteLine("Type the Path of the folder");
                string? rootPath = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Write the Path of the destination of your folder");
                string? destPath = Convert.ToString(Console.ReadLine());
                if (rootPath is not null && destPath is not null)
               { 
                string[] files = Directory.GetFiles(rootPath);

                foreach(string file in files)
                {
                    File.Move(file, $"{destPath}/{ Path.GetFileName(file) }");
                }

               } 
            }
        
            public static void DeleteFiles()
            {
                Console.WriteLine("Write the Path of your file/folder");
                string? rootPath = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Do you want to delete a FILE or a FOLDER");
                string? FileOrFolder = Convert.ToString(Console.ReadLine());
                if (FileOrFolder is not null && rootPath is not null) 
                {FileOrFolder.ToUpper(); 
                
                switch (FileOrFolder)
                {
                    case "FILE":
                        File.Delete(rootPath);
                    break;

                    case "FOLDER":
                        
                        Directory.Delete(rootPath, true);
                    break;

                    default:
                    Console.WriteLine ("Type ''FILE'' or ''FOLDER'' ");
                    break; 
                }

                }
            }
        }

        public static void Main(string[] args)
        {
            bool whiler = true;

            while (whiler is true)
            {
             Console.WriteLine(@"
             Write the following commands For what you want to do:

             |VIEW    - To Get the Files of a Folder (and their path).
             |INSPECT - To Get the Files of a Folder (and their properties).
             |CREATE  - To Create a new File or a Folder.
             |RENAME  - Rename a File or a Folder.
             |COPY    - To Copy the Files of a Folder to another.
             |MOVE    - To Move the Files of a Folder to another.
             |DELETE  - To Delete the Files of a Folder.
             |EXIT    - Exits the program.
             |_____________________________________________________________
            ");
                string MainSwitcher = "BLANK";
                string? MS = Convert.ToString(Console.ReadLine());
                if (MS is not null) { MainSwitcher = MS.ToUpper(); }
                
                switch (MainSwitcher)
                {
                    case "VIEW":
                        SoisTask.ViewFiles();
                    break;

                    case "INSPECT":
                        SoisTask.InspectFiles(true);
                    break;

                    case "CREATE":
                        SoisTask.CreateFiles();
                    break;

                    case "RENAME":
                        SoisTask.RenameFiles();
                    break;

                    case "COPY":
                        SoisTask.CopyFiles();
                    break;

                    case "MOVE":
                        SoisTask.MoveFiles();
                    break;

                    case "DELETE":
                        SoisTask.DeleteFiles();
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

        
        public static class Extra
        {

                public static void GetPath(string surrogate1)
                {

                    

                }
            

                //Content from this class adapted from https://code.4noobz.net/c-copy-a-folder-its-content-and-the-subfolders/
                //Please visit it since i did not want to copy the comments
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
            

        }

        
    }



}
