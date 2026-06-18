using Hospital.PatientService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Hospital.PatientService.Data
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options)
        : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.PatientId);

                entity.Property(p => p.FullName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(p => p.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(p => p.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(p => p.Gender)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(p => p.Address)
                    .HasMaxLength(300);
            });
        }
    }
}
