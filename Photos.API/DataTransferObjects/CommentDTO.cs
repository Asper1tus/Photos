using System;

namespace Photos.API.DataTransferObjects
{
    public record CommentDTO
    {
        public string Text { get; set; }

        public int LikesCount { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
