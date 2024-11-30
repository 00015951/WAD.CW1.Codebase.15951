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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<UserDTO>>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            var userDTOs = _mapper.Map<List<UserDTO>>(users);

            return Ok(new BaseResponse<IEnumerable<UserDTO>>(true, "Users retrieved successfully", userDTOs));
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<UserDTO>>> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new BaseResponse<UserDTO>(false, "User not found", null));
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(new BaseResponse<UserDTO>(true, "User retrieved successfully", userDTO));
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<BaseResponse<UserDTO>>> CreateUser(CreateUserDTO createUserDTO)
        {
            var user = _mapper.Map<User>(createUserDTO);
            await _userRepository.AddAsync(user);

            var userDTO = _mapper.Map<UserDTO>(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id },
                new BaseResponse<UserDTO>(true, "User created successfully", userDTO));
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<UserDTO>>> UpdateUser(int id, EditUserDTO editUserDTO)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new BaseResponse<UserDTO>(false, "User not found", null));
            }

            _mapper.Map(editUserDTO, user);
            await _userRepository.UpdateAsync(user);

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(new BaseResponse<UserDTO>(true, "User updated successfully", userDTO));
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<string>>> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new BaseResponse<string>(false, "User not found", null));
            }

            await _userRepository.DeleteAsync(id);
            return Ok(new BaseResponse<string>(true, "User deleted successfully", null));
        }
    }
}
