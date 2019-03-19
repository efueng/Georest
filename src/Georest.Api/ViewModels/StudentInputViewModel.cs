using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.ViewModels
{
    public class StudentInputViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int InstructorId { get; set; }
        public InstructorInputViewModel Instructor { get; set; }

    }
}
