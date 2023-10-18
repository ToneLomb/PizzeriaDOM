using PizzeriaDOM.src.classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using PizzeriaDOM.src.functions;
using System.IO;

namespace PizzeriaDOM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Customer c = new Customer("Kohler", "Arnaud", "Trappes", "0661735027", DateTime.Now);
            Customer c2 = new Customer("Brancolini", "Lucas", "Villejuif", "0000000000", DateTime.Now);

            List<Object> list = new List<Object>();
            list.Add(c);
            list.Add(c2);
            IOFile.WriteInFile(list, "Customer");
            List<Customer> c1 = IOFile.ReadFromFile<Customer>("Customer");

            Trace.WriteLine(c1.Count);
            Customer premierClient = c1[0];
            Customer deuxiemeClient = c1[1];
            Trace.WriteLine(premierClient.ToString());
            Trace.WriteLine(deuxiemeClient.ToString());

            InitializeComponent();
            CC.Content = new PizzeriaDOM.Pages.TakeOrder();
        }

        private void ToggleButtonClick(ToggleButton selectedButton)
        {
            // Parcourir tous les ToggleButton dans la NavBar
            foreach (var child in NavBar.Children)
            {   
                // Si c'est un ToggleButton
                if (child is ToggleButton toggleButton)
                {
                    // On le désactive (ils seront donc tous désactivés)
                    toggleButton.IsChecked = false;
                }
            }

            // Sélectionner le bouton actuel et l'active
            selectedButton.IsChecked = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new PizzeriaDOM.Pages.TakeOrder();
            ToggleButtonClick(button1);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new PizzeriaDOM.Pages.Kitchen();
            ToggleButtonClick(button2);
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = new PizzeriaDOM.Pages.Employee();
            ToggleButtonClick(Employee);
		}
	}
}
