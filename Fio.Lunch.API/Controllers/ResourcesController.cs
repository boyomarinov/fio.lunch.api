using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fio.Lunch.API.Models;
using System.IO;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Fio.Lunch.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Resources")]
    public class ResourcesController : Controller
    {
        private readonly FioLunchAPIContext _context;

        public ResourcesController(FioLunchAPIContext context)
        {
            _context = context;
        }

        // GET: api/Resources
        [HttpGet]
        public IEnumerable<Resource> GetResource()
        {
            return _context.Resource;
        }

        // GET: api/Resources/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetResource([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resource = await _context.Resource.SingleOrDefaultAsync(m => m.Id == id);

            if (resource == null)
            {
                return NotFound();
            }

            return Ok(resource);
        }

        // PUT: api/Resources/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResource([FromRoute] int id, [FromBody] Resource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resource.Id)
            {
                return BadRequest();
            }

            _context.Entry(resource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(id))
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

        // POST: api/Resources
        [HttpPost]
        public async Task<IActionResult> PostResource([FromBody] Resource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Resource.Add(resource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResource", new { id = resource.Id }, resource);
        }

        // POST: api/Resources
        [HttpPost, DisableRequestSizeLimit]
        [Route("/api/v1/resources/files")]
        public IActionResult UploadMenu()//List<IFormFile> files)
        {
            var file = Request.Form.Files[0];
            //throw new NotImplementedException("TODO: Implement file upload and read from spread sheet");
            ////long size = files.Sum(f => f.Length);

            ////// full path to file in temp location
            ////////var filePath = Path.GetTempFileName();

            EntityEntry<Resource> res = null;
            ////foreach (var formFile in files)
            ////{
            var resource = new Resource();
            if (file.Length > 0)
            {
                resource.Data = new byte[file.Length];
                using (var stream = new MemoryStream(resource.Data))
                {

                    file.CopyTo(stream);
                }
            }
            res = _context.Resource.Add(resource);


            _context.SaveChanges();
            return Ok($"/api/v1/resources/{res.Entity.Id}");
        }

        [HttpGet]
        [Route("/api/v1/resources/{id}/image")]
        public async Task<IActionResult> GetImage(int id)
        {
            var img = _context.Resource.Where(r => r.Id == id).SingleOrDefault();
            var image = new MemoryStream(img.Data);
            return File(image, "image/jpeg");
        }

        // DELETE: api/Resources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resource = await _context.Resource.SingleOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return NotFound();
            }

            _context.Resource.Remove(resource);
            await _context.SaveChangesAsync();

            return Ok(resource);
        }

        private bool ResourceExists(int id)
        {
            return _context.Resource.Any(e => e.Id == id);
        }
    }
}