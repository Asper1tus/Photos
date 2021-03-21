using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Photos.API.DataTransferObjects;
using Photos.API.Extensions;
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
            var query = dbContext.Posts.Include(p => p.Comments)
                .AsQueryable()
                .SkipOrAll(skip)
                .TakeOrAll(quantity);

            var posts = new List<PostDTO>();

            foreach (var post in query)
            {
                var postDto = new PostDTO()
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
        public async Task<ActionResult<PostDTO>> UploadPhoto([FromForm] UploadPhotoDTO photo)
        {
            string imageUrl;
            if (photo.Image != null)
            {
                using var memoryStream = new MemoryStream();
                var image = photo.Image;

                await image.CopyToAsync(memoryStream);
                imageUrl = await cloudStorage.UploadFileAsync(memoryStream, image.FileName);
            }
            else
            {
                imageUrl = "Url not found.";
            }

            var post = new Post()
            {
                Description = photo.Description,
                Title = photo.Title,
                ImageUrl = imageUrl
            };

            await dbContext.Posts.AddAsync(post);

            var postDto = new PostDTO()
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var post = dbContext.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
                return NotFound("Post not found.");

            dbContext.Posts.Remove(post);

            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}