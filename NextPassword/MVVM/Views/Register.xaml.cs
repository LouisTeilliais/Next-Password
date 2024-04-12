using NextPassword.MVVM._utils;
using NextPassword.MVVM.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        public string name;
        public string surname;
        public string email;
        public string password;

        private Api<User> Api = new Api<User>();

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (email != null && password != null)
            {
                UserBase userBase = new UserBase(email, password);
                ApiResponse<User> ApiResponse = await Api.CreateItemsAsync("/register", userBase);

                if (ApiResponse.StatusCode.Equals(200))
                {
                    NavigationService.Navigate(new Home());
                } else
                {
                    NavigationService.Navigate(new Login());
                }
            }

            return;
        }

        private void TextBox_TextChanged_Name(object sender, TextChangedEventArgs e)
        {
            name = Username.Text;
        }


        private void TextBox_TextChanged_Email(object sender, TextChangedEventArgs e)
        {
            email = Email.Text;
        }
        
        private void TextBox_TextChanged_Password(object sender, TextChangedEventArgs e)
        {
            password = Password.Text;
            SubmitButton.IsEnabled = password.Length >= 14;
        }

        private void Password_MouseEnter(object sender, MouseEventArgs e)
        {
            TooltipPassword.Visibility = Visibility.Visible;
        }
        private void Password_MouseLeave(object sender, MouseEventArgs e)
        {
            TooltipPassword.Visibility = Visibility.Hidden;
        }
    }
}
