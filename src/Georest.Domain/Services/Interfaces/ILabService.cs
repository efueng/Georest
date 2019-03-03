using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Georest.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Georest.Domain.Services.Interfaces
{
    public interface ILabService
    {
        Task<Lab> AddLab(Lab lab);
        Task<Lab> GetById(int labId);
        Task<bool> DeleteLab(int labId);
        Task<List<Lab>> GetLabsForStudent(int studentId);
        Task<Lab> UpdateLab(Lab lab);
    }
}
