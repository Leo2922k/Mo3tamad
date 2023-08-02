namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateOnly DateOfBirth { get; set;}

        public string Gender { get; set;}

        public string FullName { get; set; }

        public Photo ProfilePicture { get; set;}

        public ICollection<UserExam> UserExams { get; set;}

    }
}