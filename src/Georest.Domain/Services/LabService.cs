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
    public class LabService : ILabService
    {
        private ApplicationDbContext DbContext { get; }
        public LabService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<Lab> AddLab(Lab lab)
        {
            if (lab == null) throw new ArgumentNullException(nameof(lab));

            DbContext.Labs.Add(lab);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return lab;
        }

        public async Task<bool> DeleteLab(int labId)
        {
            Lab labToDelete = await DbContext.Labs.FindAsync(labId).ConfigureAwait(false);

            if (labToDelete == null)
            {
                DbContext.Labs.Remove(labToDelete);
                DbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public async Task<Lab> GetById(int labId)
        {
            Lab fetchedLab = await DbContext.Labs.SingleOrDefaultAsync(lab => lab.Id == labId).ConfigureAwait(false);

            return fetchedLab;
        }

        public async Task<List<Lab>> GetLabsForStudent(int studentId)
        {
            return (await DbContext.Students.FindAsync(studentId).ConfigureAwait(false)).Labs;

            //return await DbContext.Labs.Where(lab => lab.s)
        }

        public async Task<Lab> UpdateLab(Lab lab)
        {
            if (lab == null) throw new ArgumentNullException(nameof(lab));

            DbContext.Labs.Update(lab);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return lab;
        }
    }
}
