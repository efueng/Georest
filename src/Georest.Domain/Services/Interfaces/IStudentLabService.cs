using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Georest.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Georest.Domain.Services.Interfaces
{
    public interface IStudentLabService
    {
        Task<StudentLab> AddLab(StudentLab lab);
        Task<StudentLab> GetById(int labId);
        Task<bool> DeleteLab(int labId);
        Task<ICollection<StudentLab>> GetLabsForStudent(int studentId);
        Task<StudentLab> UpdateLab(StudentLab lab);
    }
}
