using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fio.Lunch.API.Models
{
    public class Day
    {
        public IEnumerable<Meal> Meals { get; set; }

        [Key]
        public DateTime Date { get; set; }

    }
}
