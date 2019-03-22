using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services
{
    public class StudentLabService : IStudentLabService
    {
        private ApplicationDbContext DbContext { get; }
        public StudentLabService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<StudentLab> AddLab(StudentLab lab)
        {
            if (lab == null) throw new ArgumentNullException(nameof(lab));

            DbContext.StudentLabs.Add(lab);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return lab;
        }

        public async Task<bool> DeleteLab(int labId)
        {
            StudentLab labToDelete = await DbContext.StudentLabs.FindAsync(labId).ConfigureAwait(false);

            if (labToDelete == null)
            {
                DbContext.StudentLabs.Remove(labToDelete);
                DbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public async Task<StudentLab> GetById(int labId)
        {
            StudentLab fetchedLab = await DbContext.StudentLabs.SingleOrDefaultAsync(lab => lab.Id == labId).ConfigureAwait(false);

            return fetchedLab;
        }

        public async Task<ICollection<StudentLab>> GetLabsForStudent(int studentId)
        {
            return (await DbContext.Students.FindAsync(studentId).ConfigureAwait(false)).Labs;
        }

        public async Task<StudentLab> UpdateLab(StudentLab lab)
        {
            if (lab == null) throw new ArgumentNullException(nameof(lab));

            DbContext.StudentLabs.Update(lab);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return lab;
        }
    }
}
