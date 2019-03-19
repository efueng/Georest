using Georest.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.ViewModels
{
    public class SectionInputViewModel
    {
        [Required]
        public string SectionString { get; set; }
        public int InstructorId { get; set; }
        //public InstructorViewModel Instructor { get; set; }
    }
}
