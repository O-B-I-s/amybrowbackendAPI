using amybrowbackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace amybrowbackendAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceDescription> ServiceDescriptions { get; set; }
        public DbSet<GalleryItem> GalleryItems { get; set; }
    }
}
