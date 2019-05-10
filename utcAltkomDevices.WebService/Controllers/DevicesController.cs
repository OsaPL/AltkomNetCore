using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using utcAltkomDevices.IServices;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.WebService.Controllers
{
    //[Route("api/devices")] or even better:
    [Route("api/[controller]")]
    [ApiController] //Or just add FromBody in: public IActionResult Post([FromBody] Device device)
    public class DevicesController : EntityController<Device>
    {
        public DevicesController(ILogger<DevicesController> logger, IEntityServices<Device> incoming) : base(logger,incoming)
        {

        }
    }
}
