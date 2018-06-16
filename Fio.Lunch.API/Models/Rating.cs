using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fio.Lunch.API.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public User User { get; set; }

        public Meal Meal { get; set; }

        public Order Order { get; set; }
    }
}
