using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Georest.Domain.Models
{
    public class InstructorLab : Lab
    {
        public int InstructorId { get; set; }
        [ForeignKey("InstructorId")]
        public Instructor Instructor { get; set; }
        public ICollection<InstructorResponse> Responses { get; set; }
    }
}
