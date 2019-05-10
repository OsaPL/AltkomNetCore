using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using utcAltkomDevices.IServices;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.WebService.Controllers
{
    public class EntityController<T> : ControllerBase where T : Base
    {
        protected readonly IEntityServices<T> service;

        protected ILogger logger;

        public EntityController(ILogger logger, IEntityServices<T> incoming)
        {
            service = incoming;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //We can extract the data that is being put into Claim on authentication
            var name = this.User.FindFirst(ClaimTypes.Name).Value;
            var welcome = this.User.FindFirst("Hey").Value;


            logger.LogWarning("Get all called");
            var input = service.Get();
            return Ok(input);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var input = service.Get(id);

            if (input == null)
            {
                return NotFound();
            }
            return Ok(input);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var input = service.Get(name);

            if (input == null)
            {
                return NotFound();
            }
            return Ok(input);
        }

        [HttpPost]
        public IActionResult Post(T input)
        {
            service.Add(input);
            return CreatedAtRoute(new { input.Id }, input);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(service.Remove(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, T input)
        {
            if (input.Id != id)
            {
                BadRequest();
            }
            return Ok(service.Update(input));
        }
    }
}
