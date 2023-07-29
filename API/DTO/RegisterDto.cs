using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDto
    {  


        [Required]
        public string Username { get; set; } 

        [Required]
        [StringLength(8, MinimumLength = 4)]
        // [String] [Phone] [Max] and other validations
        public string Password { get; set; }
        
    }
}