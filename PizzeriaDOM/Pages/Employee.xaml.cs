using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PizzeriaDOM.src.classes;
using PizzeriaDOM.src.functions;

namespace PizzeriaDOM.Pages
{
    /// <summary>
    /// Logique d'interaction pour Employee.xaml
    /// </summary>
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
            newEmployee.Show();
		}

        //read all employee from files then display them on a ListView
        private void updateEmployeeList()
        {
            //read each file
            List<Clerk> clerkList = IOFile.ReadFromFile<Clerk>("Clerk");
            List<DeliveryMan> deliveryManList = IOFile.ReadFromFile<DeliveryMan>("DeliveryMan");
            
            //concat the 2 resulting list
            List<object> employeeList = clerkList.Cast<object>().Concat(deliveryManList.Cast<object>()).ToList();

            employeeListView.ItemsSource = employeeList;

        }
    }
}
