using System;

namespace Photos.API.DataTransferObjects
{
    public record PostDTO
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string Description { get; init; }

        public string ImageUrl { get; init; }

        public DateTime CreationDate { get; init; }

        public int LikesCount { get; init; }

        public int CommentsCount { get; init; }
    }
}
