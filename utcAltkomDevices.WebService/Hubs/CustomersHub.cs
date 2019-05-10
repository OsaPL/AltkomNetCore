using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.WebService.Hubs
{
    [Authorize]
    public class CustomersHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, "CoolGuys");
            return base.OnConnectedAsync();
        }

        public async Task Added(Customer customer)
        {
            //Notify on add
            this.Clients.All.SendAsync("AddedCustomer", customer);
        }
        public async Task OkGotIt(int clientNr)
        {
            var name = Context.User.FindFirst(ClaimTypes.Name).Value;
            Console.WriteLine($"Client {clientNr} logged in as {name}, said he got the customer!");
            Console.WriteLine("Sending response to all CoolGuys");
            Clients.Group("CoolGuys").SendAsync("Cool", "Cool, cool.");
        }
    }
}