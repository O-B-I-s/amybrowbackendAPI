namespace amybrowbackendAPI.Models
{
    public class ServiceCreateInput
    {
        public string Title { get; set; }
        public string Subhead { get; set; }
        public IFormFile? Image { get; set; }
        public int[]? DescriptionIds { get; set; }
    }
}
