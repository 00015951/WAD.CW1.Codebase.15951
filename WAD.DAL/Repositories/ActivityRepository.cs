using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAD.DAL.Data;
using WAD.DAL.Dtos;
using WAD.DAL.Interfaces;
using WAD.DAL.Models;

namespace WAD.DAL.Repositories
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        public ActivityRepository(AppDbContext context) : base(context) { }

        // Adds activity and decreases user weight
        public async Task AddActivityAsync(CreateActivityDTO createActivityDTO)
        {
            var user = await _context.Users.FindAsync(createActivityDTO.UserId);
            if (user == null)
                throw new ArgumentException("Invalid UserId");

            // Decrease weight
            user.Weight -= 0.5;
            if (user.Weight < 0) user.Weight = 0; // Ensure weight does not go below zero

            // Create and add activity
            var activity = new Activity
            {
                Type = createActivityDTO.Type,
                Date = createActivityDTO.Date,
                Duration = createActivityDTO.Duration,
                CaloriesBurned = createActivityDTO.CaloriesBurned,
                UserId = createActivityDTO.UserId
            };

            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Activity>> GetActivitiesByUserIdAsync(int userId)
        {
            return await _context.Activities
                                 .Where(a => a.UserId == userId)
                                 .ToListAsync();
        }
    }
}
