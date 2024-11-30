using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WAD.DAL.Dtos;
using WAD.DAL.Interfaces;
using WAD.DAL.Models;
using WAD.DAL.Responses;

namespace WAD.CW1.Codebase._15951.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public ActivityController(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }

        // GET: api/activities/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<BaseResponse<IEnumerable<ActivityDTO>>>> GetActivitiesByUserId(int userId)
        {
            var activities = await _activityRepository.GetActivitiesByUserIdAsync(userId);
            if (!activities.Any())
            {
                return NotFound(new BaseResponse<IEnumerable<ActivityDTO>>(false, "No activities found for this user", null));
            }

            var activityDTOs = _mapper.Map<List<ActivityDTO>>(activities);
            return Ok(new BaseResponse<IEnumerable<ActivityDTO>>(true, "Activities retrieved successfully", activityDTOs));
        }

        // POST: api/activities
        [HttpPost]
        public async Task<ActionResult<BaseResponse<ActivityDTO>>> CreateActivity(CreateActivityDTO createActivityDTO)
        {
            var activity = _mapper.Map<Activity>(createActivityDTO);
            await _activityRepository.AddAsync(activity);

            var activityDTO = _mapper.Map<ActivityDTO>(activity);
            return CreatedAtAction(nameof(GetActivitiesByUserId), new { userId = createActivityDTO.UserId },
                new BaseResponse<ActivityDTO>(true, "Activity created successfully", activityDTO));
        }

        // PUT: api/activities/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<ActivityDTO>>> UpdateActivity(int id, EditActivityDTO editActivityDTO)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity == null)
            {
                return NotFound(new BaseResponse<ActivityDTO>(false, "Activity not found", null));
            }

            _mapper.Map(editActivityDTO, activity);
            await _activityRepository.UpdateAsync(activity);

            var activityDTO = _mapper.Map<ActivityDTO>(activity);
            return Ok(new BaseResponse<ActivityDTO>(true, "Activity updated successfully", activityDTO));
        }

        // DELETE: api/activities/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<string>>> DeleteActivity(int id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity == null)
            {
                return NotFound(new BaseResponse<string>(false, "Activity not found", null));
            }

            await _activityRepository.DeleteAsync(id);
            return Ok(new BaseResponse<string>(true, "Activity deleted successfully", null));
        }
    }
}
