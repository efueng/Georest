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
    public class StudentsController : ControllerBase
    {
        private IMapper Mapper { get; set; }
        private IStudentService StudentService { get; set; }

        public StudentsController(IStudentService studentService, IMapper mapper)
        {
            Mapper = mapper;
            StudentService = studentService;
        }

        // GET api/Students
        [HttpGet]
        public async Task<ActionResult<ICollection<StudentViewModel>>> GetAllStudents()
        {
            var students = await StudentService.GetAllStudents();
            //return Ok(students.Select(x => Mapper.Map<StudentViewModel>(x)));
            return Ok(Mapper.Map<ICollection<StudentViewModel>>(students));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentViewModel>> GetStudentById(int id)
        {
            Student fetchedStudent = await StudentService.GetById(id).ConfigureAwait(false);
            if (fetchedStudent == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<StudentViewModel>(fetchedStudent));
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<StudentViewModel>> AddStudent(StudentInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            Student createdStudent = await StudentService.AddStudent(Mapper.Map<Student>(viewModel)).ConfigureAwait(false);
            return Ok(createdStudent);
        }

        // PUT: api/Students/5
        [HttpPut]
        public async Task<ActionResult<StudentViewModel>> UpdateStudent(int id, StudentInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            Student fetchedStudent = await StudentService.GetById(id).ConfigureAwait(false);
            if (fetchedStudent == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedStudent);
            await StudentService.UpdateStudent(fetchedStudent).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A Student id must be specified");
            }

            if (await StudentService.DeleteStudent(id).ConfigureAwait(false))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
