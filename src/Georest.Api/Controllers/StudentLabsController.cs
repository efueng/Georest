﻿using System;
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

        public StudentLabsController(IStudentLabService labService, IMapper mapper)
        {
            Mapper = mapper;
            LabService = labService;
        }

        // GET: api/StudentLabs/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetLabById(int id)
        {
            StudentLab fetchedLab = await LabService.GetById(id).ConfigureAwait(false);
            if (fetchedLab == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<InstructorLabViewModel>(fetchedLab));
        }

        // POST: api/StudentLabs
        [HttpPost]
        public async Task<ActionResult> AddLab(StudentLabViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            StudentLab createdLab = await LabService.AddLab(Mapper.Map<StudentLab>(viewModel)).ConfigureAwait(false);
            return CreatedAtAction(nameof(AddLab), new {id = createdLab.Id}, Mapper.Map<StudentLabViewModel>(createdLab));
        }

        // PUT: api/StudentLabs/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLab(int id, InstructorLabInputViewModel viewModel)
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

        // DELETE: api/StudentLabs/5
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
