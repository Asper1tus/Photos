using System;
using System.Collections.Generic;

namespace Photos.Date.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreationDate { get; set; }
        public int LikesCount { get; set; }
        public List<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
            CreationDate = DateTime.Now;
        }
    }
}
