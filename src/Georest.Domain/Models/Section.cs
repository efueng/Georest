using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Georest.Domain.Models
{
    public class Section : Entity
    {
        [Required]
        public string SectionString
        {
            get => _SectionString;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Section is required.", nameof(value));
                }

                value = value.Trim();
                _SectionString = value;
            }
        }
        private string _SectionString;
        public int InstructorId { get; set; }
        [ForeignKey("InstructorId")]
        public Instructor Instructor { get; set; }
    }
}
