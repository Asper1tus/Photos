using System;
using System.ComponentModel.DataAnnotations;

namespace Photos.API.DataTransferObjects
{
    public class CommentDTO
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public int LikesCount { get; set; }


        [Required]
        public DateTime CreationDate { get; set; }
    }
}
