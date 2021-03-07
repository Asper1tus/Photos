using Microsoft.AspNetCore.Mvc;
using Photos.API.DataTransferObjects;
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
                    Text = comment.Text
                };

                comments.Add(commentDto);
            }

            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult> AddCommentAsync(int id, [FromBody] CreateCommentDTO commentDTO)
        {
            var post = dbContext.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
                return NotFound("Post not found.");

            Comment comment = new()
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
