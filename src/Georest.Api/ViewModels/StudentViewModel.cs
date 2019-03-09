using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public int SectionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
