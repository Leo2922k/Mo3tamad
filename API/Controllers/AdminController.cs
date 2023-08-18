using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Date;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Controllers
{

    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IExamRepository _examRepository;
        private readonly DataContext _context;
        public AdminController(UserManager<AppUser> userManager, IExamRepository examRepository, DataContext context) 
        {
            _context = context;
            _examRepository = examRepository;
            _userManager = userManager;
            
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet ("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles() {

            var users = await _userManager.Users
                        .OrderBy (u => u.UserName)
                        .Select(u => new {
                            u.Id,
                            Username = u.UserName,
                            Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                        })
                        .ToListAsync();
            
                return Ok(users);
      
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost ("edit-roles/{username}")]

        public async Task<ActionResult> EditRoles (string username, [FromQuery]string roles) {
            if (string.IsNullOrEmpty(roles)) return BadRequest("You must select at least one role");

            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) return BadRequest ("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) return BadRequest("Failed to remove from roles");

            return Ok (await _userManager.GetRolesAsync(user)); // post

        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet ("photos-to-moderate")]
        public ActionResult GetPhotosForModeration() {
            return Ok("Admins or moderators can see this");
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpDelete("deleteexam/{examid}")]
        public async Task<IActionResult> DeleteExam(int examId)
        {
            var ExamToDelete = await _examRepository.GetExamByIdAsync(examId);

            if (ExamToDelete == null)
            {
                return NotFound(); // If record not found, return 404
            }

            _examRepository.Delete(ExamToDelete);

            await _examRepository.SaveAllAsync();

            return NoContent(); // Successful deletion, return 204 No Content
        }


        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("add-exam")]
        public async Task<IActionResult> AddNewExamAsync()
        {
            try
            {
                using StreamReader reader = new(Request.Body);
                string requestBody = await reader.ReadToEndAsync();
                AppExams newExam = JsonConvert.DeserializeObject<AppExams>(requestBody);
                 _context.Exams.Add(newExam);
                 await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
       /* [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("add-exam")]
        public async Task<ActionResult> AddExam (IFormFile file) {


             var user = new AppExams {

             };


            if (await context.Exams.AnyAsync()) return;

            var examData = await File.ReadAllTextAsync("Data/ExamSeedData.json");

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

            var exams = JsonSerializer.Deserialize<List<AppExams>>(examData); // json -> c#

            foreach (var exam in exams) {
                
                context.Exams.Add(exam);
            }

            await context.SaveChangesAsync();

        }*/
        
    }
}