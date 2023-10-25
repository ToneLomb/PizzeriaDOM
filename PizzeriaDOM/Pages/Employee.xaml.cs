using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PizzeriaDOM.src.classes;
using PizzeriaDOM.src.functions;

namespace PizzeriaDOM.Pages
{
    public partial class Employee : UserControl
    {
        public Employee()
        {
            InitializeComponent();

            updateEmployeeList();
		}

        private void NewEmployee_Click(object sender, RoutedEventArgs e)
        {
            NewEmployeeWindow newEmployee = new NewEmployeeWindow();
            newEmployee.ShowDialog();

            updateEmployeeList();
		}

        //read all employee from files then display them on a ListView
        private void updateEmployeeList()
        {
            //read each file
            List<Clerk> clerkList = IOFile.ReadFromFile<Clerk>("Clerk");
            List<DeliveryMan> deliveryManList = IOFile.ReadFromFile<DeliveryMan>("DeliveryMan");
            
            //concat the 2 resulting list
            List<object> employeeList = clerkList.Cast<object>().Concat(deliveryManList.Cast<object>()).ToList();

            //use the list as the source of the ListView
            employeeListView.ItemsSource = employeeList;
        }
    }
}
