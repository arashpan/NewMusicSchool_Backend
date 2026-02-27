using MusicSchool.Domain.Entities;
using System.Linq;

namespace MusicSchool.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // اطمینان از اینکه دیتابیس ساخته شده است
            context.Database.EnsureCreated();

            // اگر داده‌ها قبلاً وجود داشته باشد، از اضافه کردن مجدد جلوگیری می‌کنیم
            if (context.Users.Any()) return;

            // ایجاد نقش‌ها
            var superUserRole = new Role { Name = "SuperUser", Description = "Super User with all permissions" };
            var adminRole = new Role { Name = "Admin", Description = "Administrator Role" };
            var managerRole = new Role { Name = "Manager", Description = "Branch Manager Role" };
            var secretaryRole = new Role { Name = "Secretary", Description = "Secretary Role" };
            var workerRole = new Role { Name = "Worker", Description = "Worker Role" };

            // اضافه کردن نقش‌ها به دیتابیس
            context.Roles.AddRange(superUserRole, adminRole, managerRole, secretaryRole, workerRole);
            context.SaveChanges();

            // ایجاد کاربر SuperUser
            var superUser = new User
            {
                Username = "superuser",
                FullName = "Super User",
                CreatedAt = DateTime.UtcNow, // تنظیم تاریخ ایجاد
                UpdatedAt = DateTime.UtcNow,  // تنظیم تاریخ به‌روزرسانی
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("supersecretpassword") // پسورد مشخص برای SuperUser
            };
            context.Users.Add(superUser);
            context.SaveChanges();

            // اتصال SuperUser به نقش SuperUser
            var superUserRoleMapping = new UserRole { UserId = superUser.Id, RoleId = superUserRole.Id };
            context.UserRoles.Add(superUserRoleMapping);

            // ایجاد کاربران دیگر مانند Admin
            var adminUser = new User
            {
                Username = "admin",
                FullName = "Admin User",
                CreatedAt = DateTime.UtcNow, // تنظیم تاریخ ایجاد
                UpdatedAt = DateTime.UtcNow,  // تنظیم تاریخ به‌روزرسانی
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("adminpassword") // پسورد دلخواه
            };
            context.Users.Add(adminUser);
            context.SaveChanges();

            // اتصال Admin به نقش Admin
            var adminUserRoleMapping = new UserRole { UserId = adminUser.Id, RoleId = adminRole.Id };
            context.UserRoles.Add(adminUserRoleMapping);

            // اضافه کردن سایر کاربران به همین صورت برای Manager، Secretary و Worker
            var managerUser = new User
            {
                Username = "manager",
                FullName = "Manager",
                CreatedAt = DateTime.UtcNow, // تنظیم تاریخ ایجاد
                UpdatedAt = DateTime.UtcNow,  // تنظیم تاریخ به‌روزرسانی
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("managerpassword")
            };
            context.Users.Add(managerUser);
            context.SaveChanges();

            var managerUserRoleMapping = new UserRole { UserId = managerUser.Id, RoleId = managerRole.Id };
            context.UserRoles.Add(managerUserRoleMapping);

            var secretaryUser = new User
            {
                Username = "secretary",
                FullName = "Secretary",
                CreatedAt = DateTime.UtcNow, // تنظیم تاریخ ایجاد
                UpdatedAt = DateTime.UtcNow,  // تنظیم تاریخ به‌روزرسانی
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("secretarypassword")
            };
            context.Users.Add(secretaryUser);
            context.SaveChanges();

            var secretaryUserRoleMapping = new UserRole { UserId = secretaryUser.Id, RoleId = secretaryRole.Id };
            context.UserRoles.Add(secretaryUserRoleMapping);

            var workerUser = new User
            {
                Username = "worker",
                FullName = "Worker",
                CreatedAt = DateTime.UtcNow, // تنظیم تاریخ ایجاد
                UpdatedAt = DateTime.UtcNow,  // تنظیم تاریخ به‌روزرسانی
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("workerpassword")
            };
            context.Users.Add(workerUser);
            context.SaveChanges();

            var workerUserRoleMapping = new UserRole { UserId = workerUser.Id, RoleId = workerRole.Id };
            context.UserRoles.Add(workerUserRoleMapping);

            // ذخیره تمامی تغییرات
            context.SaveChanges();
        }
    }
}