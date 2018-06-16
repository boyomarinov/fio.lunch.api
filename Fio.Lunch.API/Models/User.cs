﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fio.Lunch.API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public Resource UserImage { get; set; }
    }
}
