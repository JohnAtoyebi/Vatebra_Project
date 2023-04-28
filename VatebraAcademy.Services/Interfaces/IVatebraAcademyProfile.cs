using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VatebraAcademy.Core;
using VatebraAcademy.Core.Dtos;

namespace VatebraAcademy.Services.Interfaces
{
    public interface IVatebraAcademyProfile
    {
        string CreateProfile(AppUserDto appUser);
        Task<string> UpdateProfileById(string Id, AppUserDto appUser);
        Task<List<AppUser>> GetAllProfiles();
        Task<AppUser> GetProfileById(string Id);
        Task<string> DeleteProfileById(string Id);
    }
}
