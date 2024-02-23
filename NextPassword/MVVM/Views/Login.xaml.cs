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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }

        private void TextBox_TextChanged1(object sender, TextChangedEventArgs e)
        {

        }

       
    }
}
