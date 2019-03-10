using Georest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.ViewModels
{
    public class SectionViewModel
    {
        public string SectionString { get; set; }
        public Instructor Instructor { get; set; }
    }
}
