using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fio.Lunch.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fio.Lunch.API.DAL
{
    public class LunchContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Resource> Resources { get; set; }

    }
}
