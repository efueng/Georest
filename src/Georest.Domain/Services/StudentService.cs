using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services
{
    public class StudentService : IStudentService
    {
        private ApplicationDbContext DbContext { get; }
        public StudentService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<Student> AddStudent(Student student)
        {
            DbContext.Students.Add(student);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return student;
        }

        public async Task<bool> DeleteStudent(int studentId)
        {
            Student fetchedStudent = DbContext.Students.Find(studentId);

            if (fetchedStudent != null)
            {
                DbContext.Students.Remove(fetchedStudent);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }

            return false;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await DbContext.Students.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Student> GetById(int studentId)
        {
            return await DbContext.Students.FindAsync(studentId).ConfigureAwait(false);
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            DbContext.Students.Update(student);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return student;
        }
    }
}
