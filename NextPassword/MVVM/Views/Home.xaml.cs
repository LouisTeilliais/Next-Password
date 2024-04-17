using NextPassword.MVVM._utils;
using NextPassword.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }


        public void Button_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new AddPassword());
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Password> dataArray = await FetchDataFromAPI();


            // Afficher les données dans le TextBlock
            dataGrid.ItemsSource = dataArray;

        }

        private async Task<List<Password>> FetchDataFromAPI()
        {
            string path = "/api/password";
            var api = new Api<List<Password>>();
            var response = await api.GetItemsAsync(path);

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
