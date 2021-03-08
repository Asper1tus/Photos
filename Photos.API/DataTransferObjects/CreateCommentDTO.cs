using System.ComponentModel.DataAnnotations;

namespace Photos.API.DataTransferObjects
{
    public record CreateCommentDTO
    {
        [Required]
        public string Text { get; init; }
    }
}
