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
            dataGrid.ItemsSource = dataArray;
        }

        private async Task<List<Password>> FetchDataFromAPI()
        {
            string path = "/api/password";
            string bearerToken = "CfDJ8Ji46fvlb2pPnQCQ02Wp-X3uMgwPWnH7_kI_yy270leuStl1MyemaISFGZFXlYH2AsmgzIH9N4Zmoolyv_7U-hzQlMKleCF6C91fsvWjLJzP4QFj7agChMJe282vOTSjN4lo69v3wgXgTl-SwT6b1h2jEdtKFhY2v9U6y93-MG9kdjYy0kodtEgj1e4fBQ-DNAaGgMIPpzYNgI_s5Zqao11ftdcV_Y2nVPm49QqnZaNgZbxnhKaSMc3TxHLeqIDe-cb_xi4XGL4_8kYq_NB2h1UgnpXWFVEcQA10pEGC8Ksquk5PDBp_F3E-tCm2NKbsWPx8ExAJ-soUgF_APYAx2bbqMLe1cvacqx_SmLyoVM-08vUinHKjJ43eaqBF_Mo75c8bKI5fAMcA6NnNVRIVLCOkUNNKYNe3aiFOnB82HmeRQ_I4lh3I-egai59j3yBlNtH3qpbb3rt2JxbuOakzpgfOcRcW4YC87G5orcPsxk5R7n12q9deo20_CY9r3C89DhBs7zpfTuQnwhqc3oc15gP4FZq5J8lwty3rWUV_gVc_rm132F0Brjn2JmLWgQiNAfwMEtY1h_kJI6nGoEdLrcMKoeUCgPgzlDRwlJJ2mLzp3tbGlpjDXIu63rzpEaT5K6lhh6CotcSPHSDQevUQXeUMaMjQJeV_AuV7q5GG2YBqJ1p3XnnpqG7sllozzcTfIA";
            var api = new Api<Password>();
            var response = await api.GetPasswordsAsync<Password>(path, bearerToken);

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
