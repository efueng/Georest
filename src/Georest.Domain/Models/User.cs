﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Georest.Domain.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
