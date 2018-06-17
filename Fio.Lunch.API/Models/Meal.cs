using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fio.Lunch.API.Models
{
    public class Meal
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Rating { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string  Keywords { get; set; }
    }
}
