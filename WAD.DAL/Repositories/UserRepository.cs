using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAD.DAL.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System.Text;
    using WAD.DAL.Data;
    using WAD.DAL.Dtos;
    using WAD.DAL.Interfaces;
    using WAD.DAL.Models;

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task AddUserAsync(CreateUserDTO createUserDTO)
        {
            var user = new User
            {
                FullName = createUserDTO.FullName,
                Email = createUserDTO.Email,
                PasswordHash = EncodePasswordToBase64(createUserDTO.PasswordHash),
                DateOfBirth = createUserDTO.DateOfBirth,
                Weight = createUserDTO.Weight,
                Height = createUserDTO.Height
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserWithActivitiesAsync(int userId)
        {
            return await _context.Users
                                 .Include(u => u.Activities)
                                 .FirstOrDefaultAsync(u => u.Id == userId);
        }

        private string EncodePasswordToBase64(string plainPassword)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainPassword);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
