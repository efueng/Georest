using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Georest.Api.ViewModels;
using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Georest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private IMapper Mapper { get; set; }
        private IInstructorService InstructorService { get; set; }

        public InstructorsController(IInstructorService instructorService, IMapper mapper)
        {
            Mapper = mapper;
            InstructorService = instructorService;
        }

        // GET api/Instructors
        [HttpGet]
        public async Task<ActionResult<ICollection<InstructorViewModel>>> GetAllInstructors()
        {
            var instructors = await InstructorService.GetAllInstructors();
            return Ok(instructors.Select(x => Mapper.Map<InstructorViewModel>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetInstructorById(int id)
        {
            Instructor fetchedInstructor = await InstructorService.GetById(id).ConfigureAwait(false);
            if (fetchedInstructor == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<InstructorViewModel>(fetchedInstructor));
        }

        // POST: api/Instructors
        [HttpPost]
        public async Task<ActionResult<InstructorViewModel>> AddInstructor(InstructorViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            Instructor createdInstructor = await InstructorService.AddInstructor(Mapper.Map<Instructor>(viewModel)).ConfigureAwait(false);
            return CreatedAtAction(nameof(AddInstructor), new { id = createdInstructor.Id }, Mapper.Map<InstructorViewModel>(createdInstructor));
        }

        // PUT: api/Instructors/5
        [HttpPut]
        public async Task<ActionResult> UpdateInstructor(int id, InstructorInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            Instructor fetchedInstructor = await InstructorService.GetById(id).ConfigureAwait(false);
            if (fetchedInstructor == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedInstructor);
            await InstructorService.UpdateInstructor(fetchedInstructor).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE: api/Instructors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInstructor(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A Instructor id must be specified");
            }

            if (await InstructorService.DeleteInstructor(id).ConfigureAwait(false))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
