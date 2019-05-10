using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using utcAltkomDevices.IServices;
using utcAltkomDevices.Models;
using utcAltkomDevices.Models.Searchers;

namespace utcAltkomDevices.WebService.Controllers
{
    //Adds controller wide authorization, with additional role check
    [Authorize(Roles = "AdminRole")]
    [Route("api/[controller]")]
    [ApiController] //Or just add FromBody in: public IActionResult Post([FromBody] Device device)
    public class CustomersController : EntityController<Customer>
    {
        public CustomersController(ILogger<CustomersController> logger, IEntityServices<Customer> incoming) : base(logger,incoming)
        {
        }
        // "~/" Starts new route path
        [Route("~/api/customers/{customerid}/devices")]
        //[AllowAnonymous] to make it accessible without authorization
        [HttpGet]
        public IEnumerable<Device> GetDevices(int customerid)
        {
            var temp = this.Request.Headers;
            throw new NotImplementedException();
        }
    }
}
