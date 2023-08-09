using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photo")]
    public class Photo
    {

        public int PhotoId { get; set; }

        public string PhotoUrl { get; set; }

        public string PhotoPublicId { get; set; }

        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}