using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;

namespace Georest.Domain.Services
{
    public class LabService : ILabService
    {
        private ApplicationDbContext DbContext { get; }

        public LabService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public List<Lab> FetchAlLabs()
        {
            return DbContext.Labs.ToList();
        }

        public Lab GetById(int id)
        {
            return DbContext.Labs.Find(id);
        }

        public Lab AddLab(Lab lab)
        {
            DbContext.Labs.Add(lab);
            DbContext.SaveChanges();
            return lab;
        }

        public Lab UpdateLab(Lab lab)
        {
            DbContext.Labs.Update(lab);
            DbContext.SaveChanges();
            return lab;
        }

        public bool DeleteLab(int id)
        {
            Lab foundLab = DbContext.Labs.Find(id);

            if (foundLab != null)
            {
                DbContext.Labs.Remove(foundLab);
                DbContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
