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
    public class InstructorLabsController : ControllerBase
    {
        private IMapper Mapper { get; set; }
        private IInstructorLabService LabService { get; set; }

        public InstructorLabsController(IInstructorLabService labService, IMapper mapper)
        {
            Mapper = mapper;
            LabService = labService;
        }

        // GET api/InstructorLabs
        [HttpGet]
        public async Task<ActionResult<ICollection<InstructorLabViewModel>>> GetAllLabs()
        {
            ICollection<InstructorLab> exercises = await LabService.GetAllLabs().ConfigureAwait(false);
            return Ok(exercises.Select(x => Mapper.Map<InstructorLabViewModel>(x)));
        }

        // GET: api/InstructorLabs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorLabViewModel>> GetLabById(int id)
        {
            InstructorLab fetchedLab = await LabService.GetById(id).ConfigureAwait(false);
            if (fetchedLab == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<InstructorLabViewModel>(fetchedLab));
        }

        // POST: api/InstructorLabs
        [HttpPost]
        public async Task<ActionResult<InstructorLabViewModel>> AddLab(InstructorLabInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            InstructorLab createdLab = await LabService.AddLab(Mapper.Map<InstructorLab>(viewModel)).ConfigureAwait(false);
            //return CreatedAtAction(nameof(AddLab), new { id = createdLab.Id }, Mapper.Map<InstructorLabViewModel>(createdLab));
            return Ok(createdLab);
        }

        // PUT: api/InstructorLabs/5
        [HttpPut]
        public async Task<ActionResult<InstructorLabViewModel>> UpdateLab(int id, InstructorLabInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            InstructorLab fetchedLab = await LabService.GetById(id).ConfigureAwait(false);
            if (fetchedLab == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedLab);
            await LabService.UpdateLab(fetchedLab).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE: api/InstructorLabs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLab(int id)
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
    }
}