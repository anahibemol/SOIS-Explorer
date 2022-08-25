
namespace NullUtils.ExtraUtils
{
    public class Extra
    {
        // This could be condensed 
        private static readonly string[] ImageExt = { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".RAW", ".tif", ".tiff", ".pdn", ".xcf", ".webp"};

        private static readonly string[] AudioExt = {".mp3", ".wav", ".aac", ".flac", ".mp2", ".opus", ".m4a", ".wma", ".aa", ".mid", ".midi", ".s3m", ".ftm", ".xm", ".it"};

        private static readonly string[] SheetExt = {".mus", ".musx", ".gp", ".mxl", ".mei", ".sib", ".mscx", ".ly", ".mscz", ".smdl", ".mscz"};
        
        private static readonly string[] AudioDataExt = {".als", ".alc", ".alp", ".aup", ".aup3", ".cpr", ".drm", ".flp", ".ptx", ".logic", ".mmr", ".rpp", ".band", ".ptf", ".namd"};
        
        private static readonly string[] VideoExt = {".mp4", ".aad", ".webm", ".m4v", ".mkv", ".avi", ".3gp", ".asf", ".wmv", ".flv", ".mov"};

        private static readonly string[] VideoDataExt = {".veg", ".hfp", ".wfp", ".fcp", ".ppj", ".bak", ".braw", ".wlmp", ".imovieproj", ".prproj"};

        private static readonly string[] PkgExt = {".apk", ".deb", ".rpm", ".jar", ".npm", ".msi"};

        private static readonly string[] CompressedExt = {".zip", ".RAR", ".tar", ".7z"};

        private static readonly string[] MarkupExt = {".txt", ".xml", ".md", ".rss"};

        private static readonly string[] BinExt = {".bak", ".bin", ".dat", ".dsk"};

        public static string TypeCheck(params string [] file)
        {
            var y = ""; // variable name isn't specific, should change it
            string plang = "Unknown";        
            for (int i = 0; i <= file.Length; i++) // not sure why the loop is here
            {
                y = file[i];  
                if (ImageExt.Contains(y))
                {
                    return "Image";
                }

                else if (AudioExt.Contains(y))
                {
                    return "Audio"; 
                }
                
                else if (SheetExt.Contains(y))
                { 
                    return "Sheet Music"; 
                }
                else if (AudioDataExt.Contains(y))
                { 
                    return "Audio Project"; 
                }

                else if (VideoExt.Contains(y))
                { 
                    return "Video"; 
                }

                else if (VideoDataExt.Contains(y))  
                {
                    return "Video Project";
                }  

                else if (PkgExt.Contains(y)) 
                { 
                    return "Package"; 
                }   

                else if (CompressedExt.Contains(y))
                { 
                    return "Compressed File";
                }         

                else if (MarkupExt.Contains(y))
                { 
                    return "Markup Language";
                }
                // You can use a Dictionary for this, something like ProgLang[".py"] = "Python";  

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
                
                else if (BinExt.Contains(y))
                { 
                    return "Binary File";
                }

                else
                {
                    return "Unknown";
                }
            }
            
            return y;
        }

        public static void ErrorCheck(string Error)
        {
            if (Error == "ERROR 01")
            {
                Console.WriteLine($@"
                Empty Path, please write something on the same guidelines as
                ``C:\Users\USUARIO\Documents\Important``
                
                Your Path: {Error}");

                Console.ReadLine();
                Program.MainMenu();
            }
            else if (Error == "ERROR 02")
            {
                Console.WriteLine($@"
                Invalid Path, please write something on the same guidelines as
                ``C:\Users\USUARIO\Documents\Important``
                Your Path: {Error}");
                Console.ReadLine();
                Program.MainMenu();
            }
        }
    }
}
