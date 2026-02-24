using Customer.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Infrastucture.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CustomerDetails> CustomerDetails { get; set; }
        public DbSet<CustomerKyc> CustomerKycs { get; set; }

        public DbSet<DocumentType> DocumentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CustomerKyc>()
            //    .HasOne(k => k.Customer)
            //    .WithMany(k => k.CustomerKycs)

            //    .HasForeignKey(k => k.CustomerId)
            //    .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<CustomerKyc>()
                .HasOne(k => k.DocumentType)
                .WithMany()
                .HasForeignKey(k => k.DocTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
