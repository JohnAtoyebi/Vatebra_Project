using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VatebraAcademy.Core;
using VatebraAcademy.Core.Dtos;
using VatebraAcademy.Services.Interfaces;

namespace VatebraAcademy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VatebraAcademyProfileController : ControllerBase
    {
        private readonly IVatebraAcademyProfile _vatebra;
        public VatebraAcademyProfileController(IVatebraAcademyProfile vatebra)
        {
            _vatebra = vatebra;
        }

        [HttpPost("register-profile")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateStudentProfile(AppUserDto userProfile)
        {
            var getProfile = await _vatebra.CreateProfile(userProfile);
            return Ok(getProfile);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(string Email, string Password)
        {
            var getProfile = await _vatebra.Login(Email, Password);
            return Ok(getProfile);
        }

        [HttpGet("user-profiles")]
        public async Task<IActionResult> GetStudentProfiles()
        {
            return Ok(await _vatebra.GetAllProfiles());
        }

        [HttpGet("user-profile/{Id}")]
        public async Task<IActionResult> GetStudentProfilesById([FromRoute]string Id)
        {
            var getUser = await _vatebra.GetProfileById(Id);
            return Ok(getUser);
        }

        [HttpDelete("user-profile/{Id}")]
        public async Task<IActionResult> DeleteStudentProfile([FromRoute]string Id)
        {
            return Ok(await _vatebra.DeleteProfileById(Id));
        }

        [HttpPatch("user-profile/{Id}")]
        public async Task<IActionResult> UpdateStudentProfile([FromRoute]string Id, UserDto userProfile)
        {
            return Ok(await _vatebra.UpdateProfileById(Id, userProfile));
        }
    }
}
