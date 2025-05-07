using System.ComponentModel.DataAnnotations.Schema;

namespace amybrowbackendAPI.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subhead { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }


        public string? ImageUrl { get; set; }
        public ICollection<ServiceDescription>? Descriptions { get; set; }
    }
}
