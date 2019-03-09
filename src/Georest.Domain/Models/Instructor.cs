using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Georest.Domain.Models
{
    public class Instructor : Entity
    {
        [Required]
        public string FirstName
        {
            get => _FirstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("First name invalid for instructor.", nameof(value));
                }

                value = value.Trim();
                _FirstName = value;
            }
        }
        private string _FirstName;

        [Required]
        public string LastName
        {
            get => _LastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Last name invalid for instructor.", nameof(value));
                }

                value = value.Trim();
                _LastName = value;
            }
        }
        private string _LastName;

        public ICollection<InstructorLab> Labs { get; set; }
    }
}
