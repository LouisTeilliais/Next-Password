using NextPassword.MVVM._utils;
using NextPassword.MVVM._utils.Interface;
using NextPassword.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
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
        protected string title;
        protected string? link;
        protected string password;
        protected string confirmationPassword;
        protected string email;
        protected string? notes;
        protected string? username;

        private Api<Password> Api = new Api<Password>();

        private readonly IDialogService _dialogService;

        public AddPassword()
        {
            InitializeComponent();
            _dialogService = new DialogService();
        }



        private async void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            // Check password confirmation value
            if (password != confirmationPassword)
            {
                _dialogService.ShowMessage("Les mots de passe ne correspondent pas. Veuillez vérifier que les deux mots de passe sont identiques.");
            }

            /* TODO */
            if (title != null && password != null)
            {
                Password passwordBody = new Password(null, title, password, notes, username, link, null);

                ApiResponse<Password> ApiResponse = await Api.CreateItemsAsync("/api/password", passwordBody, true);

                if (ApiResponse.StatusCode.Equals(200))
                {
                    NavigationService.Navigate(new Home());
                } else
                {
                    _dialogService.ShowMessage($"Le mot de passe n'a pas pu être créé, serveur réponse code : {ApiResponse.StatusCode}");
                }
            } else
            {
                _dialogService.ShowMessage("Mot de passe ou titre vides, veuillez remplir ses 2 champs obligatoires");
                Console.WriteLine("Password or title field empty");
            }

            return;
        }

        private void TextBox_TextChanged_Title(object sender, TextChangedEventArgs e)
        {
            title = TitlePassword.Text;
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

        private void TextBox_TextChanged_Notes(object sender, RoutedEventArgs e)
        {
            notes = NotePassword.Text;
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

        private void Button_Generate_Password(object sender, RoutedEventArgs e)
        {
            // Can set password length 
            string newPasswordGenerated = Generation.GenerateRandomPassword();
            if (newPasswordGenerated == null)
            {
                _dialogService.ShowMessage("Demande de génération d'un mot de passe trop court");
                return;
            }
            password = Generation.GenerateRandomPassword();
            confirmationPassword = password;

            NewPassword.Text = password;
            NewPasswordConfirmation.Text = confirmationPassword;
        }


    }
}
