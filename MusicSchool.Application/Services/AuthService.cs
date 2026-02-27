using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MusicSchool.Application.DTOs;
using MusicSchool.Application.Services;
using MusicSchool.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicSchool.Application.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // متد برای ایجاد توکن JWT
        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["Jwt:ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // متد برای ورود (تأیید کاربر)
        public bool ValidateUser(LoginDTO loginDTO)
        {
            // برای سادگی فرض می‌کنیم که یک کاربر فرضی در دیتابیس داریم
            var user = GetUserFromDatabase(loginDTO.Username);

            if (user == null) return false;

            // مقایسه پسورد وارد شده با پسورد هش شده
            return BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash);
        }

        // فرض می‌کنیم یک کاربر از دیتابیس می‌آید (در عمل این اطلاعات از دیتابیس گرفته می‌شود)
        private User GetUserFromDatabase(string username)
        {
            // در اینجا باید کدی برای بازیابی کاربر از دیتابیس بنویسید
            // این فقط یک شبیه‌سازی است
            if (username == "admin")
            {
                return new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("adminpassword")
                };
            }

            return null;
        }
    }
}