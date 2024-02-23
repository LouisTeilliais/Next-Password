<<<<<<< HEAD
﻿using System.Text;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace NextPassword
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }

        private void CounterButton_Click(object sender, RoutedEventArgs e)
        {
            string workingDirectory = Environment.CurrentDirectory;
            // Load configuration from appsettings.json
            IConfiguration configuration = new ConfigurationBuilder()
                // Set path using parents to find root run folder 
                .SetBasePath(Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.FullName)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Retrieve values from appsettings.json
            string? name = configuration["APP_SETTINGS:DB_NAME"];
            string? password = configuration["APP_SETTINGS:DB_PASSWORD"];

            // Set XAML element text (dbName, dbPassword)
            dbName.Text = name;
            dbPassword.Text = password;
        }
    }
=======
﻿using NextPassword.MVVM.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NextPassword
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NextPassword.Navigate(new Register());
        }
    }
>>>>>>> 219c4f818d4965a9c71394c39831899f5a13ee63
}