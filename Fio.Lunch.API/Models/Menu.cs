using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fio.Lunch.API.Models
{
    public class Menu
    {
        public int Id { get; set; }

        public IEnumerable<Day> Days { get; set; }

        public bool IsActive { get; set; }

    }
}
