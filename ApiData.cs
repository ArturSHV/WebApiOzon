using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Windows;

namespace WPF_API_OZON
{
    public class ApiData
    {
        public string ApiKey { get; set; }
        public string ClientId { get; set; }

        /// <summary>
        /// для выгрузки всех товаров
        /// </summary>
        public ObservableCollection<Product> product { get; set; }

        /// <summary>
        /// для выгрузки определенного товара
        /// </summary>
        public ObservableCollection<ProductInfo> productInfo { get; set; }

        public ApiData()
        {
            product = new ObservableCollection<Product>();

            productInfo = new ObservableCollection<ProductInfo>();

        }


        /// <summary>
        /// посмотреть информацию о продукте
        /// </summary>
        public void GetProduct(string product_id)
        {
            string url = @"https://api-seller.ozon.ru/v2/product/info";

            RestRequest request = Request(url);

            var json = JsonConvert.SerializeObject(product.FirstOrDefault(x => x.product_id == product_id));

            request.AddBody(json);

            dynamic data = GetData(url, request);

            try
            {
                productInfo.Add(new ProductInfo()
                {
                    id = data.result.id,
                    name = data.result.name,
                    offer_id = data.result.offer_id,
                    price = data.result.price,
                    recommended_price = data.result.recommended_price,
                    images = data.result.images[0]
                });

                ProductInfoPage productInfoPage = new ProductInfoPage(this);
                productInfoPage.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            


        }


        /// <summary>
        /// Возвращает request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public RestRequest Request(string url)
        {
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddHeader("Host", "api-seller.ozon.ru");
            request.AddHeader("Client-Id", ClientId); 
            request.AddHeader("Api-Key", ApiKey); 
            request.AddHeader("Content-Type", "application/json");

            return request;
        }

        
        /// <summary>
        /// получение списка добавленных продуктов
        /// </summary>
        public async void GetList(string ApiKey, string ClientId)
        {
            this.ApiKey = ApiKey;
            this.ClientId = ClientId;

            await Task.Run(() =>
            {
                string url = @"https://api-seller.ozon.ru/v2/product/list";

                RestRequest request = Request(url);

                dynamic data = GetData(url, request);

                try
                {
                    var total = data.result.total;
                    App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        product.Clear();

                        for (int i = 0; i < Convert.ToInt32(total); i++)
                        {
                            product.Add(new Product { product_id = data.result.items[i].product_id, offer_id = data.result.items[i].offer_id });
                        }

                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
                
            });

        }


        /// <summary>
        /// Возвращает десериализованный json с сервера
        /// </summary>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public dynamic GetData(string url, RestRequest request)
        {
            RestClient client = new RestClient(url);

            var response = client.Execute(request);

            string stream = response.Content;

            return JsonConvert.DeserializeObject(stream);
            
        }


    }


    /// <summary>
    /// класс список продуктов
    /// </summary>
    public class Product
    {
        public string product_id { get; set; }
        public string offer_id { get; set; }
        public string sku { get; set; }
    }


    /// <summary>
    /// класс информация о продукте
    /// </summary>
    public class ProductInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string offer_id { get; set; }
        public string price { get; set; }
        public string recommended_price { get; set; }
        public string images { get; set; }

    }

    
}
