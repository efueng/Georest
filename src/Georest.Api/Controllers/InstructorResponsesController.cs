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
    public class InstructorResponsesController : ControllerBase
    {
        private IMapper Mapper { get; set; }
        private IInstructorResponseService ResponseService { get; set; }

        public InstructorResponsesController(IInstructorResponseService responseService, IMapper mapper)
        {
            Mapper = mapper;
            ResponseService = responseService;
        }

        // GET: api/InstructorResponses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorResponseViewModel>> GetInstructorResponseById(int id)
        {
            InstructorResponse fetchedResponse = await ResponseService.GetById(id).ConfigureAwait(false);
            if (fetchedResponse == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<InstructorResponseViewModel>(fetchedResponse));
        }

        // GET: api/InstructorResponses
        [HttpGet]
        public async Task<ActionResult<InstructorResponseViewModel>> GetAllInstructorResponses()
        {
            ICollection<InstructorResponse> responses = await ResponseService.GetAllResponses().ConfigureAwait(false);

            return Ok(Mapper.Map<InstructorResponseViewModel>(responses));
        }

        // POST: api/InstructorResponses
        [HttpPost]
        public async Task<ActionResult<InstructorResponseViewModel>> AddInstructorResponse(InstructorResponseInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            InstructorResponse createdResponse = await ResponseService.AddResponse(Mapper.Map<InstructorResponse>(viewModel)).ConfigureAwait(false);
            return CreatedAtAction(nameof(AddInstructorResponse), new { id = createdResponse.Id }, Mapper.Map<InstructorResponseViewModel>(createdResponse));
        }

        // PUT: api/InstructorResponses/5
        [HttpPut("{id}")]
        public async Task<ActionResult<InstructorResponseViewModel>> UpdateInstructorResponse(int id, InstructorResponseInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            InstructorResponse fetchedResponse = await ResponseService.GetById(id).ConfigureAwait(false);
            if (fetchedResponse == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedResponse);
            await ResponseService.UpdateResponse(fetchedResponse).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE: api/InstructorResponses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInstructorResponse(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A Response id must be specified");
            }

            if (await ResponseService.DeleteResponse(id).ConfigureAwait(false))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}