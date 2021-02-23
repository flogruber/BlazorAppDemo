using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAppDemo.Models;

namespace BlazorAppDemo.DB
{
    public class productDBContext : DbContext
    {
        public productDBContext(DbContextOptions<productDBContext> options): base(options)
        {
            // Nothing to do
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Nothing to do
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DevProduct>()
                .ToContainer("devProducts");

            modelBuilder.Entity<DevProduct>()
                .HasKey(x => x.productID);

            modelBuilder.Entity<DevProduct>()
                .OwnsMany(x => x.Comments);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<DevProduct> Products { get; set; }
        public DbSet<DevProductComments> Comments { get; set; }
    }
}
