using Microsoft.AspNetCore.Mvc;
using Photos.API.DataTransferObjects;
using Photos.Date.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace Photos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        readonly ApplicationContext dbContext;

        public PostsController(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<PostDTO>> GetPosts([FromQuery] int? skip, [FromQuery] int? quantity)
        {
            var query = dbContext.Posts.AsQueryable();

            if (skip != null)
                query.Skip(skip.Value);

            if (quantity != null)
                query.Take(quantity.Value);

            List<PostDTO> posts = new();

            foreach (var post in query)
            {
                PostDTO postDto = new()
                {
                    Id = post.Id,
                    Title = post.Title,
                    CreationDate = post.CreationDate,
                    Description = post.Description,
                    ImageUrl = post.ImageUrl,
                    LikesCount = post.LikesCount,
                    CommentsCount = post.Comments.Count
                };

                posts.Add(postDto);
            }

            return Ok(posts);
        }

        [HttpGet("{id}/comments")]
        public ActionResult<List<CommentDTO>> GetComments(int id, [FromQuery] int? skip, [FromQuery] int? quantity)
        {
            var query = dbContext.Comments.AsQueryable();

            query.Where(c => c.PostId == id);

            if (skip != null)
                query.Skip(skip.Value);

            if (quantity != null)
                query.Take(quantity.Value);

            List<CommentDTO> comments = new();

            foreach (var comment in query)
            {
                CommentDTO commentDto = new()
                {
                    CreationDate = comment.CreationDate,
                    LikesCount = comment.LikesCount,
                    Text = comment.Text
                };

                comments.Add(commentDto);
            }

            return Ok(comments);
        }
    }
}
