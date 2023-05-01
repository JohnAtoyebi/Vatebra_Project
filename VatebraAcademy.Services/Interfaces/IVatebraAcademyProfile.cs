using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VatebraAcademy.Core;
using VatebraAcademy.Core.Dtos;
using VatebraAcademy.Core.PaginationDto;

namespace VatebraAcademy.Services.Interfaces
{
    public interface IVatebraAcademyProfile
    {
        Task<string> CreateProfile(UserDto appUser);
        Task<string> UpdateProfileById(string Id, UserDto appUser);
        Task<PaginatedListDto<AppUserDto>> GetAllProfiles(int page, int perPage);
        Task<AppUserDto> GetProfileById(string Id);
        Task<string> DeleteProfileById(string Id);
        Task<string> GenerateToken(AppUser user);
        Task<LoginDto> Login(string Email, string Password);
    }
}
