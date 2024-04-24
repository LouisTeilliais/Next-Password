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

namespace NextPassword.MVVM.Views
{
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            PasswordList = new ObservableCollection<Password>();
        }

        public ObservableCollection<Password> PasswordList { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Password> dataArray = await FetchDataFromAPI();


            // Afficher les données dans le TextBlock
            passwordList.ItemsSource = dataArray;

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

                if (response.StatusCode == 200)
                {
                    password_title.Text = response.Results.Title;
                    password_username.Text = response.Results.Username;
                    password_url.Text = response.Results.Url;
                    password_notes.Text = response.Results.Notes;
                    password_password.Password = DecryptPassword(response.Results.PasswordHash, response.Results.Token.tokenValue);
                    password_show.Text = DecryptPassword(response.Results.PasswordHash, response.Results.Token.tokenValue);
                }
            }
        }

        private async Task<ApiResponse<Password>> GetPasswordDetails(string passwordId)
        {
            string path = $"/api/password/{passwordId}";
            var api = new Api<Password>();
            var response = await api.GetItemsAsync(path, true);

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
    }
}