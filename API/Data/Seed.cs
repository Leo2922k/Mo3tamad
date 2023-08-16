using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Date
{
    public class Seed
    {

        public static async Task SeedUsers (UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) {


            if (await userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData); // json -> c#

            var roles = new List<AppRole> {
                new AppRole {Name = "Member"},
                new AppRole {Name = "Admin"},
                new AppRole {Name = "Moderator"},
            };

            foreach (var role in roles) {
                await roleManager.CreateAsync(role);
            }
            
            foreach (var user in users) {

                user.UserName = user.UserName.ToLower();

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");
            }
            var admin = new AppUser {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new [] {"Admin", "Moderator"});

           // await context.SaveChangesAsync();

        }

        public static async Task SeedExams (DataContext context) {


            if (await context.Exams.AnyAsync()) return;

            var examData = await File.ReadAllTextAsync("Data/ExamSeedData.json");

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

            var exams = JsonSerializer.Deserialize<List<AppExams>>(examData); // json -> c#

            foreach (var exam in exams) {
                
                context.Exams.Add(exam);
            }

            await context.SaveChangesAsync();

        }

        public static async Task SeedUserExams (DataContext context) {


            if (await context.UserExam.AnyAsync()) return;

            var userexamData = await File.ReadAllTextAsync("Data/UserExamSeedData.json");

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

            var userexams = JsonSerializer.Deserialize<List<UserExam>>(userexamData); // json -> c#

            foreach (var userexam in userexams) {
                
                context.UserExam.Add(userexam);
            }

            await context.SaveChangesAsync();

        }
        
    }
}