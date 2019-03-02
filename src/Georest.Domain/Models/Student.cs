using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Georest.Domain.Models
{
    public class Student : Entity
    {
        public int SectionId { get; set; }
        [ForeignKey("SectionId")]
        public Section Section { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
