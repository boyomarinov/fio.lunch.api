using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fio.Lunch.API.Models
{
    public class Order
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Menu Menu { get; set; }

        public Day Day { get; set; }

        public Meal Meal { get; set; }

        public bool SelfOrdered { get; set; }
    }
}
