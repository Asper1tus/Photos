using System.ComponentModel.DataAnnotations;

namespace Photos.API.DataTransferObjects
{
    public record UpdatePostDTO
    {
        [Required]
        public int LikesCount { get; init; }
    }
}
