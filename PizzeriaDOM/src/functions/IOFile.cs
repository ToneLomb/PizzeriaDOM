using Newtonsoft.Json;
using PizzeriaDOM.src.classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net.Http.Json;
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


                if (File.Exists(path))
                {
                    List<Object> listFromFile = new List<Object>();
                    string dataFromFile = File.ReadAllText(path);
                    listFromFile = JsonConvert.DeserializeObject<List<object>>(dataFromFile);
                    listFromFile.AddRange(listToWrite);
                    string objectJson = JsonConvert.SerializeObject(listFromFile);
                    File.WriteAllText(path, objectJson);
                }
                else
                {
                    File.WriteAllText(path, JsonConvert.SerializeObject(listToWrite));
                }
            }
            catch (Exception e)
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
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                return new List<T>();
            }
        }

        public static int countOrders()
        {
            string workingDirectory = Directory.GetCurrentDirectory();
            string path = System.IO.Path.Combine(workingDirectory, "..\\..\\..\\db", "Orders.json");

            if (File.Exists(path))
            {
                string ObjectJson = File.ReadAllText(path);
                List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(ObjectJson);
                return orders.Count;
            }
            else
            {
                return 1;
            }
        }
        public static void updateDeliveryManDisponibility(DeliveryMan deliveryMan, bool available)
        {
            List<DeliveryMan> deliveryMen = ReadFromFile<DeliveryMan>("DeliveryMan");

            DeliveryMan deliveryManToUpdate = deliveryMen.FirstOrDefault(man => man.ID == deliveryMan.ID);

            if (deliveryManToUpdate != null)
            {
                deliveryManToUpdate.Available = available;
                string updatedList = JsonConvert.SerializeObject(deliveryMen);
                string workingDirectory = Directory.GetCurrentDirectory();
                string path = System.IO.Path.Combine(workingDirectory, "..\\..\\..\\db", "DeliveryMan.json");
                File.WriteAllText(path, updatedList);
            }


        }

        public static DeliveryMan GetDeliveryMan()
        {
            List<DeliveryMan> deliveryMen = ReadFromFile<DeliveryMan>("DeliveryMan");
            DeliveryMan deliveryMan = deliveryMen.FirstOrDefault(man => man.Available == true);
            return deliveryMan;

            
        }

        public static void updateOrder(Order order, string state)
        {
            List<Order> orderFromFile = ReadFromFile<Order>("Orders");

            Order orderToUpdate = orderFromFile.FirstOrDefault(e => e.ID == order.ID);

            if (orderToUpdate != null)
            {
                orderToUpdate.State = state;
                string updatedList = JsonConvert.SerializeObject(orderFromFile);
                string workingDirectory = Directory.GetCurrentDirectory();
                string path = System.IO.Path.Combine(workingDirectory, "..\\..\\..\\db", "Orders.json");
                File.WriteAllText(path, updatedList);
            }
            else
            {
                return;
            }

        }
    }
}