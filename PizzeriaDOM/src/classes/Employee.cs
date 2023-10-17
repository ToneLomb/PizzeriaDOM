﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaDOM.src.classes
{
    internal abstract class Employee
    {
        public abstract int OrderManaged { get; set; }
        public abstract int ID { get; set; }

    }

        internal class Clerk : Employee
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
        }


        internal class DeliveryMan : Employee
        {
            private int _orderDelivered;
            private int _ID;

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
        }
}
