using PizzeriaDOM.src.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        //return the biggest (last) ID of an employee type
        //return -1 if there is no file
        public static int getEmployeeLastID(string fileName)
        {
            string workingDirectory = Directory.GetCurrentDirectory();
            string combined = fileName + ".json";
            string path = System.IO.Path.Combine(workingDirectory, "..\\..\\..\\db", combined);
            int id = -1;

            //search in the corresponding file
            if (File.Exists(path))
            {
                if (fileName.Equals("Clerk"))
                {
                    List<Clerk> listFromFile = IOFile.ReadFromFile<Clerk>("Clerk");
                    foreach (Clerk c in listFromFile)
                    {
                        if (c.ID > id)
                        {
                            id = c.ID;
                        }
                    }

                    return id;
                }
                else if (fileName.Equals("DeliveryMan"))
                {
                    List<DeliveryMan> listFromFile = IOFile.ReadFromFile<DeliveryMan>("DeliveryMan");
                    foreach (DeliveryMan deliveryMan in listFromFile)
                    { 
                        if (deliveryMan.ID > id)
                        {
                            id = deliveryMan.ID;
                        }
                    }

                    return id;
                }
            }

            //if the file don't exist
            return -1;

        }
        public static bool EmptyProductFields(ObservableCollection<ProductPanel> Products)
        {
            foreach (var product in Products)
            {
                if (string.IsNullOrEmpty(product.selectedProduct) || string.IsNullOrEmpty(product.size))
                {
                    return true;
                }

            }
            return false;
        }
    }

    
}
