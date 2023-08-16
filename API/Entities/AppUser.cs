using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        /*
        public int Id { get; set; } // 16identity

        public string UserName { get; set; } // 16identity

        public byte[] PasswordHash { get; set; } // 16identity

        public byte[] PasswordSalt { get; set; } // 16identity
        */

        public DateOnly DateOfBirth { get; set;}

        public string Gender { get; set;}

        public string FullName { get; set; }

        public Photo ProfilePicture { get; set;}

        public ICollection<UserExam> UserExams { get; set;}

        public ICollection<AppUserRole> UserRoles { get; set; }

    }
}