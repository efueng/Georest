using Georest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services.Interfaces
{
    public interface IStudentService
    {
        Task<Student> AddStudent(Student student);
        Task<Student> GetById(int studentId);
        Task<bool> DeleteStudent(int studentId);
        Task<ICollection<Student>> GetAllStudents();
        Task<Student> UpdateStudent(Student student);
    }
}
