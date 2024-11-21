using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageItemsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ImageItemsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/ImageItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageItem>>> GetImages()
        {
            return await _context.Images.ToListAsync();
        }

        // GET: api/ImageItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageItem>> GetImageItem(Guid id)
        {
            var imageItem = await _context.Images.FindAsync(id);

            if (imageItem == null)
            {
                return NotFound();
            }

            return imageItem;
        }

        // PUT: api/ImageItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImageItem(Guid id, ImageItem imageItem)
        {
            if (id != imageItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(imageItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ImageItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImageItem>> PostImageItem(ImageItem imageItem)
        {
            _context.Images.Add(imageItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImageItem), new { id = imageItem.Id }, imageItem);
        }

        // DELETE: api/ImageItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageItem(Guid id)
        {
            var imageItem = await _context.Images.FindAsync(id);
            if (imageItem == null)
            {
                return NotFound();
            }

            _context.Images.Remove(imageItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageItemExists(Guid id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
    }
}
