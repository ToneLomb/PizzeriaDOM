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

namespace PizzeriaDOM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
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
