using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VatebraAcademy.Core;
using VatebraAcademy.Core.Dtos;
using VatebraAcademy.Core.PaginationDto;
using VatebraAcademy.Data;
using VatebraAcademy.Services.Interfaces;
using static VatebraAcademy.Core.PaginationDto.Pagination;

namespace VatebraAcademy.Services.Implementations
{
    public class VatebraAcademyProfile : IVatebraAcademyProfile
    {
        private readonly VatebraAcademyDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtConfig _jwtConfig;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        public VatebraAcademyProfile(VatebraAcademyDbContext context,
                                     UserManager<AppUser> userManager,
                                     RoleManager<IdentityRole> roleManager,
                                     IOptions<JwtConfig> options,
                                     SignInManager<AppUser> signInManager,
                                     IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _jwtConfig = options.Value;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task<string> CreateProfile(UserDto appUser)
        {
            try
            {
                var getEmail = await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == appUser.Email);
                if (getEmail != null) return null;

                if (await _roleManager.FindByNameAsync("AppUser") == null)
                    await _roleManager.CreateAsync(new IdentityRole("AppUser"));

                var createProfile = new AppUser
                {
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    OtherNames = appUser.OtherNames,
                    DOB = appUser.DOB,
                    Email = appUser.Email,
                    PhoneNumber = appUser.PhoneNumber,
                    Age = appUser.Age,
                    Gender = appUser.Gender,
                    Address = appUser.Address,
                    UserName = appUser.Email,
                    Role = "AppUser"
                };
                var token = await GenerateToken(createProfile);
                IdentityResult identityResult = await _userManager.CreateAsync(createProfile, appUser.Password);
                await _userManager.AddToRoleAsync(createProfile, "AppUser");
                _context.SaveChanges();
                return "Successfully Created Profile";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<LoginDto> Login(string Email, string Password)
        {
            try
            {
                var searchEmail = await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == Email);
                if (searchEmail == null) return null;
                var signingIn = await _signInManager.PasswordSignInAsync(searchEmail, Password, true, false);
                if (!signingIn.Succeeded) return null;
                var token = await GenerateToken(searchEmail);
                var loginDto = new LoginDto
                {
                    Token = token,
                    UserId = searchEmail.Id,
                };
                return loginDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteProfileById(string Id)
        {
            var searchForUser = await _userManager.FindByIdAsync(Id);
            if (searchForUser == null) return null;
            _context.AppUsers.Remove(searchForUser);
            await _context.SaveChangesAsync();
            return "User's profile successfully deleted.";
        }

        public async Task<PaginatedListDto<AppUserDto>> GetAllProfiles(int page, int perPage)
        {
            var searchForUser = await _context.AppUsers.ToListAsync();
            if (searchForUser == null) return null;
            var userToReturn = _mapper.Map<List<AppUserDto>>(searchForUser);
            var user = Pagnation<AppUserDto>.Paginate(userToReturn, page, perPage);
            return user;
        }

        public async Task<AppUserDto> GetProfileById(string Id)
        {
            var searchForUser = await _userManager.FindByIdAsync(Id);
            if (searchForUser == null) return null;
            var userToReturn = _mapper.Map<AppUserDto>(searchForUser);
            return userToReturn;
        }

        public async Task<string> UpdateProfileById(string Id, UserDto appUser)
        {
            var searchForUser = await _userManager.FindByIdAsync(Id);
            if (searchForUser == null) return null;

            searchForUser.FirstName = appUser.FirstName;
            searchForUser.LastName = appUser.LastName;
            searchForUser.OtherNames = appUser.OtherNames;
            searchForUser.DOB = appUser.DOB;
            searchForUser.PhoneNumber = appUser.PhoneNumber;
            searchForUser.Age = appUser.Age;
            searchForUser.Gender = appUser.Gender;
            searchForUser.Address = appUser.Address;

            _context.AppUsers.Update(searchForUser);
            _context.SaveChanges();
            return "Successfully updated user's profile";
        }

        public async Task<string> GenerateToken(AppUser user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            foreach (string role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
