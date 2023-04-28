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
        public VatebraAcademyProfile(VatebraAcademyDbContext context)
        {
            _context = context;
        }
        public string CreateProfile(AppUserDto appUser)
        {
            try
            {
                var createProfile = new AppUser
                {
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    OtherNames = appUser.OtherNames,
                    DOB = appUser.DOB,
                    PhoneNumber = appUser.PhoneNumber,
                    Age = appUser.Age,
                    Gender = appUser.Gender,
                    Address = appUser.Address,
                };

                _context.AppUsers.Add(createProfile);
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
