using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Georest.Domain.Models;
using Georest.Domain.Services;
using System.Threading.Tasks;

namespace Georest.Domain.Tests.Services
{
    [TestClass]
    public class StudentServiceTests : DatabaseServiceTests
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

            using (var context = new ApplicationDbContext(Options))
            {
                await context.Instructors.AddAsync(instructor);
                await context.Sections.AddAsync(section);
                await context.SaveChangesAsync();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StudentService_RequiresDbContext()
        {
            new StudentService(null);
        }

        [TestMethod]
        public async Task AddStudent_PersistsStudent()
        {
            var student = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                SectionId = 1
            };
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new StudentService(context);

                Student addedStudent = await service.AddStudent(student);
                Assert.AreEqual(addedStudent, student);
                Assert.AreNotEqual(0, addedStudent.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Student retrievedStudent = context.Students.Single();
                Assert.AreEqual(student.FirstName, retrievedStudent.FirstName);
                Assert.AreEqual(student.LastName, retrievedStudent.LastName);
            }
        }

        [TestMethod]
        public async Task UpdateStudent_UpdatesExistingStudent()
        {
            var student = new Student
            {
                FirstName = "John",
                LastName = "Doe",
                SectionId = 1
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.Students.Add(student);
                context.SaveChanges();
            }

            student.FirstName = "Jane";
            student.LastName = "Other";
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new StudentService(context);
                Student updatedStudent = await service.UpdateStudent(student);
                Assert.AreEqual(student, updatedStudent);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                Student retrievedStudent = context.Students.Single();
                Assert.AreEqual(student.Id, retrievedStudent.Id);
                Assert.AreEqual(student.FirstName, retrievedStudent.FirstName);
                Assert.AreEqual(student.LastName, retrievedStudent.LastName);
            }
        }
    }
}