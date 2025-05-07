using amybrowbackendAPI.Models;
using amybrowbackendAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace amybrowbackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DescriptionsController : ControllerBase
    {
        private readonly IDescriptionRepository _descRepo;

        public DescriptionsController(IDescriptionRepository descRepo)
        {
            _descRepo = descRepo;
        }

        // GET: api/descriptions
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _descRepo.GetAllAsync());

        // GET: api/descriptions/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var d = await _descRepo.GetByIdAsync(id);
            if (d == null) return NotFound();
            return Ok(d);
        }

        // POST: api/descriptions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceDescription dto)
        {
            await _descRepo.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        // PUT: api/descriptions/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServiceDescription dto)
        {
            if (id != dto.Id) return BadRequest();
            var exists = await _descRepo.GetByIdAsync(id);
            if (exists == null) return NotFound();

            exists.Title = dto.Title;
            exists.Item = dto.Item;
            await _descRepo.UpdateAsync(exists);
            return NoContent();
        }

        // DELETE: api/descriptions/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _descRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
