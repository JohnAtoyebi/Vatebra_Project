using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VatebraAcademy.Core;
using VatebraAcademy.Core.Dtos;
using VatebraAcademy.Services.Interfaces;

namespace VatebraAcademy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatebraAcademyProfileController : ControllerBase
    {
        private readonly IVatebraAcademyProfile _vatebra;
        public VatebraAcademyProfileController(IVatebraAcademyProfile vatebra)
        {
            _vatebra = vatebra;
        }

        [HttpGet("userprofiles")]
        public async Task<IActionResult> GetStudentProfiles()
        {
            return Ok(await _vatebra.GetAllProfiles());
        }

        [HttpGet("userprofile/{id}")]
        public async Task<IActionResult> GetStudentProfilesById([FromQuery]string Id)
        {
            return Ok(await _vatebra.GetProfileById(Id));
        }

        [HttpPost("userprofile")]
        public async Task<IActionResult> CreateStudentProfile(AppUserDto userProfile)
        {
            var getProfile = await _vatebra.CreateProfile(userProfile);
            return Ok(getProfile);
        }

        [HttpDelete("userprofile/{id}")]
        public async Task<IActionResult> DeleteStudentProfile([FromQuery]string userId)
        {
            return Ok(await _vatebra.DeleteProfileById(userId));
        }

        [HttpPatch("userprofile")]
        public async Task<IActionResult> UpdateStudentProfile(string Id, AppUserDto userProfile)
        {
            return Ok(await _vatebra.UpdateProfileById(Id, userProfile));
        }
    }
}
