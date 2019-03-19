using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Georest.Domain.Models;
using Georest.Domain.Services;
using System.Threading.Tasks;

namespace Georest.Domain.Tests.Services
{
    [TestClass]
    public class InstructorResponseServiceTests : DatabaseServiceTests
    {
        [TestInitialize]
        public async Task Init()
        {
            var exercise = new Exercise
            {
                Title = "Exercise Title",
                Body = "Exercise Body"
            };

            using (var context = new ApplicationDbContext(Options))
            {
                await context.Exercises.AddAsync(exercise);
                await context.SaveChangesAsync();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstructorResponseService_RequiresDbContext()
        {
            new InstructorResponseService(null);
        }

        [TestMethod]
        public async Task AddInstructorResponse_PersistsInstructorResponse()
        {
            var instructorResponse = new InstructorResponse
            {
                Body = "InstructorResponse Body",
                ExerciseId = 1
            };
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new InstructorResponseService(context);

                InstructorResponse addedInstructorResponse = await service.AddResponse(instructorResponse);
                Assert.AreEqual(addedInstructorResponse, instructorResponse);
                Assert.AreEqual(addedInstructorResponse.Body, instructorResponse.Body);
                Assert.AreNotEqual(0, addedInstructorResponse.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                InstructorResponse retrievedInstructorResponse = context.InstructorResponses.Single();
                Assert.AreEqual(instructorResponse.Body, retrievedInstructorResponse.Body);
            }
        }

        [TestMethod]
        public async Task UpdateInstructorResponse_UpdatesExistingInstructorResponse()
        {
            var instructorResponse = new InstructorResponse
            {
                Body = "InstructorResponse Body",
                ExerciseId = 1
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.InstructorResponses.Add(instructorResponse);
                context.SaveChanges();
            }

            instructorResponse.Body = "Updated Body";
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new InstructorResponseService(context);
                InstructorResponse updatedInstructorResponse = await service.UpdateResponse(instructorResponse);
                Assert.AreEqual(instructorResponse.Body, updatedInstructorResponse.Body);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                InstructorResponse retrievedInstructorResponse = context.InstructorResponses.Single();
                Assert.AreEqual(instructorResponse.Id, retrievedInstructorResponse.Id);
                Assert.AreEqual(instructorResponse.Body, retrievedInstructorResponse.Body);
                Assert.AreEqual(instructorResponse.ExerciseId, retrievedInstructorResponse.ExerciseId);
            }
        }
    }
}