using Bogus;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using utcAltkomDevices.IServices;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.FakeServices
{
    public class EntityOptions
    {
        public int Quantity { get; set; }
    }

    public class FakeEntitiesService<T> : IEntityServices<T> where T : Base 
    {
        protected readonly ICollection<T> list;

        private EntityOptions options;

        public FakeEntitiesService(IOptions<EntityOptions> options, Faker<T> faker)
        {
            this.options = options.Value;

            list = faker.Generate(this.options.Quantity);
        }

        public virtual bool Add(T input)
        {
            list.Add(input);
            return true;
        }

        public virtual ICollection<T> Get()
        {
            return list;
        }

        public virtual T Get(int id)
        {
            return list.SingleOrDefault(p => p.Id == id);
        }

        public virtual T Get(string name)
        {
            return list.FirstOrDefault(p => p.Name == name);
        }

        public virtual T Remove(int id)
        {
            T customer = Get(id);
            list.Remove(customer);
            return customer;
        }

        public virtual bool Update(T input)
        {
            bool result;
            T t = Remove(input.Id);
            result = t != null;
            result &= Add(t);
            return result;
        }
    }
}
