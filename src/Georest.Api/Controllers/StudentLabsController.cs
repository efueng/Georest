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
    public class StudentLabsController : ControllerBase
    {
        private IMapper Mapper { get; set; }
        private IStudentLabService LabService { get; set; }
        private string IsOverridden { get; set; } = "false";
        private string LabKey { get; set; } = "Lab1Key";

        public StudentLabsController(IStudentLabService labService, IMapper mapper)
        {
            Mapper = mapper;
            LabService = labService;
        }

        // GET: api/Lab
        //[HttpGet]
        //[Produces(typeof(ICollection<InstructorLabViewModel>))]
        //public IActionResult GetInstructorById()
        //{
        //    return Created(nameof(GetInstructorById), StudentLabService.FetchAlLabs().ToList());
        //}

        // GET: api/Labs/5
        [HttpGet("{id}", Name = "GetLabById")]
        public async Task<IActionResult> GetLabById(int id)
        {
            StudentLab fetchedLab = await LabService.GetById(id).ConfigureAwait(false);
            if (fetchedLab == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<InstructorLabViewModel>(fetchedLab));
        }

        // POST: api/Labs
        [HttpPost]
        public async Task<IActionResult> AddLab(StudentLabViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            StudentLab createdLab = await LabService.AddLab(Mapper.Map<StudentLab>(viewModel)).ConfigureAwait(false);
            return CreatedAtAction(nameof(AddLab), new {id = createdLab.Id}, Mapper.Map<StudentLabViewModel>(createdLab));
        }

        // PUT: api/Labs/5
        [HttpPut("{id}", Name = "UpdateLab")]
        public async Task<IActionResult> UpdateLab(int id, InstructorLabInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            StudentLab fetchedLab = await LabService.GetById(id).ConfigureAwait(false);
            if (fetchedLab == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedLab);
            await LabService.UpdateLab(fetchedLab).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}", Name = "DeleteLab")]
        public async Task<IActionResult> DeleteLab(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A Lab id must be specified");
            }

            if (await LabService.DeleteLab(id).ConfigureAwait(false))
            {
                return Ok();
            }

            return NotFound();
        }

        //public ActionResult LabEditorView(string id)
        //{
        //    LabKey = id;
        //    InstructorLabViewModel model;
        //    //model = TryImportLabViewModel();
        //    User user = HttpContext.Session.Get<User>("User");
        //    //User user = Session["User"] as User;
        //    //user.CurrentLabState = model;
        //    //return View(user);
        //    return null;
        //}
    }
}
