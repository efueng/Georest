using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Georest.Domain.Models;
using Georest.Domain.Services;
using System.Threading.Tasks;

namespace Georest.Domain.Tests.Services
{
    [TestClass]
    public class SectionServiceTests : DatabaseServiceTests
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
        public void SectionService_RequiresDbContext()
        {
            new SectionService(null);
        }

        [TestMethod]
        public async Task AddSection_PersistsSection()
        {
            var section = new Section
            {
                SectionString = "Section 1",
                InstructorId = 1
            };
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new SectionService(context);

                Section addedSection = await service.AddSection(section);
                Assert.AreEqual(addedSection, section);
                Assert.AreNotEqual(0, addedSection.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Section retrievedSection = context.Sections.Single();
                Assert.AreEqual(section.SectionString, retrievedSection.SectionString);
                Assert.AreEqual(section.InstructorId, retrievedSection.InstructorId);
            }
        }

        [TestMethod]
        public async Task UpdateSection_UpdatesExistingSection()
        {
            var section = new Section
            {
                SectionString = "Section 1",
                InstructorId = 1
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.Sections.Add(section);
                context.SaveChanges();
            }

            section.SectionString = "Updated section";
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new SectionService(context);
                Section updatedSection = await service.UpdateSection(section);
                Assert.AreEqual(section, updatedSection);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Section retrievedSection = context.Sections.Single();
                Assert.AreEqual(section.Id, retrievedSection.Id);
                Assert.AreEqual(section.SectionString, retrievedSection.SectionString);
                Assert.AreEqual(section.InstructorId, retrievedSection.InstructorId);
            }
        }
    }
}