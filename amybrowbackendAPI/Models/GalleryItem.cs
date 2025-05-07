using System.ComponentModel.DataAnnotations.Schema;

namespace amybrowbackendAPI.Models
{
    public class GalleryItem
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile? BeforeImage { get; set; }
        public string? BeforeImageUrl { get; set; }

        [NotMapped]
        public IFormFile? AfterImage { get; set; }
        public string? AfterImageUrl { get; set; }
    }
}
