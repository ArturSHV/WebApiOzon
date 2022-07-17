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
using System.Windows.Shapes;

namespace WPF_API_OZON
{
    /// <summary>
    /// Логика взаимодействия для ProductInfoPage.xaml
    /// </summary>
    public partial class ProductInfoPage : Window
    {
        
        public ProductInfoPage(ApiData apiData)
        {
            InitializeComponent();

            DataContext = apiData;

        }
    }
}
