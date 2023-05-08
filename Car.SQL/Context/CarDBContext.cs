using Car.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Car = Car.Model.Car;

namespace Car.SQL.Context
{
    public class CarDBContext : DbContext
    {
        public CarDBContext(DbContextOptions<CarDBContext> options) : base(options)
        {
        }

        public DbSet<_Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Renter> Renters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<_Car>()
                .HasIndex(c => c.CarNumber)
                .IsUnique();
        }
    }
}
