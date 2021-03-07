using Microsoft.AspNetCore.Mvc;
using Photos.API.DataTransferObjects;
using Photos.Date.CloudStorage;
using Photos.Date.Entities;
using Photos.Date.EntityFramework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Photos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        readonly ApplicationContext dbContext;
        readonly ICloudStorage cloudStorage;

        public PostsController(ApplicationContext dbContext, ICloudStorage cloudStorage)
        {
            this.dbContext = dbContext;
            this.cloudStorage = cloudStorage;
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

        [HttpPost]
        public async Task<ActionResult<PostDTO>> UploadPhotoAsync([FromForm] UploadPhotoDTO photo)
        {
            using var memoryStream = new MemoryStream();
            var image = photo.Image;

            await image.CopyToAsync(memoryStream);
            var imageUrl = await cloudStorage.UploadFileAsync(memoryStream, image.FileName);

            Post post = new()
            {
                Description = photo.Description,
                Title = photo.Title,
                ImageUrl = imageUrl
            };

            await dbContext.Posts.AddAsync(post);

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
            
            await dbContext.SaveChangesAsync();

            return Ok(postDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost(int id, [FromBody] UpdatePostDTO postDto)
        {
            var post = dbContext.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
                return NotFound("Post not found.");

            post.LikesCount = postDto.LikesCount;

            dbContext.Update(post);

            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
