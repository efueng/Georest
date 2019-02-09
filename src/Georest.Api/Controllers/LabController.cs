using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Georest.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Georest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabController : ControllerBase
    {
        private string IsOverridden { get; set; } = "false";
        private string LabKey { get; set; } = "Lab1Key";

        // GET: api/Lab
        [HttpGet]
        public IActionResult Get()
        {
            //return Created(nameof(Get), );
            return Ok();
        }

        // GET: api/Lab/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Lab
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Lab/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
