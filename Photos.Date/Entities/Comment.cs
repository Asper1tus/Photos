using System;

namespace Photos.Date.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int LikesCount { get; set; }
        public DateTime CreationDate { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}