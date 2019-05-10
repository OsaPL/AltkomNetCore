using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using utcAltkomDevices.FakeServices.Fakers;
using utcAltkomDevices.Models;
using utcAltkomDevices.Models.Searchers;

namespace utcAltkomDevices.FakeServices
{
    public class FakeCustomerService : FakeEntitiesService<Customer>
    {
        public FakeCustomerService(IOptions<EntityOptions> options,CustomerFaker faker) : base(options,faker)
        {

        }
        public IQueryable<Customer> Get(CustomerSearchParams parameters)
        {
            IQueryable<Customer> results = list.AsQueryable(); 

            if (parameters.From.HasValue)
            {
                results = results.Where(x => x.Birth >= parameters.From);
            }
            if (parameters.To.HasValue)
            {
                results = results.Where(x => x.Birth <= parameters.To);
            }

            return results;
        }
    }
}
