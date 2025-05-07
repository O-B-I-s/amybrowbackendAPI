using amybrowbackendAPI.Models;
using amybrowbackendAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace amybrowbackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryRepository _galleryRepo;
        private readonly IWebHostEnvironment _env;

        public GalleryController(IGalleryRepository galleryRepo, IWebHostEnvironment env)
        {
            _galleryRepo = galleryRepo;
            _env = env;
        }

        // GET api/gallery
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _galleryRepo.GetAllAsync());

        // GET api/gallery/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _galleryRepo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST api/gallery
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] GalleryItem item)
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploads);

            // Before image
            if (item.BeforeImage != null)
            {
                var bName = $"{Guid.NewGuid()}{Path.GetExtension(item.BeforeImage.FileName)}";
                var bPath = Path.Combine(uploads, bName);
                using var bs = new FileStream(bPath, FileMode.Create);
                await item.BeforeImage.CopyToAsync(bs);
                item.BeforeImageUrl = $"/uploads/{bName}";
            }

            // After image
            if (item.AfterImage != null)
            {
                var aName = $"{Guid.NewGuid()}{Path.GetExtension(item.AfterImage.FileName)}";
                var aPath = Path.Combine(uploads, aName);
                using var asf = new FileStream(aPath, FileMode.Create);
                await item.AfterImage.CopyToAsync(asf);
                item.AfterImageUrl = $"/uploads/{aName}";
            }

            // clears IFormFile props before saving
            item.BeforeImage = null;
            item.AfterImage = null;

            await _galleryRepo.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        // PUT api/gallery/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromForm] GalleryItem updated)
        {
            var existing = await _galleryRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploads);

            if (updated.BeforeImage != null)
            {
                var name = $"{Guid.NewGuid()}{Path.GetExtension(updated.BeforeImage.FileName)}";
                var path = Path.Combine(uploads, name);
                using var s = new FileStream(path, FileMode.Create);
                await updated.BeforeImage.CopyToAsync(s);
                existing.BeforeImageUrl = $"/uploads/{name}";
            }

            if (updated.AfterImage != null)
            {
                var name = $"{Guid.NewGuid()}{Path.GetExtension(updated.AfterImage.FileName)}";
                var path = Path.Combine(uploads, name);
                using var s = new FileStream(path, FileMode.Create);
                await updated.AfterImage.CopyToAsync(s);
                existing.AfterImageUrl = $"/uploads/{name}";
            }

            await _galleryRepo.UpdateAsync(existing);
            return NoContent();
        }

        // DELETE api/gallery/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _galleryRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
