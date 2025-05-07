using amybrowbackendAPI.Models;
using amybrowbackendAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace amybrowbackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _svcRepo;
        private readonly IDescriptionRepository _descRepo;
        private readonly IWebHostEnvironment _env;

        public ServicesController(
            IServiceRepository svcRepo,
            IDescriptionRepository descRepo,
            IWebHostEnvironment env)
        {
            _svcRepo = svcRepo;
            _descRepo = descRepo;
            _env = env;
        }

        // GET: api/services
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var all = await _svcRepo.GetAllAsync();
            return Ok(all);
        }

        // GET: api/services/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var svc = await _svcRepo.GetByIdAsync(id);
            if (svc == null) return NotFound();
            return Ok(svc);
        }

        // POST: api/services
        [HttpPost, Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ServiceCreateInput input)
        {
            //Saving image to wwwroot/uploads
            var root = _env.WebRootPath ?? _env.ContentRootPath;
            var upload = Path.Combine(root, "uploads");
            Directory.CreateDirectory(upload);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(input.Image.FileName)}";
            var filePath = Path.Combine(upload, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
                await input.Image.CopyToAsync(stream);

            // Fetching descriptions
            var descriptions = new List<ServiceDescription>();
            if (input.DescriptionIds != null)
            {
                foreach (var descId in input.DescriptionIds ?? Array.Empty<int>())
                {
                    var d = await _descRepo.GetByIdAsync(descId);
                    if (d != null) descriptions.Add(d);
                }
            }


            // Building service
            var service = new Service
            {
                Title = input.Title,
                Subhead = input.Subhead,
                ImageUrl = $"/uploads/{fileName}",
                Descriptions = descriptions
            };


            await _svcRepo.AddAsync(service);
            return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
        }

        // PUT: api/services/5
        [HttpPut("{id:int}"), Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] ServiceCreateInput input)
        {
            var existing = await _svcRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // Optional image replacement
            if (input.Image != null)
            {
                var root = _env.WebRootPath ?? _env.ContentRootPath;
                var upload = Path.Combine(root, "uploads");
                Directory.CreateDirectory(upload);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(input.Image.FileName)}";
                var filePath = Path.Combine(upload, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await input.Image.CopyToAsync(stream);

                existing.ImageUrl = $"/uploads/{fileName}";
            }

            // Updating text fields
            existing.Title = input.Title;
            existing.Subhead = input.Subhead;

            // Re-link descriptions
            existing.Descriptions.Clear();
            foreach (var descId in input.DescriptionIds ?? Array.Empty<int>())
            {
                var d = await _descRepo.GetByIdAsync(descId);
                if (d != null) existing.Descriptions.Add(d);
            }

            await _svcRepo.UpdateAsync(existing);
            return NoContent();
        }

        // DELETE: api/services/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _svcRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
