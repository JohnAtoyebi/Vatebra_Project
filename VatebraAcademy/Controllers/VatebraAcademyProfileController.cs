using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VatebraAcademy.Core;
using VatebraAcademy.Core.Dtos;
using VatebraAcademy.Core.PaginationDto;
using VatebraAcademy.Core.Response;
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
        public async Task<IActionResult> CreateStudentProfile(UserDto userProfile)
        {
            var getProfile = await _vatebra.CreateProfile(userProfile);
            if(getProfile == null)
                return BadRequest(Utilities.BuildResponse<object>(true, "creating student profile was unsuccessful... email or phone number already exists", ModelState, ""));
            return Ok(Utilities.BuildResponse<object>(true, "successfully created student profile", ModelState, getProfile));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(string Email, string Password)
        {
            var getProfile = await _vatebra.Login(Email, Password);
            if (getProfile == null)
                return BadRequest(Utilities.BuildResponse<object>(true, "error logging in... make sure you inputted a correct email or password", ModelState, ""));
            return Ok(Utilities.BuildResponse<object>(true, "successfully logged into your student profile", ModelState, getProfile));
        }

        [HttpGet("user-profiles")]
        public async Task<IActionResult> GetStudentProfiles([FromQuery]PageCount pageCount)
        {
            var getProfile = await _vatebra.GetAllProfiles(pageCount.Page, pageCount.PerPage);
            if(getProfile == null)
                return BadRequest(Utilities.BuildResponse<object>(true, "getting students profile wasn't successful", ModelState, ""));
            return Ok(Utilities.BuildResponse<object>(true, "successfully got students profile", ModelState, getProfile));
        }

        [HttpGet("user-profile/{Id}")]
        public async Task<IActionResult> GetStudentProfilesById([FromRoute]string Id)
        {
            var getUser = await _vatebra.GetProfileById(Id);
            if(getUser == null)
                return BadRequest(Utilities.BuildResponse<object>(true, $"getting student profile wasn't successful... {Id} doesn't exist", ModelState, ""));
            return Ok(Utilities.BuildResponse<object>(true, "successfully got student profile", ModelState, getUser));
        }

        [HttpPatch("user-profile/{Id}")]
        public async Task<IActionResult> UpdateStudentProfile([FromRoute] string Id, UserDto userProfile)
        {
            var getUser = await _vatebra.UpdateProfileById(Id, userProfile);
            if (getUser == null)
                return BadRequest(Utilities.BuildResponse<object>(true, $"updating student profile wasn't successful...  User with {Id} doesn't exist", ModelState, ""));
            return Ok(Utilities.BuildResponse<object>(true, "successfully updated student profile", ModelState, getUser));
        }

        [HttpDelete("user-profile/{Id}")]
        public async Task<IActionResult> DeleteStudentProfile([FromRoute]string Id)
        {
            var deleteUser = await _vatebra.DeleteProfileById(Id);
            if (deleteUser == null)
                return BadRequest(Utilities.BuildResponse<object>(true, $"deleting student profile wasn't successful... User with {Id} doesn't exist", ModelState, ""));
            return Ok(Utilities.BuildResponse<object>(true, "successfully deleted student profile", ModelState, ""));
        }
    }
}
