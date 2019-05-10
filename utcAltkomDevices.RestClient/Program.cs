using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using utcAltkomDevices.FakeServices;
using utcAltkomDevices.IServices;
using utcAltkomDevices.Models;
using utcAltkomDevices.RestApiClientServices;

namespace utcAltkomDevices.RestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await GetCustomers(args[0]);
            await GetDevices(args[0]);
        }

        private static async Task GetDevices(string address)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(address);

                HttpResponseMessage response = await client.GetAsync("api/devices");

                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<Device> devices = await response.Content.ReadAsAsync<IEnumerable<Device>>();

                    DeviceFaker deviceFaker = new DeviceFaker();

                    response = await client.PostAsJsonAsync<Device>("api/devices", deviceFaker.Generate());
                    string responsecontent = await response.Content.ReadAsStringAsync();
                }
            }
        }

        private static async Task GetCustomers(string address)
        {
            IEntityServicesAsync<Customer> service = new RestApiCustomerService(new HttpClient() { BaseAddress = new Uri(address)});

            var customer = await service.GetAsync();
        }
    }
}
