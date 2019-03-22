using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Georest.Api.Controllers;
using Georest.Api.ViewModels;
using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Georest.Api.Models;

namespace Georest.Api.Tests.Controllers
{
    [TestClass]
    public class InstructorsControllerTests
    {
        [AssemblyInitialize]
        public static void ConfigureAutoMapper(TestContext context)
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new AutoMapperProfileConfiguration()));
        }

        private CustomWebApplicationFactory<Startup> Factory { get; set; }

        [TestInitialize]
        public void CreateWebFactory()
        {
            Factory = new CustomWebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task GetAllInstructors_ReturnsInstructors()
        {
            var client = Factory.CreateClient();

            var instructor1 = new Instructor
            {
                FirstName = "Stu",
                LastName = "Steiner"
            };
            var instructor2 = new Instructor
            {
                FirstName = "Tom",
                LastName = "Capaul"
            };

            var service = new Mock<IInstructorService>();
            //service.Setup(x => x.GetAllInstructors())
            //    .Returns(Task.FromResult(new Collection<Instructor> { instructor1, instructor2 }))
            //    .Verifiable();


            var controller = new InstructorsController(service.Object, Mapper.Instance);

            var result = (await controller.GetAllInstructors()).Result as OkObjectResult;

            List<InstructorViewModel> instructors = ((IEnumerable<InstructorViewModel>)result.Value).ToList();

            Assert.AreEqual(2, instructors.Count);
            AssertAreEqual(instructors[0], instructor1);
            AssertAreEqual(instructors[1], instructor2);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task CreateInstructor_RequiresInstructor()
        {
            var service = new Mock<IInstructorService>();
            var controller = new InstructorsController(service.Object, Mapper.Instance);

            var result = (await controller.AddInstructor(null)).Result as BadRequestResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateInstructor_ReturnsCreatedInstructor()
        {
            var instructor = new InstructorInputViewModel
            {
                FirstName = "Stu",
                LastName = "Steiner"
            };

            var service = new Mock<IInstructorService>();
            service.Setup(x => x.AddInstructor(It.Is<Instructor>(g => g.FirstName == instructor.FirstName)))
                .Returns(Task.FromResult(new Instructor
                {
                    Id = 2,
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName
                }))
                .Verifiable();

            var controller = new InstructorsController(service.Object, Mapper.Instance);

            var result = (await controller.AddInstructor(instructor)).Result as OkObjectResult;
            Instructor resultValue = (Instructor) result.Value;

            Assert.IsNotNull(resultValue);
            Assert.AreEqual(2, resultValue.Id);
            Assert.AreEqual("Stu", resultValue.FirstName);
            Assert.AreEqual("Steiner", resultValue.LastName);
            service.VerifyAll();
        }

        //[TestMethod]
        //public async Task UpdateInstructor_RequiresInstructor()
        //{
        //    var service = new Mock<IInstructorService>(MockBehavior.Strict);
        //    var controller = new InstructorsController(service.Object, Mapper.Instance);


        //    BadRequestResult result = await controller.UpdateInstructor(1, null);

        //    Assert.IsNotNull(result);
        //}

        [TestMethod]
        public async Task UpdateInstructor_ReturnsUpdatedInstructor()
        {
            var instructor = new InstructorInputViewModel
            {
                FirstName = "Stu",
                LastName = "Steiner"
            };
            var service = new Mock<IInstructorService>();
            //service.Setup(x => x.GetById(2))
            //    .Returns(Task.FromResult(new Instructor
            //    {
            //        Id = 2,
            //        Name = instructor.Name
            //    }))
            //    .Verifiable();

            var controller = new InstructorsController(service.Object, Mapper.Instance);

            var result = (await controller.UpdateInstructor(2, instructor));

            Assert.IsNotNull(result);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task DeleteInstructor_RequiresPositiveId(int instructorId)
        {
            var service = new Mock<IInstructorService>(MockBehavior.Strict);
            var controller = new InstructorsController(service.Object, Mapper.Instance);

            var result = await controller.DeleteInstructor(instructorId);

            Assert.IsTrue(result is BadRequestObjectResult);
        }

        [TestMethod]
        public async Task DeleteInstructor_ReturnsNotFoundWhenTheInstructorFailsToDelete()
        {
            var service = new Mock<IInstructorService>();
            service.Setup(x => x.DeleteInstructor(2))
                .Returns(Task.FromResult(false))
                .Verifiable();
            var controller = new InstructorsController(service.Object, Mapper.Instance);

            var result = await controller.DeleteInstructor(2);

            Assert.IsTrue(result is NotFoundResult);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteInstructor_ReturnsOkWhenInstructorIsDeleted()
        {
            var service = new Mock<IInstructorService>();
            service.Setup(x => x.DeleteInstructor(2))
                .Returns(Task.FromResult(true))
                .Verifiable();
            var controller = new InstructorsController(service.Object, Mapper.Instance);

            var result = await controller.DeleteInstructor(2);

            Assert.IsTrue(result is OkResult);
            service.VerifyAll();
        }

        private static void AssertAreEqual(InstructorViewModel expected, Instructor actual)
        {
            if (expected == null && actual == null) return;
            if (expected == null || actual == null) Assert.Fail();

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.LastName);
        }
    }
}