using Newtonsoft.Json;
using NextPassword.MVVM.Models;
using NextPassword.MVVM._utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using static MaterialDesignThemes.Wpf.Theme;
using NextPassword.MVVM._utils.Interface;

namespace NextPassword.MVVM.Views
{
    public partial class Home : Page
    {
        public Password SelectedPasswordDetails { get; set; }
        public ObservableCollection<Password> PasswordList { get; set; }

        private readonly IDialogService _dialogService;

        private bool isFieldEnabled;

        protected string title;
        protected string? link;
        protected string password;
        // protected string confirmationPassword;
        protected string? notes;
        protected string? username;

        public Home()
        {
            InitializeComponent();
            PasswordList = new ObservableCollection<Password>();
            _dialogService = new DialogService();
            isFieldEnabled = false;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Password> dataArray = await FetchDataFromAPI();


            // Afficher les données dans le TextBlock
            passwordList.ItemsSource = dataArray;

        }

        public void Can_update_password(object sender, RoutedEventArgs e)
        {
            SetUpdateMode();
        }

        public async void Update_password(object sender, RoutedEventArgs e)
        {
            if (password_password.Password == null)
            {
                _dialogService.ShowMessage("Votre mot de passe est vide, veuillez le remplir");
                return;
            }

            SelectedPasswordDetails.PasswordHash = password_password.Password;

            if (SelectedPasswordDetails != null)
            {
                string? passwordId = SelectedPasswordDetails.Id != null ? SelectedPasswordDetails.Id : null;

                if (passwordId != null)
                {
                    ApiResponse<Password> updatedPassword = await UpdatePassword(passwordId, SelectedPasswordDetails);

                    if (updatedPassword.StatusCode == 200)
                    {
                        SelectedPasswordDetails = updatedPassword.Results;

                        // Change password field value by new values inserted
                        SetPasswordFieldValue(SelectedPasswordDetails);
                        SetUpdateMode();
                    } else
                    {
                        _dialogService.ShowMessage($"Serveur erreur {updatedPassword.StatusCode}");
                    }
                    
                } else
                {
                    _dialogService.ShowMessage("Pas de mot de passe trouvé pour la suppression");
                }
            } else
            {
                _dialogService.ShowMessage("Pas de nouvelle modification à apporter");
            }
            return;
        }

        public async void Delete_password(object sender, RoutedEventArgs e)
        {
            if (SelectedPasswordDetails != null)
            {
                string? passwordId = SelectedPasswordDetails.Id != null ? SelectedPasswordDetails.Id : null;
                
                if (passwordId != null)
                {
                    ApiResponse<Password> deletePassword = await DeletePassword(passwordId);

                    if (deletePassword.StatusCode == 200)
                    {
                        NavigationService.Navigate(new Home());
                    }
                    
                } else
                {
                    _dialogService.ShowMessage("Pas de mot de passe trouvé à effacer");
                }
                
            } else
            {
                _dialogService.ShowMessage("Aucun mot de passe sélectionné");
            }
        }

        public void Add_password(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPassword());
        }

        private async Task<List<Password>> FetchDataFromAPI()
        {
            string path = "/api/password";
            var api = new Api<List<Password>>();
            var response = await api.GetItemsAsync(path, true);

            if (response.StatusCode == 200)
            {
                return response.Results;
            }
            else
            {
                return new List<Password>();
            }
        }

        private async void passwordList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Password selectedPassword = (Password)passwordList.SelectedItem;
            if (selectedPassword != null)
            {
                string passwordId = selectedPassword.Id;

                ApiResponse<Password> response = await GetPasswordDetails(passwordId);

                SelectedPasswordDetails = response.Results;

                if (response.StatusCode == 200)
                {
                    SetPasswordFieldValue(SelectedPasswordDetails);

                    if (!can_update_button.IsEnabled) {
                        can_update_button.IsEnabled = true;
                    }

                    if (!delete_button.IsEnabled)
                    {
                        delete_button.IsEnabled = true;
                    }
                }
            }
        }

        private void SetPasswordFieldValue(Password password)
        {
            password_title.Text = password.Title;
            password_username.Text = password.Username;
            password_url.Text = password.Url;
            password_notes.Text = password.Notes;
            password_password.Password = DecryptPassword(password.PasswordHash, password.Token.tokenValue);
            password_show.Text = DecryptPassword(password.PasswordHash, password.Token.tokenValue);
        }

        private void SetUpdateMode()
        {
            isFieldEnabled = !isFieldEnabled;

            password_title.IsEnabled = isFieldEnabled;
            password_username.IsEnabled = isFieldEnabled;
            password_url.IsEnabled = isFieldEnabled;
            password_notes.IsEnabled = isFieldEnabled;
            password_password.IsEnabled = isFieldEnabled;
        }

        private async Task<ApiResponse<Password>> GetPasswordDetails(string passwordId)
        {
            string path = $"/api/password/{passwordId}";
            var api = new Api<Password>();
            var response = await api.GetItemsAsync(path, true);

            return response;
        }

        private async Task<ApiResponse<Password>> DeletePassword(string passwordId)
        {
            string path = $"/api/password/{passwordId}";
            var api = new Api<Password>();
            var response = await api.DeleteItemsAsync(path, true);

            return response;
        }

        private async Task<ApiResponse<Password>> UpdatePassword(string passwordId, Password newPassword)
        {
            string path = $"/api/password/{passwordId}";
            var api = new Api<Password>();
            var response = await api.UpdateItemsAsync(path, newPassword, true);

            return response;
        }

        public string DecryptPassword(string encryptedPassword, string token)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(encryptedPassword);

            using (Aes aes = Aes.Create())
            {
                aes.Key = GetAesCompatibleKey(token, aes.KeySize / 8); // Get a compatible key
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        private byte[] GetAesCompatibleKey(string token, int keySize)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
                Array.Resize(ref key, keySize);
                return key;
            }
        }

        private void showPasswordToggle_Checked(object sender, RoutedEventArgs e)
        {
            password_password.Visibility = System.Windows.Visibility.Collapsed;
            password_show.Visibility = System.Windows.Visibility.Visible;

            password_show.Focus();
        }

        private void showPasswordToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            password_password.Visibility = System.Windows.Visibility.Visible;
            password_show.Visibility = System.Windows.Visibility.Collapsed;

            password_password.Focus();
        }

        private void Copy_Password(object sender, RoutedEventArgs e)
        {
            string password = password_password.Password;
            Clipboard.SetText(password);
            MessageBox.Show("Mot de passe copié dans le presse-papiers !");
        }

        private void TextBox_TextChanged_Title(object sender, TextChangedEventArgs e)
        {
            SelectedPasswordDetails.Title = password_username.Text;
        }

        private void TextBox_TextChanged_Link(object sender, TextChangedEventArgs e)
        {
            SelectedPasswordDetails.Url = password_url.Text;
        }

        private void TextBox_TextChanged_Username(object sender, TextChangedEventArgs e)
        {
            SelectedPasswordDetails.Username = password_username.Text;
        }

        private void TextBox_TextChanged_Note(object sender, TextChangedEventArgs e)
        {
            SelectedPasswordDetails.Notes = password_notes.Text;
        }
    }
}