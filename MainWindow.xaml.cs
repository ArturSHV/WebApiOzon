using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WPF_API_OZON
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApiData apiData = new ApiData();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = apiData;


            GetList.Click += (s, e) =>
            {
                if (!String.IsNullOrEmpty(ApiKey.Text) || (!String.IsNullOrEmpty(ClientId.Text)))
                    apiData.GetList(ApiKey.Text, ClientId.Text);
                else
                    MessageBox.Show("Введите Api-Key и Client-id");
            };
            
            

            ShowProduct.Click += (s, e) => apiData.GetProduct(hiddenProductId.Text);

        }



    }
}
