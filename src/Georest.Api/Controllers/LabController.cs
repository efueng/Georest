using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Georest.Api.ViewModels;
using Georest.Domain.Models;
using Georest.Domain.Services;
using Georest.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Georest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabController : ControllerBase
    {
        private IMapper Mapper { get; set; }
        private ILabService LabService { get; set; }
        private string IsOverridden { get; set; } = "false";
        private string LabKey { get; set; } = "Lab1Key";

        public LabController(ILabService labService, IMapper mapper)
        {
            Mapper = mapper;
            LabService = labService;
        }

        // GET: api/Lab
        [HttpGet]
        [Produces(typeof(ICollection<LabViewModel>))]
        public IActionResult Get()
        {
            return Created(nameof(Get), LabService.FetchAlLabs().ToList());
        }

        // GET: api/Lab/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var fetchedLab = LabService.GetById(id);
            if (fetchedLab == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<LabViewModel>(fetchedLab));
        }

        // POST: api/Lab
        [HttpPost]
        public IActionResult Post(LabViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            var createdLab = LabService.AddLab(Mapper.Map<Lab>(viewModel));
            return CreatedAtAction(nameof(Post), new {id = createdLab.Id}, Mapper.Map<LabViewModel>(createdLab));
        }

        // PUT: api/Lab/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, LabInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var fetchedLab = LabService.GetById(id);
            if (fetchedLab == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedLab);
            LabService.UpdateLab(fetchedLab);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A Lab id must be specified");
            }

            if (LabService.DeleteLab(id))
            {
                return Ok();
            }

            return NotFound();
        }

        public ActionResult LabEditorView(string id)
        {
            LabKey = id;
            LabViewModel model;
            //model = TryImportLabViewModel();
            User user = HttpContext.Session.Get<User>("User");
            //User user = Session["User"] as User;
            //user.CurrentLabState = model;
            //return View(user);
            return null;
        }
    }
}
