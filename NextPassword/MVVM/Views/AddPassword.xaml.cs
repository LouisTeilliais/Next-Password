using NextPassword.MVVM._utils;
using NextPassword.MVVM.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NextPassword.MVVM.Views
{
    /// <summary>
    /// Logique d'interaction pour AddPassword.xaml
    /// </summary>
    public partial class AddPassword : Page
    {
        public AddPassword()
        {
            InitializeComponent();
        }

        protected string title;
        protected string? link;
        protected string password;
        protected string confirmationPassword;
        protected string email;
        protected string? notes;
        protected string? username;

        private Api<Password> Api = new Api<Password>();

        public async void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            /* TODO */
            if (title != null && password != null)
            {
                Password passwordBody = new Password(null, title, password, notes, username, link);
                /*Trace.WriteLine(userBase.Password, userBase.Email);*/
                ApiResponse<Password> ApiResponse = await Api.CreateItemsAsync("/api/password", passwordBody, true);

                MessageBox.Show(ApiResponse.StatusCode.ToString());
                /*Trace.WriteLine(ApiResponse.StatusCode);*/
                if (ApiResponse.StatusCode.Equals(200))
                {
                    NavigationService.Navigate(new Home());
                }
            } else
            { 
                // TODO Create modal to warn user if require field is empty
                Console.WriteLine("Password or title field empty");
            }

            return;
        }

        private void TextBox_TextChanged_Title(object sender, TextChangedEventArgs e)
        {
            Title = TitlePassword.Text;
        }

        private void TextBox_TextChanged_Link(object sender, TextChangedEventArgs e)
        {
            link = LinkPassword.Text;
        }

        private void TextBox_TextChanged_Password(object sender, TextChangedEventArgs e)
        {
            password = NewPassword.Text;
        }

        private void TextBox_TextChanged_PasswordConfirmation(object sender, TextChangedEventArgs e)
        {
            confirmationPassword = NewPasswordConfirmation.Text;
            /*SubmitButton.IsEnabled = password.Length >= 14;*/
        }

        private void TextBox_TextChanged_Username(object sender, TextChangedEventArgs e)
        {
            username = UsernamePassord.Text;
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }
        private void Button_MouseEnter_ToolTip(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.ToolTip = "Mot de passe de 12 caractères minimum, contenant des chiffres, majuscules, minimum et caractères spéciaux";
        }

        private void Button_MouseLeave_ToolTip(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.ToolTip = null;
        }


    }
}
