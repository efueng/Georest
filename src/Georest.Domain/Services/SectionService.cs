using Georest.Domain.Models;
using Georest.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services
{
    public class SectionService : ISectionService
    {
        private ApplicationDbContext DbContext { get; }
        public SectionService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<Section> AddSection(Section section)
        {
            DbContext.Sections.Add(section);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return section;
        }

        public async Task<bool> DeleteSection(int sectionId)
        {
            Section fetchedSection = DbContext.Sections.Find(sectionId);

            if (fetchedSection != null)
            {
                DbContext.Sections.Remove(fetchedSection);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }

            return false;
        }

        public async Task<ICollection<Section>> GetAllSections()
        {
            return await DbContext.Sections.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Section> GetById(int sectionId)
        {
            return await DbContext.Sections.FindAsync(sectionId).ConfigureAwait(false);
        }

        public async Task<Section> UpdateSection(Section section)
        {
            DbContext.Sections.Update(section);
            await DbContext.SaveChangesAsync().ConfigureAwait(false);

            return section;
        }
    }
}
