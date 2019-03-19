using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Georest.Domain.Models;
using Georest.Domain.Services;
using System.Threading.Tasks;

namespace Georest.Domain.Tests.Services
{
    [TestClass]
    public class InstructorLabServiceTests : DatabaseServiceTests
    {
        [TestInitialize]
        public async Task Init()
        {
            var instructor = new Instructor
            {
                FirstName = "John",
                LastName = "Doe"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                await context.Instructors.AddAsync(instructor);
                await context.SaveChangesAsync();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstructorLabService_RequiresDbContext()
        {
            new InstructorLabService(null);
        }

        [TestMethod]
        public async Task AddInstructorLab_PersistsInstructorLab()
        {
            var lab = new InstructorLab
            {
                Title = "Lab 1",
                InstructorId = 1
                
            };
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new InstructorLabService(context);

                InstructorLab addedInstructorLab = await service.AddLab(lab);
                Assert.AreEqual(addedInstructorLab, lab);
                Assert.AreNotEqual(0, addedInstructorLab.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                InstructorLab retrievedInstructorLab = context.InstructorLabs.Single();
                Assert.AreEqual(lab.Title, retrievedInstructorLab.Title);
            }
        }

        [TestMethod]
        public async Task UpdateInstructorLab_UpdatesExistingInstructorLab()
        {
            var lab = new InstructorLab
            {
                Title = "Lab 1",
                InstructorId = 1

            };

            lab.Title = "Lab Updated";
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new InstructorLabService(context);
                InstructorLab updatedInstructorLab = await service.UpdateLab(lab);
                Assert.AreEqual(lab, updatedInstructorLab);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                InstructorLab retrievedInstructorLab = context.InstructorLabs.Single();
                Assert.AreEqual(lab.Id, retrievedInstructorLab.Id);
                Assert.AreEqual(lab.Title, retrievedInstructorLab.Title);
            }
        }
    }
}