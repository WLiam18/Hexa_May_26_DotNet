using Hospital.AppointmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.AppointmentService.Data
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.AppointmentId);

                entity.Property(a => a.AppointmentStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(a => a.Reason)
                    .HasMaxLength(300);
            });
        }
    }
}
