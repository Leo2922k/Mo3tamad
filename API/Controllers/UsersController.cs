using System.Security.Claims;
using API.Date;
using API.DTO;
using API.Entities;
// UsersController: GetUsers - GetUser - UpdateUser - AddPhoto

using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController 
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService) 
        {
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet] //200 ok
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {

            var users = await _userRepository.GerMembersAsync();

            return Ok(users);

        }
        

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);

        }

        [HttpPut] // update // 204 no content
        public async Task <ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto) {

            var user = await _userRepository.GetUserByUsernameAsync (User.GetUsername());

            if (user == null) return NotFound();

            _mapper.Map(memberUpdateDto, user); // to update

            if (await _userRepository.SaveAllAsync()) return NoContent(); // all right

            return BadRequest("Failed to update user");

        }

        [HttpPost("add-photo")] // 201 created
        public async Task<ActionResult<PhotoDto>> AddPhoto (IFormFile file) {

            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo {
                PhotoUrl = result.SecureUrl.AbsoluteUri,
                PhotoPublicId = result.PublicId,

            };

            user.ProfilePicture = photo;

           // if (await _userRepository.SaveAllAsync()) return _mapper.Map<PhotoDto>(photo);

            if (await _userRepository.SaveAllAsync())  {

                return CreatedAtAction(
                     nameof(GetUser),
                     new {username = user.UserName},
                    _mapper.Map<PhotoDto>(photo)
                  );
            }

            return BadRequest("Problem adding photo");
        }

    }
}