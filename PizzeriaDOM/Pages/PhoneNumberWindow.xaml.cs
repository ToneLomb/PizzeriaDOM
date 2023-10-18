using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PizzeriaDOM.Pages
{
    /// <summary>
    /// Logique d'interaction pour PhoneNumberWindow.xaml
    /// </summary>
    public partial class PhoneNumberWindow : Window
    {
        public PhoneNumberWindow()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LabelPhoneNumber.Content = "You Entered: " + TextPhoneNumber.Text;
                this.Close();
            }
        }
    }
}
