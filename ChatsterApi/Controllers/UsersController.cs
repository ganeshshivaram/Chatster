using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ChatsterApi.Data;
using ChatsterApi.Dtos;
using ChatsterApi.Helpers;
using ChatsterApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatsterApi.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepo, IMapper mapper)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserParams userParams)
        {
            var currentLoggedInUserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            userParams.UserId = currentLoggedInUserId;

            var currentLoggedInUser = await _userRepo.GetUser(currentLoggedInUserId);

            if (string.IsNullOrWhiteSpace(userParams.Gender))
                userParams.Gender = currentLoggedInUser.Gender == "male" ? "female" : "male";

            var users = await _userRepo.GetAllUsers(userParams);

            var usersForListing = _mapper.Map<IEnumerable<UserForListingDto>>(users);
            Response.AddPaginationHeaders(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(usersForListing);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepo.GetUser(id);
            var userDetailDto = _mapper.Map<UserForDetailedDto>(user);
            return Ok(userDetailDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, UserForUpdateDto userDto)
        {
            if (id != Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _userRepo.GetUser(id);
            if (user != null)
            {
                _mapper.Map(userDto, user);
                if (await _userRepo.SaveAll())
                    return NoContent();
            }
            throw new Exception($"Updating user with {id} failed");
        }

        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> SendLike(int id, int recipientId)
        {

            if (id != Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var recipientUser = await _userRepo.GetUser(recipientId);
            if (recipientUser == null)
            {
                return NotFound();
            }

            var like = await _userRepo.GetLike(id, recipientId);

            if (like != null)
                return BadRequest("User has already been liked");

            like = new Like() { LikerId = id, LikeeId = recipientId };
            _userRepo.Add<Like>(like);

            return Ok();
        }
    }
}