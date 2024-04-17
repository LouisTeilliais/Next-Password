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

namespace NextPassword.MVVM.Views
{
    public partial class Page1 : Page
    {
        public Page1()
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
    }
}
