namespace API.DTO
{
    public class MemberDto
    {
         public int Id { get; set; }

        public string UserName { get; set; }

        public DateOnly DateOfBirth { get; set;}

        public string Gender { get; set;}

        public string ProfilePictureUrl { get; set; }

        public PhotoDto ProfilePicture { get; set;}

        public string FullName { get; set; }

        public ICollection<UserExamDto> UserExams { get; set; }
    }
}