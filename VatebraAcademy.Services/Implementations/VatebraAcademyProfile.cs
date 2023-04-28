using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VatebraAcademy.Core;
using VatebraAcademy.Core.Dtos;
using VatebraAcademy.Data;
using VatebraAcademy.Services.Interfaces;

namespace VatebraAcademy.Services.Implementations
{
    public class VatebraAcademyProfile : IVatebraAcademyProfile
    {
        private readonly VatebraAcademyDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public VatebraAcademyProfile(VatebraAcademyDbContext context,
                                     UserManager<AppUser> userManager,
                                     RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<string> CreateProfile(AppUserDto appUser)
        {
            try
            {

                if (await _roleManager.FindByNameAsync("AppUser") == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole("AppUser"));
                }

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

        public async Task<string> DeleteProfileById(string Id)
        {
            var searchForUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == Id);
            if (searchForUser == null) return $"User with {Id} doesn't exist.";
            _context.AppUsers.Remove(searchForUser);
            await _context.SaveChangesAsync();
            return "User's profile successfully deleted.";
        }

        public async Task<List<AppUser>> GetAllProfiles()
        {
            var appUser = new List<AppUser>();
            var searchForUser = await _context.AppUsers.ToListAsync();
            if (searchForUser == null) return appUser;
            return searchForUser;
        }

        public async Task<AppUser> GetProfileById(string Id)
        {
            var appUser = new AppUser();
            var searchForUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == Id);
            if (searchForUser == null) return appUser;
            return searchForUser;
        }

        public async Task<string> UpdateProfileById(string Id, AppUserDto appUser)
        {
            var searchForUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == Id);
            if (searchForUser == null) return $"User with {Id} doesn't exist.";

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
    }
}
