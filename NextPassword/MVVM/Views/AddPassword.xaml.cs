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

        protected new string Title;
        protected string Link;
        protected string Password;
        protected string ConfirmationPassword;
        protected string Email;

        private void TextBox_TextChanged_Title(object sender, TextChangedEventArgs e)
        {
            Title = TitlePassword.Text;
        }

        private void TextBox_TextChanged_Link(object sender, TextChangedEventArgs e)
        {
            Link = LinkPassword.Text;
        }

        private void TextBox_TextChanged_Password(object sender, TextChangedEventArgs e)
        {
            Password = NewPassword.Text;
        }

        private void TextBox_TextChanged_PasswordConfirmation(object sender, TextChangedEventArgs e)
        {
            ConfirmationPassword = NewPasswordConfirmation.Text;
        }

    }
}
