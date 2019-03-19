using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Georest.Domain.Models;
using Georest.Domain.Services;
using System.Threading.Tasks;

namespace Georest.Domain.Tests.Services
{
    [TestClass]
    public class StudentResponseServiceTests : DatabaseServiceTests
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
        public void StudentResponseService_RequiresDbContext()
        {
            new StudentResponseService(null);
        }

        [TestMethod]
        public async Task AddStudentResponse_PersistsStudentResponse()
        {
            var studentResponse = new StudentResponse
            {
                Body = "StudentResponse Body",
                ExerciseId = 1
            };
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new StudentResponseService(context);

                StudentResponse addedStudentResponse = await service.AddResponse(studentResponse);
                Assert.AreEqual(addedStudentResponse, studentResponse);
                Assert.AreEqual(addedStudentResponse.Body, studentResponse.Body);
                Assert.AreNotEqual(0, addedStudentResponse.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                StudentResponse retrievedStudentResponse = context.StudentResponses.Single();
                Assert.AreEqual(studentResponse.Body, retrievedStudentResponse.Body);
            }
        }

        [TestMethod]
        public async Task UpdateStudentResponse_UpdatesExistingStudentResponse()
        {
            var studentResponse = new StudentResponse
            {
                Body = "StudentResponse Body",
                ExerciseId = 1
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.StudentResponses.Add(studentResponse);
                context.SaveChanges();
            }
            
            studentResponse.Body = "Updated Body";
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new StudentResponseService(context);
                StudentResponse updatedStudentResponse = await service.UpdateResponse(studentResponse);
                Assert.AreEqual(studentResponse.Body, updatedStudentResponse.Body);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                StudentResponse retrievedStudentResponse = context.StudentResponses.Single();
                Assert.AreEqual(studentResponse.Id, retrievedStudentResponse.Id);
                Assert.AreEqual(studentResponse.Body, retrievedStudentResponse.Body);
                Assert.AreEqual(studentResponse.ExerciseId, retrievedStudentResponse.ExerciseId);
            }
        }
    }
}