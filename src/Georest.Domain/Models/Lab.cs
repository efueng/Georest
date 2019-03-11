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
        public string Title
        {
            get => _Title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title of lab is invalid.", nameof(value));
                }

                value = value.Trim();
                _Title = value;
            }
        }
        private string _Title;

        public bool IsPublished { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime PublishedOn { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
