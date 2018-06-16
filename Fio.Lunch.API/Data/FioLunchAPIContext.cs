using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fio.Lunch.API.Models;

namespace Fio.Lunch.API.Models
{
    public class FioLunchAPIContext : DbContext
    {
        public FioLunchAPIContext (DbContextOptions<FioLunchAPIContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=fio.lunch.db");
        }

        public DbSet<Fio.Lunch.API.Models.Menu> Menu { get; set; }

        public DbSet<Fio.Lunch.API.Models.Meal> Meal { get; set; }

        public DbSet<Fio.Lunch.API.Models.Day> Day { get; set; }

        public DbSet<Fio.Lunch.API.Models.Order> Order { get; set; }

        public DbSet<Fio.Lunch.API.Models.User> User { get; set; }

        public DbSet<Fio.Lunch.API.Models.Resource> Resource { get; set; }

        public DbSet<Fio.Lunch.API.Models.Rating> Rating { get; set; }
    }
}
