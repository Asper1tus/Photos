using Microsoft.AspNetCore.Mvc;
using Photos.API.DataTransferObjects;
using Photos.API.Extensions;
using Photos.Date.Entities;
using Photos.Date.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photos.API.Controllers
{
    [Route("api/posts/{id}/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        readonly ApplicationContext dbContext;
        public CommentsController(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<CommentDTO>> GetComments(int id, [FromQuery] int? skip, [FromQuery] int? quantity)
        {
            var post = dbContext.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
                return NotFound("Post not found.");

            var query = dbContext.Comments.AsQueryable()
                .Where(c => c.PostId == id)
                .SkipOrAll(skip)
                .TakeOrAll(quantity);

            var comments = new List<CommentDTO>();
            foreach (var comment in query)
            {
                var commentDto = new CommentDTO()
                {
                    CreationDate = comment.CreationDate,
                    Text = comment.Text
                };

                comments.Add(commentDto);
            }

            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(int id, [FromBody] CreateCommentDTO commentDTO)
        {
            var post = dbContext.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
                return NotFound("Post not found.");

            var comment = new Comment()
            {
                Text = commentDTO.Text,
                Post = post
            };

            await dbContext.Comments.AddAsync(comment);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
