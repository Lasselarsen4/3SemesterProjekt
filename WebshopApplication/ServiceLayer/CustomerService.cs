using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebshopApplication.Models;


namespace WebshopApplication.ServiceLayer
{
    public class CustomerService : ICustomerService
    {
        private readonly IServiceConnection _serviceConnection;

        public CustomerService(IConfiguration configuration)
        {
            var baseUrl = configuration["ServiceUrlToUse"];
            _serviceConnection = new ServiceConnection(baseUrl);
        }

        public async Task<List<Customer>> GetCustomers(string sortParam, int id = -1)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/customer";
            if (id > 0)
            {
                _serviceConnection.UseUrl += $"/{id}";
            }
            else if (!string.IsNullOrEmpty(sortParam) && sortParam.ToLower() != "none")
            {
                _serviceConnection.UseUrl += $"?sortBy={sortParam}";
            }

            var response = await _serviceConnection.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (id > 0)
                {
                    var singleCustomer = JsonConvert.DeserializeObject<Customer>(content);
                    return new List<Customer> { singleCustomer };
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<Customer>>(content);
                }
            }

            return new List<Customer>();
        }

        public async Task<bool> SaveCustomer(Customer customer)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/customer";

            var customerForInsert = new
            {
                customer.FirstName,
                customer.LastName,
                customer.Email,
                customer.Phone,
                customer.StreetName,
                customer.HouseNumber,
                customer.ZipCode
            };

            var json = JsonConvert.SerializeObject(customerForInsert);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _serviceConnection.CallServicePost(content);
            return response != null && response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/customer/{customer.CustomerId}";

            var customerForUpdate = new
            {
                customer.CustomerId,
                customer.FirstName,
                customer.LastName,
                customer.Email,
                customer.Phone,
                customer.StreetName,
                customer.HouseNumber,
                customer.ZipCode
            };

            var json = JsonConvert.SerializeObject(customerForUpdate);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _serviceConnection.CallServicePut(content);
            return response != null && response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            _serviceConnection.UseUrl = $"{_serviceConnection.BaseUrl}/customer/{id}";
            var response = await _serviceConnection.CallServiceDelete();
            return response != null && response.IsSuccessStatusCode;
        }
    }
}
