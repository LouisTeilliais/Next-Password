using System.Configuration;
using System.Data;
using System.Windows;
using System.Xml.Linq;

namespace NextPassword
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public NextPasswordApp AppTest { get; set; }
        public App()
        {
            AppTest = new NextPasswordApp();
        }

    }

    public partial class NextPasswordApp
    {
        public string nextPasswordName = "Hello World";
    }

}
