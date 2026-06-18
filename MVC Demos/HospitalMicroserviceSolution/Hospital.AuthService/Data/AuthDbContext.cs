using Hospital.AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.AuthService.Data
{
    public class AuthDbContext : DbContext
    {
      
            public AuthDbContext(DbContextOptions<AuthDbContext> options)
                : base(options)
            {
            }

            public DbSet<User> Users { get; set; }

            public DbSet<Role> Roles { get; set; }

            public DbSet<UserRole> UserRoles { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>(entity =>
                {
                    entity.HasKey(u => u.UserId);

                    entity.Property(u => u.Username)
                        .IsRequired()
                        .HasMaxLength(100);

                    entity.HasIndex(u => u.Username)
                        .IsUnique();

                    entity.Property(u => u.PasswordHash)
                        .IsRequired()
                        .HasMaxLength(500);

                    entity.Property(u => u.FullName)
                        .IsRequired()
                        .HasMaxLength(150);
                });

                modelBuilder.Entity<Role>(entity =>
                {
                    entity.HasKey(r => r.RoleId);

                    entity.Property(r => r.RoleName)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.HasIndex(r => r.RoleName)
                        .IsUnique();
                });

                modelBuilder.Entity<UserRole>(entity =>
                {
                    entity.HasKey(ur => ur.UserRoleId);

                    entity.HasOne(ur => ur.User)
                        .WithMany(u => u.UserRoles)
                        .HasForeignKey(ur => ur.UserId);

                    entity.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId);
                });
            }
        }
    }


