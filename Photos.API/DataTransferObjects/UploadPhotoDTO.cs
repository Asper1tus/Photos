using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Photos.API.DataTransferObjects
{
    public record UploadPhotoDTO
    {
        [Required]
        public IFormFile Image { get; init; }

        [Required]
        public string Title { get; init; }

        [Required]
        public string Description { get; init; }
    }
}
