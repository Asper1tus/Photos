using System;

namespace Photos.API.DataTransferObjects
{
    public record CommentDTO
    {
        public string Text { get; init; }

        public int LikesCount { get; init; }

        public DateTime CreationDate { get; init; }
    }
}
