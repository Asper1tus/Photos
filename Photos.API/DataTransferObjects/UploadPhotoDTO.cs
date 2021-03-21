using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Photos.API.DataTransferObjects
{
    public class UploadPhotoDTO
    {
        public IFormFile Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
