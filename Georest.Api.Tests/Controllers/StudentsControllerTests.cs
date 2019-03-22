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
    public class StudentsControllerTests
    {
        private CustomWebApplicationFactory<Startup> Factory { get; set; }

        [TestInitialize]
        public void CreateWebFactory()
        {
            Factory = new CustomWebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task GetAllStudents_ReturnsStudents()
        {
            var client = Factory.CreateClient();

            var student1 = new Student
            {
                FirstName = "Stu",
                LastName = "Steiner"
            };
            var student2 = new Student
            {
                FirstName = "Tom",
                LastName = "Capaul"
            };

            var service = new Mock<IStudentService>();
            //service.Setup(x => x.GetAllStudents())
            //    .Returns(Task.FromResult(new Collection<Student> { student1, student2 }))
            //    .Verifiable();


            var controller = new StudentsController(service.Object, Mapper.Instance);

            var result = (await controller.GetAllStudents()).Result as OkObjectResult;

            List<StudentViewModel> students = ((IEnumerable<StudentViewModel>)result.Value).ToList();

            Assert.AreEqual(2, students.Count);
            AssertAreEqual(students[0], student1);
            AssertAreEqual(students[1], student2);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task CreateStudent_RequiresStudent()
        {
            var service = new Mock<IStudentService>();
            var controller = new StudentsController(service.Object, Mapper.Instance);

            var result = (await controller.AddStudent(null)).Result as BadRequestResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateStudent_ReturnsCreatedStudent()
        {
            var student = new StudentInputViewModel
            {
                FirstName = "Stu",
                LastName = "Steiner"
            };

            var service = new Mock<IStudentService>();
            service.Setup(x => x.AddStudent(It.Is<Student>(g => g.FirstName == student.FirstName)))
                .Returns(Task.FromResult(new Student
                {
                    Id = 2,
                    FirstName = student.FirstName,
                    LastName = student.LastName
                }))
                .Verifiable();

            var controller = new StudentsController(service.Object, Mapper.Instance);

            var result = (await controller.AddStudent(student)).Result as OkObjectResult;
            Student resultValue = (Student)result.Value;

            Assert.IsNotNull(resultValue);
            Assert.AreEqual(2, resultValue.Id);
            Assert.AreEqual("Stu", resultValue.FirstName);
            Assert.AreEqual("Steiner", resultValue.LastName);
            service.VerifyAll();
        }

        //[TestMethod]
        //public async Task UpdateStudent_RequiresStudent()
        //{
        //    var service = new Mock<IStudentService>(MockBehavior.Strict);
        //    var controller = new StudentsController(service.Object, Mapper.Instance);


        //    BadRequestResult result = await controller.UpdateStudent(1, null);

        //    Assert.IsNotNull(result);
        //}

        [TestMethod]
        public async Task UpdateStudent_ReturnsUpdatedStudent()
        {
            var student = new StudentInputViewModel
            {
                FirstName = "Stu",
                LastName = "Steiner"
            };
            var service = new Mock<IStudentService>();
            //service.Setup(x => x.GetById(2))
            //    .Returns(Task.FromResult(new Student
            //    {
            //        Id = 2,
            //        Name = student.Name
            //    }))
            //    .Verifiable();

            var controller = new StudentsController(service.Object, Mapper.Instance);

            var result = (await controller.UpdateStudent(2, student));

            Assert.IsNotNull(result);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task DeleteStudent_RequiresPositiveId(int studentId)
        {
            var service = new Mock<IStudentService>(MockBehavior.Strict);
            var controller = new StudentsController(service.Object, Mapper.Instance);

            var result = await controller.DeleteStudent(studentId);

            Assert.IsTrue(result is BadRequestObjectResult);
        }

        [TestMethod]
        public async Task DeleteStudent_ReturnsNotFoundWhenTheStudentFailsToDelete()
        {
            var service = new Mock<IStudentService>();
            service.Setup(x => x.DeleteStudent(2))
                .Returns(Task.FromResult(false))
                .Verifiable();
            var controller = new StudentsController(service.Object, Mapper.Instance);

            var result = await controller.DeleteStudent(2);

            Assert.IsTrue(result is NotFoundResult);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteStudent_ReturnsOkWhenStudentIsDeleted()
        {
            var service = new Mock<IStudentService>();
            service.Setup(x => x.DeleteStudent(2))
                .Returns(Task.FromResult(true))
                .Verifiable();
            var controller = new StudentsController(service.Object, Mapper.Instance);

            var result = await controller.DeleteStudent(2);

            Assert.IsTrue(result is OkResult);
            service.VerifyAll();
        }

        private static void AssertAreEqual(StudentViewModel expected, Student actual)
        {
            if (expected == null && actual == null) return;
            if (expected == null || actual == null) Assert.Fail();

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.LastName);
        }
    }
}