using System.ComponentModel.DataAnnotations;

namespace Photos.API.DataTransferObjects
{
    public class CreateCommentDTO
    {
        [Required]
        public string Text { get; set; }
    }
}
