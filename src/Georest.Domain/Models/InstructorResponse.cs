using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Georest.Domain.Models
{
    public class InstructorResponse : Entity
    {
        public int ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }

        [Required]
        public string Body
        {
            get => _Body;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Body is required for InstructorResponse.", nameof(value));
                }

                value = value.Trim();
                _Body = value;
            }
        }
        private string _Body;
    }
}
