using Microsoft.AspNetCore.Mvc;
using MusicSchool.Application.Abstractions.Services;
using MusicSchool.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.API.Controllers
{
    [Route("api/superadmins")]
    [ApiController]
    public class SuperAdminsController : ControllerBase
    {
        private readonly ISuperAdminService _superAdminService;

        public SuperAdminsController(ISuperAdminService superAdminService)
        {
            _superAdminService = superAdminService;
        }

        // دریافت لیست تمام مدیران کل
        [HttpGet]
        public async Task<ActionResult<List<SuperAdmin>>> GetSuperAdmins(CancellationToken ct)
        {
            var superAdmins = await _superAdminService.GetAllAsync(ct);
            return Ok(superAdmins);
        }

        // دریافت اطلاعات یک مدیر کل خاص
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperAdmin>> GetSuperAdmin(int id, CancellationToken ct)
        {
            var superAdmin = await _superAdminService.GetByIdAsync(id, ct);
            if (superAdmin == null)
            {
                return NotFound();
            }
            return Ok(superAdmin);
        }

        // اضافه کردن مدیر کل جدید
        [HttpPost]
        public async Task<ActionResult<SuperAdmin>> CreateSuperAdmin([FromBody] SuperAdmin superAdmin, CancellationToken ct)
        {
            var createdSuperAdmin = await _superAdminService.CreateAsync(superAdmin, ct);
            return CreatedAtAction(nameof(GetSuperAdmin), new { id = createdSuperAdmin.Id }, createdSuperAdmin);
        }

        // به‌روزرسانی اطلاعات مدیر کل
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSuperAdmin(int id, [FromBody] SuperAdmin updatedSuperAdmin, CancellationToken ct)
        {
            await _superAdminService.UpdateAsync(id, updatedSuperAdmin, ct);
            return NoContent();
        }

        // حذف مدیر کل
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperAdmin(int id, CancellationToken ct)
        {
            await _superAdminService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}