using System;
using System.Collections.Generic;
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

namespace Site
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            Worker();
            Broken();

        }

        public static async void Broken()
        {
            string data = Parser().Result;
        }

        public static async void Worker()
        {
            string data1 = await Parser();
        }
        static async Task<string> Parser()
        {
            var httpClient = new HttpClient();
            await Task.Delay(200);
            return await httpClient.GetStringAsync("https://docs.google.com/presentation/d/1IaVhC8PLxB5ktrOtdBl3y34v7sB6SCGDpCi6Y-MXJBI/edit?slide=id.g35f82db78fd_0_104#slide=id.g35f82db78fd_0_104");
        }
    }
}
