using System;
using System.ComponentModel.DataAnnotations;

namespace Photos.API.DataTransferObjects
{
    public record CommentDTO
    {
        [Required]
        public string Text { get; init; }

        [Required]
        public int LikesCount { get; init; }


        [Required]
        public DateTime CreationDate { get; init; }
    }
}
