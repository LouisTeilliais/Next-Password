using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace NextPassword.MVVM.Views
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        protected string password;
        protected string email;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }

        private void TextBox_TextChanged_Email(object sender, TextChangedEventArgs e)
        {
            email = Email.Text;
        }

        private void TextBox_TextChanged_Password(object sender, TextChangedEventArgs e)
        {
            password = Password.Text;
        }


    }
}
