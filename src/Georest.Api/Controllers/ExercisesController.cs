﻿using System;
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
            var exercises = await ExerciseService.GetAllExercises();
            
            return Ok(Mapper.Map<ICollection<ExerciseViewModel>>(exercises));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseViewModel>> GetExerciseById(int id)
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
        public async Task<ActionResult<ExerciseViewModel>> AddExercise(ExerciseInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            Exercise createdExercise = await ExerciseService.AddExercise(Mapper.Map<Exercise>(viewModel)).ConfigureAwait(false);
            return Ok(createdExercise);
        }

        // PUT: api/Exercises/5
        [HttpPut]
        public async Task<ActionResult<ExerciseViewModel>> UpdateExercise(int id, ExerciseInputViewModel viewModel)
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
