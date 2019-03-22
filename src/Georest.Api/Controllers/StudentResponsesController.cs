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
    public class StudentResponsesController : ControllerBase
    {
        private IMapper Mapper { get; set; }
        private IStudentResponseService ResponseService { get; set; }

        public StudentResponsesController(IStudentResponseService responseService, IMapper mapper)
        {
            Mapper = mapper;
            ResponseService = responseService;
        }

        // GET: api/StudentResponses
        [HttpGet]
        public async Task<ActionResult<StudentResponseViewModel>> GetAllStudentResponses()
        {
            ICollection<StudentResponse> responses = await ResponseService.GetAllResponses().ConfigureAwait(false);

            return Ok(responses.Select(x => Mapper.Map<StudentResponseViewModel>(x)));
        }

        // GET: api/StudentResponses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResponseViewModel>> GetStudentResponseById(int id)
        {
            StudentResponse fetchedResponse = await ResponseService.GetById(id).ConfigureAwait(false);
            if (fetchedResponse == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<StudentResponseViewModel>(fetchedResponse));
        }

        // POST: api/StudentResponses
        [HttpPost]
        public async Task<ActionResult<StudentResponseViewModel>> AddStudentResponse(StudentResponseInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            StudentResponse createdResponse = await ResponseService.AddResponse(Mapper.Map<StudentResponse>(viewModel)).ConfigureAwait(false);
            return CreatedAtAction(nameof(AddStudentResponse), new { id = createdResponse.Id }, Mapper.Map<StudentResponseViewModel>(createdResponse));
        }

        // PUT: api/StudentResponses/5
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentResponseViewModel>> UpdateStudentResponse(int id, StudentResponseInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            StudentResponse fetchedResponse = await ResponseService.GetById(id).ConfigureAwait(false);
            if (fetchedResponse == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedResponse);
            await ResponseService.UpdateResponse(fetchedResponse).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE: api/StudentResponses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentResponse(int id)
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