using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;
using NextPassword.MVVM.Models;

namespace NextPassword.MVVM.Views
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            PasswordList = new ObservableCollection<Password>();
        }

        public ObservableCollection<Password> PasswordList { get; set; }

        private async void PasswordList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await FetchDataFromAPI();

                passwordList.ItemsSource = PasswordList;
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Erreur lors de la récupération des données : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task FetchDataFromAPI()
        {
            string baseUrl = "http://pokeapi.co/api/v2/pokemon/1";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                {
                    using (HttpContent content = res.Content)
                    {
                        var data = await content.ReadAsStringAsync();

                        if (data != null)
                        {
                            // Désérialisez le JSON dans une liste de Pokémon
                            var passwordList = JsonConvert.DeserializeObject<ApiResponse>(data);

                            if (passwordList?.Results != null)
                            {
                                PasswordList.Clear();

                                foreach (var password in passwordList.Results)
                                {
                                    PasswordList.Add(password);
                                }
                            }

                            // Retournez le premier élément de la liste
                            //return passwordList?.Results[3];
                        }
                        //else
                        //{
                          //  return null;
                        //}
                    }
                }
            }
        }
    }
}
