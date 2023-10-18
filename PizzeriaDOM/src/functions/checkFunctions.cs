using PizzeriaDOM.src.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaDOM.src.functions
{
    public class checkFunctions
    {
        public static Boolean customerExist(String telephoneNumber)
        {
            List<Customer> listFromFile = IOFile.ReadFromFile<Customer>("Customer");
            Customer customerFound = listFromFile.FirstOrDefault(c => c.TelephoneNumber == telephoneNumber);

            if (customerFound != null)
            {
                return true;
            }
            else { return false; }

        }

    }
}
