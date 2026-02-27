using Microsoft.AspNetCore.Mvc;
using MusicSchool.Application.Abstractions.Repositories;
using MusicSchool.Application.Abstractions.Services;
using MusicSchool.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MusicSchool.API.Controllers
{
    [Route("api/managers")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerService _managerService;
        private readonly ISuperAdminRepository _superAdminRepository;

        public ManagersController(IManagerService managerService, ISuperAdminRepository superAdminRepository)
        {
            _managerService = managerService;
            _superAdminRepository = superAdminRepository;
        }

        // ایجاد مدیر جدید
        [HttpPost]
        public async Task<ActionResult<Manager>> CreateManager([FromBody] Manager manager, CancellationToken ct)
        {
            if (manager == null)
            {
                return BadRequest("Manager data is required.");
            }

            var createdManager = await _managerService.CreateAsync(manager, ct);
            return CreatedAtAction(nameof(GetManager), new { id = createdManager.Id }, createdManager);
        }

        // دریافت اطلاعات یک مدیر
        [HttpGet("{id}")]
        public async Task<ActionResult<Manager>> GetManager(int id, CancellationToken ct)
        {
            var manager = await _managerService.GetByIdAsync(id, ct);
            if (manager == null)
            {
                return NotFound();
            }

            return Ok(manager);
        }

        // دریافت لیست تمام مدیران
        [HttpGet]
        public async Task<ActionResult<List<Manager>>> GetManagers(CancellationToken ct)
        {
            var managers = await _managerService.GetAllAsync(ct);
            return Ok(managers);
        }

        // به‌روزرسانی اطلاعات مدیر
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManager(int id, [FromBody] Manager updatedManager, CancellationToken ct)
        {
            await _managerService.UpdateAsync(id, updatedManager, ct);
            return NoContent();
        }

        // حذف مدیر
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(int id, CancellationToken ct)
        {
            await _managerService.DeleteAsync(id, ct);
            return NoContent();
        }

        // تبدیل مدیر به مدیر کل
        [HttpPut("{id}/convert-to-superadmin")]
        public async Task<IActionResult> ConvertToSuperAdmin(int id, CancellationToken ct)
        {
            // تبدیل مدیر به مدیر کل
            var superAdmin = await _managerService.ConvertToSuperAdminAsync(id, ct);
            if (superAdmin == null)
            {
                return BadRequest("Manager not found or already a SuperAdmin.");
            }

            // ثبت مدیر کل در جدول SuperAdmin
            await _superAdminRepository.AddAsync(superAdmin, ct);
            return Ok(superAdmin);
        }
    }
}