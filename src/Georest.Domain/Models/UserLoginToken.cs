using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Domain.Models
{
    public class UserLoginToken
    {
        public string EWUid { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}
