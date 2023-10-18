using PizzeriaDOM.src.classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaDOM.src.functions
{
    public class checkFunctions
    {
        public static Customer customerExist(String telephoneNumber)
        {
            string workingDirectory = Directory.GetCurrentDirectory();
            string combined = "Customer" + ".json";
            string path = System.IO.Path.Combine(workingDirectory, "..\\..\\..\\db", combined);


            if (File.Exists(path))
            {
                List<Customer> listFromFile = IOFile.ReadFromFile<Customer>("Customer");
                Customer customerFound = listFromFile.FirstOrDefault(c => c.TelephoneNumber == telephoneNumber);

                if (customerFound != null)
                {
                    return customerFound;
                }
                else { return null; }
            }
            else { return null; }
            

        }

    }
}
