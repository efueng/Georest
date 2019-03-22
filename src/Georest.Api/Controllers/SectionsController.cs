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
    public class SectionsController : ControllerBase
    {
        private IMapper Mapper { get; set; }
        private ISectionService SectionService { get; set; }

        public SectionsController(ISectionService sectionService, IMapper mapper)
        {
            Mapper = mapper;
            SectionService = sectionService;
        }

        // GET api/Sections
        [HttpGet]
        public async Task<ActionResult<ICollection<SectionViewModel>>> GetAllSections()
        {
            var sections = await SectionService.GetAllSections();

            return Ok(Mapper.Map<ICollection<SectionViewModel>>(sections));
        }

        // GET: api/Sections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SectionViewModel>> GetSectionById(int id)
        {
            Section fetchedSection = await SectionService.GetById(id).ConfigureAwait(false);
            if (fetchedSection == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<SectionViewModel>(fetchedSection));
        }

        // POST: api/Sections
        [HttpPost]
        public async Task<ActionResult<SectionViewModel>> AddSection(SectionInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            Section createdSection = await SectionService.AddSection(Mapper.Map<Section>(viewModel)).ConfigureAwait(false);
            return CreatedAtAction(nameof(AddSection), new { id = createdSection.Id }, Mapper.Map<SectionViewModel>(createdSection));
        }

        // PUT: api/Sections/5
        [HttpPut("{id}")]
        public async Task<ActionResult<SectionViewModel>> UpdateSection(int id, SectionInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            Section fetchedSection = await SectionService.GetById(id).ConfigureAwait(false);
            if (fetchedSection == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedSection);
            await SectionService.UpdateSection(fetchedSection).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE: api/Sections/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSection(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A Section id must be specified");
            }

            if (await SectionService.DeleteSection(id).ConfigureAwait(false))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
