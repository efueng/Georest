using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Georest.Domain.Models
{
    public class Lab : Entity
    {
        [Required]
        public string Title { get; set; }
        public int InstructorId { get; set; }
        [ForeignKey("InstructorId")]
        public Instructor Instructor { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime PublishedOn { get; set; }
        public List<Exercise> Exercises { get; set; }
        public List<Response> Responses { get; set; }
    }
}
