using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Date
{
    public class Seed
    {

        public static async Task SeedUsers (DataContext context) {


            if (await context.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData); // json -> c#

            foreach (var user in users) {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();

                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password"));
                user.PasswordSalt = hmac.Key;
                context.Users.Add(user);
            }

            await context.SaveChangesAsync();

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