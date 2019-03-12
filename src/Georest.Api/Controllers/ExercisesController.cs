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
    public class ExercisesController : ControllerBase
    {
        private IMapper Mapper { get; set; }
        private IExerciseService ExerciseService { get; set; }

        public ExercisesController(IExerciseService exerciseService, IMapper mapper)
        {
            Mapper = mapper;
            ExerciseService = exerciseService;
        }

        // GET api/Exercises
        [HttpGet]
        public async Task<ActionResult<ICollection<ExerciseViewModel>>> GetAllExercises()
        {
            ICollection<Exercise> exercises = await ExerciseService.GetAllExercises().ConfigureAwait(false);
            //return Ok(exercises.Select(x => Mapper.Map<ExerciseViewModel>(x)));
            return Ok(Mapper.Map<ICollection<ExerciseViewModel>>(exercises));
        }

        // GET: api/Exercises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exercise>> GetExerciseById(int id)
        {
            Exercise fetchedExercise = await ExerciseService.GetById(id).ConfigureAwait(false);
            if (fetchedExercise == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ExerciseViewModel>(fetchedExercise));
        }

        // POST: api/Exercises
        [HttpPost]
        public async Task<ActionResult<Exercise>> AddExercise(ExerciseInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            Exercise createdExercise = await ExerciseService.AddExercise(Mapper.Map<Exercise>(viewModel)).ConfigureAwait(false);
            return CreatedAtAction(nameof(AddExercise), new { id = createdExercise.Id }, createdExercise);
        }

        // PUT: api/Exercises/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Exercise>> UpdateExercise(int id, ExerciseInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            Exercise fetchedExercise = await ExerciseService.GetById(id).ConfigureAwait(false);
            if (fetchedExercise == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedExercise);
            await ExerciseService.UpdateExercise(fetchedExercise).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE: api/Exercises/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExercise(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A Exercise id must be specified");
            }

            if (await ExerciseService.DeleteExercise(id).ConfigureAwait(false))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}