using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Georest.Domain.Models;
using Georest.Domain.Services;
using System.Threading.Tasks;

namespace Georest.Domain.Tests.Services
{
    [TestClass]
    public class InstructorServiceTests : DatabaseServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstructorService_RequiresDbContext()
        {
            new InstructorService(null);
        }

        [TestMethod]
        public async Task AddInstructor_PersistsInstructor()
        {
            var instructor = new Instructor
            {
                FirstName = "John",
                LastName = "Doe"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new InstructorService(context);

                Instructor addedInstructor = await service.AddInstructor(instructor);
                Assert.AreEqual(addedInstructor, instructor);
                Assert.AreNotEqual(0, addedInstructor.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Instructor retrievedInstructor = context.Instructors.Single();
                Assert.AreEqual(instructor.FirstName, retrievedInstructor.FirstName);
                Assert.AreEqual(instructor.LastName, retrievedInstructor.LastName);
            }
        }

        [TestMethod]
        public async Task UpdateInstructor_UpdatesExistingInstructor()
        {
            var instructor = new Instructor
            {
                FirstName = "John",
                LastName = "Doe"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.Instructors.Add(instructor);
                context.SaveChanges();
            }

            instructor.FirstName = "Jane";
            instructor.LastName = "Other";
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new InstructorService(context);
                Instructor updatedInstructor = await service.UpdateInstructor(instructor);
                Assert.AreEqual(instructor, updatedInstructor);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Instructor retrievedInstructor = context.Instructors.Single();
                Assert.AreEqual(instructor.Id, retrievedInstructor.Id);
                Assert.AreEqual(instructor.FirstName, retrievedInstructor.FirstName);
                Assert.AreEqual(instructor.LastName, retrievedInstructor.LastName);
            }
        }
    }
}