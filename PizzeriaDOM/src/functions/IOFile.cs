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
        //Fonction qui écrit une liste objet sous format Json dans le fichier (donné le nom)
        //Envoyer : List<Object> list = new List<Object>
        public static void WriteInFile(List<Object> listToWrite, string fileName)
        {
            try
            {
                string workingDirectory = Directory.GetCurrentDirectory();
                string combined = fileName + ".json";
                string path = System.IO.Path.Combine(workingDirectory, "..\\..\\..\\db", combined);

                List<Object> listFromFile = new List<Object>();
                if (File.Exists(path))
                {
                    string dataFromFile = File.ReadAllText(path);
                    listFromFile = JsonConvert.DeserializeObject<List<object>>(dataFromFile);
                }

                listFromFile.AddRange(listToWrite);


                string objectJson = JsonConvert.SerializeObject(listFromFile);
                File.WriteAllText(path, objectJson);
            }
            catch ( Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }

        //Function qui une liste d'objet d'un fichier
        //Exemple : List<Customer> listCustomer = IOFile.ReadFromFile<Customer>("Customer");
        public static List<T> ReadFromFile<T>(string fileName)
        {
            try
            {
                string workingDirectory = Directory.GetCurrentDirectory();
                string combined = fileName + ".json";
                string path = System.IO.Path.Combine(workingDirectory, "..\\..\\..\\db", combined);

                if (File.Exists(path))
                {
                    string ObjectJson = File.ReadAllText(path);
                    return JsonConvert.DeserializeObject<List<T>>(ObjectJson);
                }
                else
                {
                    Trace.WriteLine($"Could not load {fileName}");
                    return new List<T>();
                }
            } 
            catch ( Exception e )
            {
                Trace.WriteLine(e.ToString());
                return new List<T>();
            }
        }

    }
}
