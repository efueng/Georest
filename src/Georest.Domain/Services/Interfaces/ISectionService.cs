using Georest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Georest.Domain.Services.Interfaces
{
    public interface ISectionService
    {
        Task<Section> AddSection(Section section);
        Task<Section> GetById(int sectionId);
        Task<bool> DeleteSection(int sectionId);
        Task<ICollection<Section>> GetAllSections();
        Task<Section> UpdateSection(Section section);
    }
}
