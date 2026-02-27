using Microsoft.EntityFrameworkCore;
using MusicSchool.Application.Abstractions.Repositories;
using MusicSchool.Application.Abstractions.Services;
using MusicSchool.Application.Features.Branches;
using MusicSchool.Application.Features.Employees;
using MusicSchool.Application.Features.Managers;
using MusicSchool.Application.Features.Students;
using MusicSchool.Application.Features.SuperAdmins;
using MusicSchool.Infrastructure.Persistence;
using MusicSchool.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentRegistrationService, StudentRegistrationService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<ISuperAdminService, SuperAdminService>();
builder.Services.AddScoped<ISuperAdminRepository, SuperAdminRepository>();
// پیکربندی DbContext برای اتصال به دیتابیس
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// سایر پیکربندی‌ها
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// داده‌های اولیه را به دیتابیس اضافه می‌کنیم
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(context); // فراخوانی متد برای اضافه کردن داده‌ها
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();