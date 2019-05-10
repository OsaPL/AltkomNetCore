using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using utcAltkomDevices.IServices;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.RestApiClientServices
{
    public class RestApiCustomerService : IEntityServicesAsync<Customer>
    {
        private HttpClient client;

        public RestApiCustomerService(HttpClient client)
        {
            this.client = client;
        }

        public Task<bool> AddAsync(Customer input)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Customer>> GetAsync()
        {
            HttpResponseMessage response = await client.GetAsync("api/customers");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                ICollection<Customer> customers = JsonConvert.DeserializeObject<ICollection<Customer>>(content);
                return customers;
            }
            return null;
        }

        public Task<Customer> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Customer input)
        {
            throw new NotImplementedException();
        }
    }
}
