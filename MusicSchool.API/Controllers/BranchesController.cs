using MusicSchool.Application.Abstractions.Services;
using MusicSchool.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MusicSchool.API.Controllers
{
    [Route("api/branches")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchesController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        // ایجاد شعبه
        [HttpPost]
        public async Task<ActionResult<Branch>> CreateBranch([FromBody] Branch branch, CancellationToken ct)
        {
            if (branch == null)
            {
                return BadRequest("Branch data is required.");
            }

            // اعتبارسنجی فیلدهای شعبه
            if (string.IsNullOrWhiteSpace(branch.Name) || string.IsNullOrWhiteSpace(branch.Location))
            {
                return BadRequest("Name and Location are required for the branch.");
            }

            // اگر شعبه به عنوان شعبه اصلی است، ParentBranchId باید null باشد
            if (branch.IsMainBranch && branch.ParentBranchId != null)
            {
                return BadRequest("A main branch cannot have a parent.");
            }

            // اگر ParentBranchId ارسال شده باشد، باید ParentBranch از دیتابیس بارگذاری شود
            if (branch.ParentBranchId.HasValue)
            {
                var parentBranch = await _branchService.GetByIdAsync(branch.ParentBranchId.Value, ct);
                if (parentBranch == null || parentBranch.IsMainBranch == false)
                {
                    return BadRequest("Invalid Parent Branch.");
                }
                branch.ParentBranch = parentBranch;  // پیوند دادن شعبه اصلی
            }

            var createdBranch = await _branchService.CreateAsync(branch, ct);
            return CreatedAtAction(nameof(GetBranch), new { id = createdBranch.Id }, createdBranch);
        }

        [HttpPut("{id}/convert-to-main")]
        public async Task<IActionResult> ConvertToMainBranch(int id, CancellationToken ct)
        {
            var branch = await _branchService.GetByIdAsync(id, ct);
            if (branch == null)
            {
                return NotFound();
            }

            // تغییر وضعیت شعبه به شعبه اصلی
            branch.IsMainBranch = true;
            await _branchService.UpdateAsync(id, branch, ct);

            return Ok(branch);
        }

        // دریافت شعبه
        [HttpGet("{id}")]
        public async Task<ActionResult<Branch>> GetBranch(int id, CancellationToken ct)
        {
            var branch = await _branchService.GetByIdAsync(id, ct);
            if (branch == null)
                return NotFound();

            return Ok(branch);
        }

        // دریافت لیست شعبه‌ها
        [HttpGet]
        public async Task<ActionResult<List<Branch>>> GetBranches(CancellationToken ct)
        {
            var branches = await _branchService.GetAllAsync(ct);
            return Ok(branches);
        }

        // به‌روزرسانی شعبه
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(int id, [FromBody] Branch updatedBranch, CancellationToken ct)
        {
            await _branchService.UpdateAsync(id, updatedBranch, ct);
            return NoContent();
        }

        // حذف شعبه
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id, CancellationToken ct)
        {
            await _branchService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}