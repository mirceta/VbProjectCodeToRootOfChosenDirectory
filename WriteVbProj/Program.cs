using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WriteVbProj
{
    class Program
    {

        /*
         Write all found .bas and .frm files from vbSourceDirectory recursively to the root of
         a destination folder. 
         Then you can use Find All References in Birokrat by opening it in VS code!
         */

        static string VbSourceDirectory = @"C:\Users\km\Desktop\playground\birokrat\Prg";
        static string DestinationDirectory = @"C:\Users\km\Desktop\playground\prgvscode";

        static void Main(string[] args) {
            var x = new string[] { ".bas", ".frm" };
            List<string> acc = new List<string>();
            GetAllPathsToFilesRecurse(ref acc, VbSourceDirectory, x);

            acc.ForEach(x => {
                string text = File.ReadAllText(x);
                File.WriteAllText(Path.Combine(DestinationDirectory, Path.GetFileName(x)), text);
            });
        }

        static void GetAllPathsToFilesRecurse(ref List<string> acc, string sDir, string[] extensions = null) {
            try {

                foreach (string f in Directory.GetFiles(sDir)) {
                    string ext = Path.GetExtension(f);
                    if (extensions == null || extensions.Contains(ext))
                        acc.Add(f);
                }

                foreach (string d in Directory.GetDirectories(sDir)) {
                    GetAllPathsToFilesRecurse(ref acc, d, extensions);
                }
            } catch (System.Exception excpt) {
                Console.WriteLine(excpt.Message);
            }
        }
    }
}
