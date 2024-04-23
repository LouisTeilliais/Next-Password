﻿using NextPassword.MVVM._utils;
using NextPassword.MVVM._utils.Interface;
using NextPassword.MVVM.Models;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace NextPassword.MVVM.Views
{
    public partial class Login : Page
    {
        protected string password;
        protected string email;
        private Api<User> Api = new Api<User>();

        private readonly IDialogService _dialogService;
        public Login()
        {
            InitializeComponent();
            _dialogService = new DialogService();
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (email != null && password != null)
                {
                    UserBase userBase = new UserBase(email, password);
                    ApiResponse<User> ApiResponse = await Api.CreateItemsAsync("/login", userBase, false);

                    if (ApiResponse.StatusCode.Equals(200))
                    {
                        NavigationService.Navigate(new Home());
                    } else
                    {
                        _dialogService.ShowMessage($"Connexion impossible avec l'utilisateur possédant l'email suivant : {email}");
                    }
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage($"Une erreur s'est produite : {ex.Message}");
            }
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
