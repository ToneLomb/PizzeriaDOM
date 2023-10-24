using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaDOM.src.classes
{
    public abstract class Employee
    {
        public abstract int OrderManaged { get; set; }
        public abstract int ID { get; set; }

    }

    public class Clerk : Employee
        {
            private int _orderManaged;
            private int _ID;

            public override int OrderManaged
            {
                get => _orderManaged;
                set => _orderManaged = value;
            }

            public override int ID
            {
                get => _ID;
                set => _ID = value;
            }

            public string typeName
            {
                get { return this.GetType().Name; }
            }
    }


        public class DeliveryMan : Employee
        {
            private int _orderDelivered;
            private int _ID;
            private bool _available;

        public DeliveryMan(int orderDelivered, int ID, bool available)
        {
            this._ID = ID;
            this._orderDelivered = orderDelivered;
            this._available = available;
        }

            public override int OrderManaged
            {
                get => _orderDelivered;
                set => _orderDelivered = value;
            }

            public override int ID
            {
                get => _ID;
                set => _ID = value;
            }

            public bool Available
            {
                get => _available;
                set => _available = value;
            }

            public string typeName
            {
                get { return this.GetType().Name; }
            }
    }
}
