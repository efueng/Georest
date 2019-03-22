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
    public class ExercisesControllerTests
    {
        private CustomWebApplicationFactory<Startup> Factory { get; set; }

        [TestMethod]
        public async Task GetAllExercises_ReturnsExercises()
        {
            var client = Factory.CreateClient();

            var exercise1 = new Exercise
            {
                Title = "Exercise 1",
                Body = "Body 1"
            };
            var exercise2 = new Exercise
            {
                Title = "Exercise 2",
                Body = "Body 2"
            };

            var service = new Mock<IExerciseService>();
            //service.Setup(x => x.GetAllExercises())
            //    .Returns(Task.FromResult(new Collection<Exercise> { exercise1, exercise2 }))
            //    .Verifiable();


            var controller = new ExercisesController(service.Object, Mapper.Instance);
            await controller.AddExercise(Mapper.Map<ExerciseInputViewModel>(exercise1));
            await controller.AddExercise(Mapper.Map<ExerciseInputViewModel>(exercise2));
            var result = (await controller.GetAllExercises()).Result as OkObjectResult;

            List<ExerciseViewModel> exercises = ((IEnumerable<ExerciseViewModel>)result.Value).ToList();

            Assert.AreEqual(2, exercises.Count);
            AssertAreEqual(exercises[0], exercise1);
            AssertAreEqual(exercises[1], exercise2);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task CreateExercise_RequiresExercise()
        {
            var service = new Mock<IExerciseService>();
            var controller = new ExercisesController(service.Object, Mapper.Instance);

            var result = (await controller.AddExercise(null)).Result as BadRequestResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateExercise_ReturnsCreatedExercise()
        {
            var exercise = new Exercise
            {
                Title = "Exercise 1",
                Body = "Body 1"
            };

            var service = new Mock<IExerciseService>();
            service.Setup(x => x.AddExercise(It.Is<Exercise>(g => g.Title == exercise.Title)))
                .Returns(Task.FromResult(new Exercise
                {
                    Title = exercise.Title,
                    Body = exercise.Body
                }))
                .Verifiable();

            var controller = new ExercisesController(service.Object, Mapper.Instance);

            var result = (await controller.AddExercise(Mapper.Map<ExerciseInputViewModel>(exercise))).Result as OkObjectResult;
            Exercise resultValue = (Exercise)result.Value;

            Assert.IsNotNull(resultValue);
            Assert.AreEqual("Exercise 1", resultValue.Title);
            Assert.AreEqual("Body 1", resultValue.Body);
            service.VerifyAll();
        }

        //[TestMethod]
        //public async Task UpdateExercise_RequiresExercise()
        //{
        //    var service = new Mock<IExerciseService>(MockBehavior.Strict);
        //    var controller = new ExercisesController(service.Object, Mapper.Instance);


        //    BadRequestResult result = await controller.UpdateExercise(1, null);

        //    Assert.IsNotNull(result);
        //}

        [TestMethod]
        public async Task UpdateExercise_ReturnsUpdatedExercise()
        {
            var exercise = new Exercise
            {
                Title = "Exercise 1",
                Body = "Body 1"
            };
            var service = new Mock<IExerciseService>();
            service.Setup(x => x.AddExercise(It.Is<Exercise>(g => g.Title == exercise.Title)))
                .Returns(Task.FromResult(new Exercise
                {
                    Title = exercise.Title,
                    Body = exercise.Body
                }))
                .Verifiable();

            var controller = new ExercisesController(service.Object, Mapper.Instance);

            var result = (await controller.UpdateExercise(1, Mapper.Map<ExerciseInputViewModel>(exercise)));

            Assert.IsNotNull(result);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task DeleteExercise_RequiresPositiveId(int exerciseId)
        {
            var service = new Mock<IExerciseService>(MockBehavior.Strict);
            var controller = new ExercisesController(service.Object, Mapper.Instance);

            var result = await controller.DeleteExercise(exerciseId);

            Assert.IsTrue(result is BadRequestObjectResult);
        }

        [TestMethod]
        public async Task DeleteExercise_ReturnsNotFoundWhenTheExerciseFailsToDelete()
        {
            var service = new Mock<IExerciseService>();
            service.Setup(x => x.DeleteExercise(2))
                .Returns(Task.FromResult(false))
                .Verifiable();
            var controller = new ExercisesController(service.Object, Mapper.Instance);

            var result = await controller.DeleteExercise(2);

            Assert.IsTrue(result is NotFoundResult);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteExercise_ReturnsOkWhenExerciseIsDeleted()
        {
            var service = new Mock<IExerciseService>();
            service.Setup(x => x.DeleteExercise(2))
                .Returns(Task.FromResult(true))
                .Verifiable();
            var controller = new ExercisesController(service.Object, Mapper.Instance);

            var result = await controller.DeleteExercise(2);

            Assert.IsTrue(result is OkResult);
            service.VerifyAll();
        }

        private static void AssertAreEqual(ExerciseViewModel expected, Exercise actual)
        {
            if (expected == null && actual == null) return;
            if (expected == null || actual == null) Assert.Fail();

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Body, actual.Body);
        }
    }
}