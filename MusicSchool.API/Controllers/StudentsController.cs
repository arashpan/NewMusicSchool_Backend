using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicSchool.Application.Abstractions.Services;
using MusicSchool.Application.DTOs.Students;

namespace MusicSchool.API.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        // دریافت اطلاعات یک دانش‌آموز
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id, CancellationToken ct)
        {
            var student = await _service.GetByIdAsync(id, ct);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // دریافت لیست تمام دانش‌آموزان
        [HttpGet]
        public async Task<ActionResult<List<StudentDto>>> GetAllStudents(CancellationToken ct)
        {
            var students = await _service.GetAllAsync(ct);
            return Ok(students);
        }

        // به‌روزرسانی اطلاعات دانش‌آموز
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto updateStudentDto, CancellationToken ct)
        {
            await _service.UpdateAsync(id, updateStudentDto, ct);
            return NoContent();
        }

        // حذف دانش‌آموز
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id, CancellationToken ct)
        {
            await _service.DeleteAsync(id, ct);
            return NoContent();
        }

        // فقط نقش‌هایی که اجازه مدیریت هنرجو دارند (Worker ندارد)
        //[Authorize(Roles = "SuperUser,Admin,Manager,Secretary")]
        [HttpPost("register")]
        public async Task<ActionResult<StudentDto>> Register([FromBody] RegisterStudentRequestDto request, CancellationToken ct)
        {
            // اعتبارسنجی پایه
            if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
                return BadRequest("FirstName و LastName اجباری است.");

            if (request.BranchId <= 0)
                return BadRequest("BranchId معتبر نیست.");

            if (request.DateOfBirth.Date > DateTime.UtcNow.Date)
                return BadRequest("DateOfBirth معتبر نیست (تاریخ تولد نمی‌تواند در آینده باشد).");

            // محاسبه سن
            var today = DateTime.UtcNow.Date;
            var age = today.Year - request.DateOfBirth.Year;
            if (request.DateOfBirth.Date > today.AddYears(-age)) age--;

            // اگر زیر 15 سال => Parent اجباری
            if (age < 15)
            {
                if (request.Parent == null)
                    return BadRequest("برای هنرجوی زیر ۱۵ سال، اطلاعات حداقل یک والد اجباری است.");

                if (string.IsNullOrWhiteSpace(request.Parent.FirstName) || string.IsNullOrWhiteSpace(request.Parent.LastName))
                    return BadRequest("FirstName و LastName والد اجباری است.");
            }

            var result = await _service.CreateAsync(request, ct);
            return Ok(result);
        }
    }
}