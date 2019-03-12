using Georest.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.ViewModels
{
    public class SectionInputViewModel
    {
        [Required]
        public string SectionString { get; set; }
        public Instructor Instructor { get; set; }
    }
}
