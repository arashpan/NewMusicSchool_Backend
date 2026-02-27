using MusicSchool.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MusicSchool.Domain.Entities;

namespace MusicSchool.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // ایجاد کارمند
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee, CancellationToken ct)
        {
            var createdEmployee = await _employeeService.CreateAsync(employee, ct);
            return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Id }, createdEmployee);
        }

        // دریافت کارمند
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id, CancellationToken ct)
        {
            var employee = await _employeeService.GetByIdAsync(id, ct);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // دریافت لیست کارمندان
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees(CancellationToken ct)
        {
            var employees = await _employeeService.GetAllAsync(ct);
            return Ok(employees);
        }

        // به‌روزرسانی کارمند
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee updatedEmployee, CancellationToken ct)
        {
            await _employeeService.UpdateAsync(id, updatedEmployee, ct);
            return NoContent();
        }

        // حذف کارمند
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id, CancellationToken ct)
        {
            await _employeeService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}