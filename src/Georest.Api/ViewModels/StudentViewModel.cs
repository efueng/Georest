using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public SectionViewModel Section { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<StudentLabViewModel> StudentLabs { get; set; }
    }
}
