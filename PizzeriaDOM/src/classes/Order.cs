using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaDOM.src.classes
{
    public class Order
    {
        private int _ID;
        private double _priceOrder;
        private string _state; // [Preparation, delivery, closed, lost]
        private DateTime _dateOrder;
        private List<Product> _product;
        private Customer _customer;

        private int _kitchenCountdown = 15;
        private Clerk _clerk;


        //DÃ©finir un certain prix selon type / size pizza

        public Order(int ID, Customer customer, double priceOrder, string state, DateTime dateOrder, List<Product> product, Clerk clerk)
        {
            this._ID = ID;
            this._customer = customer;
            this._priceOrder = priceOrder;
            this._state = state;
            this._dateOrder = dateOrder;
            this._product = product;
            this._clerk = clerk;
        }

        public int ID
        {
            get => _ID;
            set => _ID = value;
        }

        public Customer Customer
        {
            get => _customer;
            set => _customer = value;
        }

        public Clerk Clerk
        {
            get => _clerk;
            set => _clerk = value;
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

        public int KitchenCountdown
        {
            get => _kitchenCountdown;
            set => _kitchenCountdown = value;
        }
        public override string ToString()
        {
            string productInfo = string.Join(", ", _product.Select(p => p.ToString()));

            return $"Order ID: {_ID}\nCustomer: {_customer}\nPrice: {_priceOrder:C2}\nState: {_state}\nDate: {_dateOrder}\nProducts: {productInfo}";
        }

        public class Product
        {
            private string _size;
            private string _name;
            private double _price;

            public Product(string size, string type, double price)
            {
                this._size = size;
                this._name = type;
                this._price = price;
            }

            public string Size
            {
                get => _size;
                set => _size = value;
            }

            public string Name
            {
                get => _name;
                set => _name = value;
            }

            public double Price
            {
                get => _price;
                set => _price = value;
            }

        }
    }
}
