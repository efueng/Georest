using Georest.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.ViewModels
{
    public class SectionViewModel
    {
        public int Id { get; set; }
        public string SectionString { get; set; }
        public int InstructorId { get; set; }
    }
}
