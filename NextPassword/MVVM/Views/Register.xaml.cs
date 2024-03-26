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
/*        private string _messageErreur;

        public string MessageErreur
        {
            get { return _messageErreur; }
            set
            {
                if (_messageErreur != value)
                {
                    _messageErreur = value;
                    OnPropertyChanged();
                }
            }
        }*/

        private Api<User> Api = new Api<User>();

/*        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Méthode pour générer un message d'erreur
        public void GenererErreur(string message)
        {
            MessageErreur = message;
        }*/

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("Button was click");
            if (email != null && password != null )
            {
                UserBase userBase = new UserBase(email, password);
                ApiResponse<User> ApiResponse = await Api.CreateItemsAsync("/register", userBase);

                if (ApiResponse.StatusCode.Equals(200) ) { 
                    NavigationService.Navigate(new Home());
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

        /*private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // Open the URL in the default web browser
            NavigationService.Navigate(new Uri("Login.xaml"));
            
        }*/
    }
}
