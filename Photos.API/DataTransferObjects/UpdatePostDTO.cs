using System.ComponentModel.DataAnnotations;

namespace Photos.API.DataTransferObjects
{
    public class UpdatePostDTO
    {
        [Required]
        public int LikesCount { get; set; }
    }
}
