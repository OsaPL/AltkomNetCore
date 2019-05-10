using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using utcAltkomDevices.IServices;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.DbServices
{
    public class DbCustomerServices : IEntityServices<Customer>
    {
        private readonly UtcContext context;

        public DbCustomerServices(UtcContext context)
        {
            this.context = context;
        }

        public bool Add(Customer input)
        {
            Trace.WriteLine(context.Entry(input).State);
            // Add object to contextTracker
            context.Customers.Add(input);
            Trace.WriteLine(context.Entry(input).State);

            // Send changes to Db
            context.SaveChanges();
            Trace.WriteLine(context.Entry(input).State);

            return true;
        }

        public ICollection<Customer> Get()
        {
            //TODO: No need to track, so to optimize you can mark it as untracked by contextTracker

            return context.Customers.ToList();
        }

        public Customer Get(int id)
        {
            return context.Customers.Find(id);

            // For nested objects described by keys
            //return context.Customers
            //    .Include(prop => prop.Devices)
            //    .ThenInclude(d => d.Firmware)
            //    .ThenInclude(f => f.Version)
            //    .SignleOrDefault(c => c.Id);

        }

        public Customer Get(string name)
        {
            throw new NotImplementedException();
        }

        public Customer Remove(int id)
        {
            //This can be optimized, no need for Get
            //var customer = Get(id);
            //context.Customers.Remove(customer);

            // Create a dummy and make it tracked
            Customer temp = new Customer() { Id = id };
            context.Attach(temp);

            context.Customers.Remove(temp);
            //.Remove(temp) just does the same as:
            //context.Entry(temp).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            context.SaveChanges();

            return temp;
        }

        public bool Update(Customer input)
        {
            context.Customers.Update(input);
            context.SaveChanges();
            return true;
        }
    }
}
