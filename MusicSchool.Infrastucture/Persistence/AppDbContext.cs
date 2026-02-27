using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicSchool.Domain.Common;
using MusicSchool.Domain.Entities;

namespace MusicSchool.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // قبلی‌ها
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        //======================================
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Secretary> Secretaries => Set<Secretary>();
        public DbSet<Manager> Managers => Set<Manager>();
        public DbSet<SuperAdmin> SuperAdmins => Set<SuperAdmin>();
        public DbSet<Branch> Branches => Set<Branch>();
        // جدیدها (ثبت نام)
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Parent> Parents => Set<Parent>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // تنظیم Discriminator برای تمایز مدل‌ها
            modelBuilder.Entity<Employee>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Manager>("Manager")
                .HasValue<Secretary>("Secretary")
                .HasValue<SuperAdmin>("SuperAdmin");

            // مشخص کردن جداول جداگانه برای هر مدل
            modelBuilder.Entity<Manager>().ToTable("Managers");
            modelBuilder.Entity<Secretary>().ToTable("Secretaries");
            modelBuilder.Entity<SuperAdmin>().ToTable("SuperAdmins");

            // تنظیمات برای Employee
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Branch)    // ارتباط با شعبه
                .WithMany()
                .HasForeignKey(e => e.BranchId)  // استفاده از BranchId برای ارتباط
                .OnDelete(DeleteBehavior.Restrict);  // رفتار حذف: جلوگیری از حذف شعبه زمانی که کارمندان وجود دارند

            // تنظیمات برای Manager (ارث‌بری از Employee)
            modelBuilder.Entity<Manager>()
                .HasOne(m => m.Branch)
                .WithMany()
                .HasForeignKey(m => m.BranchId)  // استفاده از BranchId برای ارتباط
                .OnDelete(DeleteBehavior.Restrict);

            // تنظیمات برای Secretary (ارث‌بری از Employee)
            modelBuilder.Entity<Secretary>()
                .HasOne(s => s.Branch)
                .WithMany()
                .HasForeignKey(s => s.BranchId)  // استفاده از BranchId برای ارتباط
                .OnDelete(DeleteBehavior.Restrict);

            // تنظیمات برای SuperAdmin (ارث‌بری از Manager)
            modelBuilder.Entity<SuperAdmin>()
                .HasOne(s => s.Branch)
                .WithMany()
                .HasForeignKey(s => s.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student
            modelBuilder.Entity<Student>(b =>
            {
                b.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
                b.Property(x => x.LastName).IsRequired().HasMaxLength(100);
                b.Property(x => x.DateOfBirth).IsRequired();

                b.Property(x => x.PhoneNumber).HasMaxLength(30);
                b.Property(x => x.Email).HasMaxLength(200);

                b.HasOne(x => x.Parent)
                 .WithMany()
                 .HasForeignKey(x => x.ParentId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            // Parent
            modelBuilder.Entity<Parent>(b =>
            {
                b.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
                b.Property(x => x.LastName).IsRequired().HasMaxLength(100);

                b.Property(x => x.PhoneNumber).HasMaxLength(30);
                b.Property(x => x.Email).HasMaxLength(200);
            });

            modelBuilder.Entity<Branch>(b =>
            {
                b.Property(x => x.Name).IsRequired().HasMaxLength(100);
                b.Property(x => x.Location).IsRequired().HasMaxLength(200);

                // تنظیم ارتباط ParentBranch و SubBranches
                b.HasOne(x => x.ParentBranch)
                 .WithMany(x => x.SubBranches)
                 .HasForeignKey(x => x.ParentBranchId)
                 .OnDelete(DeleteBehavior.Restrict); // حذف شعبه‌ها باعث حذف زیرمجموعه‌ها نمی‌شود
            });
        }

        public override int SaveChanges()
        {
            ApplyAuditTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditTimestamps()
        {
            var now = DateTime.UtcNow;

            var entries = ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = now;
                }
                else
                {
                    entry.Entity.UpdatedAt = now;
                }
            }
        }
    }
}