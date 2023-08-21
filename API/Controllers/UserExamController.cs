// UserExamController: GetUsersExams / GetUserExam / AddUserExamAttemptAsync / AddUserExamMoreAttemptsAsync /DeleteUserExam

using API.Date;
using API.DTO;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UserExamController : BaseApiController 
    {
        

        private readonly IUserExamRepository _userexamRepository;

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserExamController(DataContext context, IUserExamRepository userexamRepository, IMapper mapper)
        {
            _context = context;
            _userexamRepository = userexamRepository;
            _mapper = mapper;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserExam>>> GetUsersExams()
        {

            var userexams = await _userexamRepository.GetUsersExamsAsync();

            return Ok(userexams);

        }

        [HttpGet("{twoids}")]
        public async Task<ActionResult<UserExamDto>> GetUserExam(int id, int examid)
        {
            return await _userexamRepository.GetUserExamDtoAsync(id, examid);

        }

        /**/
        [HttpPost("add-attempt")]
    public async Task<ActionResult> AddUserExamAttemptAsync(UserExam userExamAttempt)
    {
        var userExam = new UserExam
        {
            Id = userExamAttempt.Id,
            ExamId = userExamAttempt.ExamId,
            UserExamScreenVideoUrl = userExamAttempt.UserExamScreenVideoUrl,
            UserExamCamVideoUrl = userExamAttempt.UserExamCamVideoUrl,
            UserExamGrade = userExamAttempt.UserExamGrade
        };

        // Add the new UserExam entity to the context and save changes asynchronously
        _context.UserExam.Add(userExam);
        try
        {
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest("Error occurred while saving exam attempt.");
        }

        }

        [HttpPut]
        public async Task <ActionResult> AddUserExamMoreAttemptsAsync(UserExam userExamAttempt) {
       
            var userexamnew =await _context.UserExam.FindAsync(userExamAttempt.Id, userExamAttempt.ExamId);

            if (userexamnew != null) {
                userexamnew.UserExamScreenVideoUrl = userExamAttempt.UserExamScreenVideoUrl;
                userexamnew.UserExamCamVideoUrl = userExamAttempt.UserExamCamVideoUrl;
                userexamnew.UserExamGrade = userExamAttempt.UserExamGrade;

                await _context.SaveChangesAsync();

                return Ok();
            } else {
                return Ok("There is no previous attempt.");
            }


        }

        [HttpDelete("{id}/{examId}")]
        public async Task<IActionResult> DeleteUserExam(int id, int examId)
        {
            var userExamToDelete = await _userexamRepository.GetUserExamByIdsAsync(id, examId);

            if (userExamToDelete == null)
            {
                return NotFound(); // If record not found, return 404
            }

            _userexamRepository.Delete(userExamToDelete);

            await _userexamRepository.SaveAllAsync();

            return NoContent(); // Successful deletion, return 204 No Content
        }
    }
}