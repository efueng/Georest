using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Georest.Domain.Models;
using Georest.Domain.Services;
using System.Threading.Tasks;

namespace Georest.Domain.Tests.Services
{
    [TestClass]
    public class StudentLabServiceTests : DatabaseServiceTests
    {
        [TestInitialize]
        public async Task Init()
        {
            var instructor = new Instructor
            {
                FirstName = "John",
                LastName = "Doe"
            };

            var section = new Section
            {
                SectionString = "Section 1",
                InstructorId = 1
            };


            var student = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                SectionId = 1
            };

            using (var context = new ApplicationDbContext(Options))
            {
                await context.Instructors.AddAsync(instructor);
                await context.Sections.AddAsync(section);
                await context.Students.AddAsync(student);
                context.SaveChanges();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StudentLabService_RequiresDbContext()
        {
            new StudentLabService(null);
        }

        [TestMethod]
        public async Task AddStudentLab_PersistsStudentLab()
        {
            var lab = new StudentLab
            {
                StudentId = 1

            };
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new StudentLabService(context);

                StudentLab addedStudentLab = await service.AddLab(lab);
                Assert.AreNotEqual(0, addedStudentLab.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                StudentLab retrievedStudentLab = context.StudentLabs.Single();
                Assert.AreEqual(lab.Id, retrievedStudentLab.Id);
                Assert.AreEqual(lab.StudentId, retrievedStudentLab.StudentId);
            }
        }

        //[TestMethod]
        //public async Task UpdateStudentLab_UpdatesExistingStudentLab()
        //{
        //    var lab = new StudentLab
        //    {
        //        StudentId = 1

        //    };

        //    lab.Title = "Lab Updated";
        //    using (var context = new ApplicationDbContext(Options))
        //    {
        //        var service = new StudentLabService(context);
        //        StudentLab updatedStudentLab = await service.UpdateLab(lab);
        //        Assert.AreEqual(lab, updatedStudentLab);
        //    }

        //    using (var context = new ApplicationDbContext(Options))
        //    {
        //        StudentLab retrievedStudentLab = context.StudentLabs.Single();
        //        Assert.AreEqual(lab.Id, retrievedStudentLab.Id);
        //        Assert.AreEqual(lab.Title, retrievedStudentLab.Title);
        //    }
        //}
    }
}