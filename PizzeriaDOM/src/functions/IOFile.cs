using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PizzeriaDOM.src.functions
{
    public class IOFile
    {
        //Fonction qui écrit un objet sous format Json dans le fichier (donné le nom)
        public static void WriteInFile(Object toWrite, string fileName)
        {
            try
            {
                string workingDirectory = Directory.GetCurrentDirectory();
                string combined = fileName + ".json";
                string path = System.IO.Path.Combine(workingDirectory, "..\\..\\..\\db", combined);
                string objectJson = JsonConvert.SerializeObject(toWrite);
                File.WriteAllText(path, objectJson);
            }
            catch ( Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }

    }
}
