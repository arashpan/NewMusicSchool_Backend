using System.Threading;
using System.Threading.Tasks;
using MusicSchool.Application.DTOs.Students;

namespace MusicSchool.Application.Abstractions.Services
{
    public interface IStudentRegistrationService
    {
        Task<StudentDto> RegisterAsync(RegisterStudentRequestDto request, CancellationToken ct);
    }
}