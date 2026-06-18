using Hospital.DoctorService.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.DoctorService.Data
{
    public class DoctorDbContext : DbContext
    {
        public DoctorDbContext(DbContextOptions<DoctorDbContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d.DoctorId);

                entity.Property(d => d.FullName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(d => d.Specialization)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(d => d.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(d => d.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(d => d.ConsultationFee)
                    .HasColumnType("decimal(18,2)");
            });
        }
    }
}
