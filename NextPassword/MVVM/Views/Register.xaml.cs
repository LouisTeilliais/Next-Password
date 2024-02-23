using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace NextPassword.MVVM.Views
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

<<<<<<< HEAD
        public string name;
        public string surname;
        public string email;
        public string password;
        public int countClick = 0;
        public Button submit;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            name = Name.Text;
            surname = Surname.Text;
            email = Email.Text;
            password = Password.Text;
        }

        private void TextBox_TextChanged_Name(object sender, TextChangedEventArgs e)
        {
            name = Name.Text;
        }

        private void TextBox_TextChanged_Surname(object sender, TextChangedEventArgs e)
        {
            surname = Surname.Text;
        }

        private void TextBox_TextChanged_Email(object sender, TextChangedEventArgs e)
        {
            email = Email.Text;
        }

        private void TextBox_TextChanged_Password(object sender, TextChangedEventArgs e)
        {
            password = Password.Text;
        }

        private void Is_Submit_Enable(object sender, EventArgs e)
        {
            if ()
            submit.IsEnabled = true;
=======
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

>>>>>>> 219c4f818d4965a9c71394c39831899f5a13ee63
        }

        /*private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // Open the URL in the default web browser
            NavigationService.Navigate(new Uri("Login.xaml"));
            
        }*/
    }
}
