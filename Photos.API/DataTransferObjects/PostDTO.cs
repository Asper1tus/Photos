using System;
using System.ComponentModel.DataAnnotations;

namespace Photos.API.DataTransferObjects
{
    public class PostDTO
    {
        [Required]

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public int LikesCount { get; set; }

        [Required]
        public int CommentsCount { get; set; }
    }
}
