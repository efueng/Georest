using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Georest.Api.ViewModels;
using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Georest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService UserService { get; }
        private IMapper Mapper { get; }

        public UserController(IUserService userService, IMapper mapper)
        {
            UserService = userService;
            Mapper = mapper;
        }

        // GET: api/User
        [HttpGet]
        [Produces(typeof(ICollection<UserViewModel>))]
        public IActionResult Get()
        {
            return Ok(UserService.FetchAll().Select(x => Mapper.Map<UserViewModel>(x)));
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        [Produces(typeof(UserViewModel))]
        public IActionResult Get(int id)
        {
            var fetchedUser = UserService.GetById(id);
            if (fetchedUser == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UserViewModel>(fetchedUser));
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Post(UserInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            var createdUser = UserService.AddUser(Mapper.Map<User>(viewModel));

            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, Mapper.Map<UserViewModel>(createdUser));
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, UserInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var fetchedUser = UserService.GetById(id);
            if (fetchedUser == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedUser);
            UserService.UpdateUser(fetchedUser);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A User id must be specified");
            }

            if (UserService.DeleteUser(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
