using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.HubReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.WriteLine("Hub Receiver start.");

            ConnectTestAsync();

            Console.WriteLine("Waiting for key press...");
            Console.ReadKey();
        }

        private static async Task ConnectTestAsync()
        {
            Random rand = new Random();
            int clientNr = rand.Next();
            Console.WriteLine($"Im clientNr: {clientNr}");

            string url = "http://localhost:5000/hubs/customers";
            //string url = "http://b0b353d8.ngrok.io/hubs/customers";


            HubConnection hubConnection = new HubConnectionBuilder()
                .WithUrl(url, p => p.Headers.Add("Authorization", "Basic YWRtaW46MTIzNA=="))
                .Build();

            Console.WriteLine("Connecting...");
            await hubConnection.StartAsync();
            Console.WriteLine("Connected.");

            hubConnection.On<Customer>("AddedCustomer", (c =>
            {
                Console.WriteLine(c.ToString());
                hubConnection.SendAsync("OkGotIt", clientNr);
            }));

            hubConnection.On<string>("Cool", (c =>
                Console.WriteLine("Server returned: " + c)));
        }
    }
}
