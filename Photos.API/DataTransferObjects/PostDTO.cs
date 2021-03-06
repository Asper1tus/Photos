using System;

namespace Photos.API.DataTransferObjects
{
    public record PostDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreationDate { get; set; }

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }
    }
}
