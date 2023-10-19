using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaDOM.src.classes
{
    internal class Order
    {
        private int _ID;
        private string _customerTelephoneNumber;
        private double _priceOrder;
        private string _state; // [Preparation, delivery, closed, lost]
        private DateTime _dateOrder;
        private List<Product> _product;

        //DÃ©finir un certain prix selon type / size pizza

        public Order(int ID, string customerTelephoneNumber, double priceOrder, string state, DateTime dateOrder, List<Product> product)
        {
            this._ID = ID;
            this._customerTelephoneNumber = customerTelephoneNumber;
            this._priceOrder = priceOrder;
            this._state = state;
            this._dateOrder = dateOrder;
            this._product = product;
        }

        public int ID
        {
            get => _ID;
            set => _ID = value;
        }

        public string CustomerTelephoneNumber
        {
            get => _customerTelephoneNumber;
            set => _customerTelephoneNumber = value;
        }

        public double PriceOrder
        {
            get => _priceOrder;
            set => _priceOrder = value;
        }

        public string State
        {
            get => _state;
            set => _state = value;
        }

        public DateTime DateOrder
        {
            get => _dateOrder;
            set => _dateOrder = value;
        }

        public List<Product> ProductList
        {
            get => _product;
            set => _product = value;
        }
        public override string ToString()
        {
            string productInfo = string.Join(", ", _product.Select(p => p.ToString()));

            return $"Order ID: {_ID}\nCustomer Telephone Number: {_customerTelephoneNumber}\nPrice: {_priceOrder:C2}\nState: {_state}\nDate: {_dateOrder}\nProducts: {productInfo}";
        }

        public class Product
        {
            private string _size;
            private string _type;
            private int _price;

            public Product(string size, string type, int price)
            {
                this._size = size;
                this._type = type;
                this._price = price;
            }

            public string Size
            {
                get => _size;
                set => _size = value;
            }

            public string Type
            {
                get => _type;
                set => _type = value;
            }

            public int Price
            {
                get => _price;
                set => _price = value;
            }
        }
    }
}
