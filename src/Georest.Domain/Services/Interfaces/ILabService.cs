using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Georest.Domain.Models;

namespace Georest.Domain.Services.Interfaces
{
    public interface ILabService
    {
        List<Lab> FetchAlLabs();
        Lab GetById(int id);
        Lab AddLab(Lab lab);
        Lab UpdateLab(Lab lab);
        bool DeleteLab(int id);

    }
}
