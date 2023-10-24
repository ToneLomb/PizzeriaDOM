using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaDOM.src.classes
{
    public class Customer
    {
        private string _surname;
        private string _firstName;
        private string _address;
        private string _telephoneNumber;
        private DateTime _dateFirstOrder;
        private int _purchaseNumber;

        public Customer(string surname, string firstName, string address, string telephoneNumber, DateTime dateFirstOrder)
        {
            this._surname = surname;
            this._firstName = firstName;
            this._address = address;
            this._telephoneNumber = telephoneNumber;
            this._dateFirstOrder = dateFirstOrder;
            this._purchaseNumber = 0;
        }

        public string Surname
        {
            get => _surname;
            set => _surname = value;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string Adress
        {
            get => _address;
            set => _address = value;
        }

        public string TelephoneNumber
        {
            get => _telephoneNumber;
            set => _telephoneNumber = value;
        }

        public DateTime DateFirstOrder
        {
            get => _dateFirstOrder;
            set => _dateFirstOrder = value;
        }

        public int PurchaseNumber
        {
            get { return _purchaseNumber; }
            set => _purchaseNumber = value;
        }
        public override string ToString()
        {
            return $"Customer Information:\n" +
                   $"Name: {FirstName} {Surname}\n" +
                   $"Address: {Adress}\n" +
                   $"Telephone Number: {TelephoneNumber}\n" +
                   $"Date of First Order: {DateFirstOrder}\n" +
                   $"Purchase Number: {PurchaseNumber}";
        }

    }
}