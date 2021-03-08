using System;
using System.ComponentModel.DataAnnotations;

namespace Photos.API.DataTransferObjects
{
    public record PostDTO
    {
        [Required]

        public int Id { get; init; }

        [Required]
        public string Title { get; init; }

        [Required]
        public string Description { get; init; }

        [Required]
        public string ImageUrl { get; init; }

        [Required]
        public DateTime CreationDate { get; init; }

        [Required]
        public int LikesCount { get; init; }

        [Required]
        public int CommentsCount { get; init; }
    }
}
