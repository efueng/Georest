using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Geolabs_Core.Models
{
    public class UserLoginToken
    {
        public string EWUid { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}