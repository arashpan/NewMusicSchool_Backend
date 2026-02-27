using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MusicSchool.Infrastructure.Persistence;
using MusicSchool.Domain.Entities;
using MusicSchool.Application.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;  // برای بارگذاری کلید JWT از appsettings.json

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // متد برای ورود و گرفتن توکن JWT
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == loginDTO.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            // ایجاد توکن JWT
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        // متد برای ایجاد توکن JWT
        private string GenerateJwtToken(User user)
        {
            // گرفتن نقش‌ها برای کاربر
            var roleNames = _context.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.Role.Name)
                .ToList();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            // اضافه کردن نقش‌ها به توکن
            foreach (var roleName in roleNames)
                claims.Add(new Claim(ClaimTypes.Role, roleName));

            // استفاده از کلید JWT از appsettings.json
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

        // متد برای تغییر مشخصات کاربری
        [HttpPut("update-profile")]
        public IActionResult UpdateProfile([FromBody] UpdateProfileDTO updateProfileDTO)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == updateProfileDTO.Username);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // بروزرسانی FullName یا سایر اطلاعات
            user.FullName = updateProfileDTO.FullName ?? user.FullName;  // اگر FullName ارسال نشد، مقدار قبلی حفظ می‌شود.
            user.UpdatedAt = DateTime.UtcNow;  // بروزرسانی تاریخ آخرین تغییر
            _context.SaveChanges();

            return Ok("Profile updated successfully.");
        }

        // متد برای تغییر رمز عبور
        [HttpPut("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == changePasswordDTO.Username);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // بررسی اینکه پسورد قدیمی صحیح است
            if (!BCrypt.Net.BCrypt.Verify(changePasswordDTO.OldPassword, user.PasswordHash))
            {
                return BadRequest("Old password is incorrect.");
            }

            // تغییر پسورد
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(changePasswordDTO.NewPassword);
            _context.SaveChanges();

            return Ok("Password changed successfully.");
        }
    }
}