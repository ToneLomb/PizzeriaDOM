using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriaDOM.src.classes;

namespace PizzeriaDOM.src.functions
{
    class tools
    {
        public static double getPrice(String productName,String fileName, String size)
        {
            double price = 0;
            List<Order.Product> products = IOFile.ReadFromFile < Order.Product>(fileName);
            foreach (Order.Product product in products)
            {
                if(product.Name == productName) price = product.Price; break;
            }
            if (size == "Small") return price;
            else if (size == "Medium") return price * 1.5;
            else return price * 2;

        }

    }
}
