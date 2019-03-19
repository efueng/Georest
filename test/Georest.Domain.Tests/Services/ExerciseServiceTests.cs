using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Georest.Domain.Models;
using Georest.Domain.Services;
using System.Threading.Tasks;

namespace Georest.Domain.Tests.Services
{
    [TestClass]
    public class ExerciseServiceTests : DatabaseServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExerciseService_RequiresDbContext()
        {
            new ExerciseService(null);
        }

        [TestMethod]
        public async Task AddExercise_PersistsExercise()
        {
            var exercise = new Exercise
            {
                Title = "Exercise Title",
                Body = "Exercise Body"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new ExerciseService(context);

                Exercise addedExercise = await service.AddExercise(exercise);
                Assert.AreEqual(addedExercise, exercise);
                Assert.AreNotEqual(0, addedExercise.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Exercise retrievedExercise = context.Exercises.Single();
                Assert.AreEqual(exercise.Title, retrievedExercise.Title);
                Assert.AreEqual(exercise.Body, retrievedExercise.Body);
            }
        }

        [TestMethod]
        public async Task UpdateExercise_UpdatesExistingExercise()
        {
            var exercise = new Exercise
            {
                Title = "Exercise Title",
                Body = "Exercise Body"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.Exercises.Add(exercise);
                context.SaveChanges();
            }

            exercise.Title = "Updated Title";
            exercise.Body = "Update Body";
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new ExerciseService(context);
                Exercise updatedExercise = await service.UpdateExercise(exercise);
                Assert.AreEqual(exercise, updatedExercise);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Exercise retrievedExercise = context.Exercises.Single();
                Assert.AreEqual(exercise.Id, retrievedExercise.Id);
                Assert.AreEqual(exercise.Title, retrievedExercise.Title);
                Assert.AreEqual(exercise.Body, retrievedExercise.Body);
            }
        }
    }
}