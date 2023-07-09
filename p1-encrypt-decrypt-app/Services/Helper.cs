using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1_encrypt_decrypt_app.Services
{
    internal class Helper
    {        
        // get path folder in project with folder_name but project is parent
        public static string path_to_project_not_bin(string foler_name)
        {
            string? projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            string path_folder = "";

            if (!string.IsNullOrEmpty(projectPath))
            {
                // 
                path_folder = Path.Combine(projectPath, foler_name);
                if (!Directory.Exists(path_folder))
                {
                    Directory.CreateDirectory(path_folder);
                    Console.WriteLine("Create new folder");
                }
                else
                {
                    Console.WriteLine("Folder exsits");
                }

            }

            return path_folder;
        }
    }
}
