using NextPassword.MVVM._utils;
using NextPassword.MVVM.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NextPassword.MVVM._utils.Interface;

namespace NextPassword.MVVM.Views
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Register : Page
    {
        public string name;
        public string email;
        public string password;

        private Api<User> Api = new Api<User>();

        private readonly IDialogService _dialogService;
        public Register()
        {
            InitializeComponent();
            _dialogService = new DialogService();
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (email != null && password != null)
            {
                UserBase userBase = new UserBase(email, password);
                ApiResponse<User> ApiResponse = await Api.CreateItemsAsync("/register", userBase);

                if (ApiResponse.StatusCode.Equals(200))
                {
                    NavigationService.Navigate(new Login());
                } else
                {
                    _dialogService.ShowMessage($"Impossible de créer l'utilisateur : {email}");
                }
            } else
            {
                _dialogService.ShowMessage($"Le champ email et/ou password est vide, veuillez remplir ses deux champs obligatoires");
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
