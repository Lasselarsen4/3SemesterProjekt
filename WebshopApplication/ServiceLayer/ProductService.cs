using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration; // Add this namespace for IConfiguration
using ModelAPI;

namespace WebshopApplication.ServiceLayer
{
    public class ProductService : IProductService
    {
        private readonly IServiceConnection _productServiceConnection;
        public string BaseUrl { get; set; }

        public ProductService(IConfiguration configuration)
        {
            BaseUrl = configuration["BaseUrl"];
            _productServiceConnection = new ServiceConnection(BaseUrl);
        }

        public async Task<List<Product>?> GetProducts(string? sortParam, int id = -1)
        {
            List<Product>? listFromService = null;

            _productServiceConnection.UseUrl = _productServiceConnection.BaseUrl;
            _productServiceConnection.UseUrl += "product";
            bool hasValidId = (id > 0);
            if (hasValidId)
            {
                _productServiceConnection.UseUrl += "/" + id.ToString();
            }
            else
            {
                bool hasSortParam = ((sortParam != null) && (!sortParam.ToLower().Equals("none")));
                if (hasSortParam)
                {
                    _productServiceConnection.UseUrl += "?sortBy=" + sortParam;
                }
            }

            if (_productServiceConnection != null)
            {
                try
                {
                    var serviceResponse = await _productServiceConnection.CallServiceGet();
                    if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                    {
                        var content = await serviceResponse.Content.ReadAsStringAsync();
                        if (hasValidId)
                        {
                            Product? foundProduct = JsonConvert.DeserializeObject<Product>(content);
                            if (foundProduct != null)
                            {
                                listFromService = new List<Product>() { foundProduct };
                            }
                        }
                        else
                        {
                            listFromService = JsonConvert.DeserializeObject<List<Product>>(content);
                        }
                    }
                    else
                    {
                        if (serviceResponse != null && serviceResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            listFromService = new List<Product>();
                        }
                        else
                        {
                            listFromService = null;
                        }
                    }
                }
                catch
                {
                    listFromService = null;
                }
            }

            return listFromService;
        }

        public async Task<bool> SaveProduct(Product product)
        {
            bool savedOk = false;

            _productServiceConnection.UseUrl = _productServiceConnection.BaseUrl;
            _productServiceConnection.UseUrl += "product";

            if (_productServiceConnection != null)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(product);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var serviceResponse = await _productServiceConnection.CallServicePost(content);
                    if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                    {
                        savedOk = true;
                    }
                }
                catch
                {
                    savedOk = false;
                }
            }

            return savedOk;
        }


        public async Task<bool> UpdateProduct(Product product)
        {
            bool updatedOk = false;

            _productServiceConnection.UseUrl = _productServiceConnection.BaseUrl;
            _productServiceConnection.UseUrl += "product";
            _productServiceConnection.UseUrl += "/" + product.ProductId;

            if (_productServiceConnection != null)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(product);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var serviceResponse = await _productServiceConnection.CallServicePut(content);
                    bool wasResponse = (serviceResponse != null);
                    if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                    {
                        updatedOk = true;
                    }
                }
                catch
                {
                    updatedOk = false;
                }
            }

            return updatedOk;
        }

        public async Task<bool> DeleteProduct(int delId)
        {
            bool deletedOk = false;

            _productServiceConnection.UseUrl = _productServiceConnection.BaseUrl;
            _productServiceConnection.UseUrl += "product/" + delId;

            if (_productServiceConnection != null)
            {
                try
                {
                    var serviceResponse = await _productServiceConnection.CallServiceDelete();
                    bool wasResponse = (serviceResponse != null);
                    if (serviceResponse != null && serviceResponse.IsSuccessStatusCode)
                    {
                        deletedOk = true;
                    }
                }
                catch
                {
                    deletedOk = false;
                }
            }

            return deletedOk;
        }
    }
}
