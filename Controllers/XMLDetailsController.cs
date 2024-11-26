using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _4LumenBackEnd.Models;

namespace _4LumenBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XMLDetailsController : ControllerBase
    {
        private readonly XmlDetailsContext _context;

        public XMLDetailsController(XmlDetailsContext context)
        {
            _context = context;
        }

        // GET: api/XMLDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<XMLDetails>>> GetXMLDetails()
        {
            return await _context.XMLDetails.ToListAsync();
        }

        // GET: api/XMLDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<XMLDetails>> GetXMLDetails(int id)
        {
            var xMLDetails = await _context.XMLDetails.FindAsync(id);

            if (xMLDetails == null)
            {
                return NotFound();
            }

            return xMLDetails;
        }

        // PUT: api/XMLDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutXMLDetails(int id, XMLDetails xMLDetails)
        {
            if (id != xMLDetails.XmlId)
            {
                return BadRequest();
            }

            _context.Entry(xMLDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!XMLDetailsExists(id))
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

        // POST: api/XMLDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<XMLDetails>> PostXMLDetails(XMLDetails xMLDetails)
        {
            _context.XMLDetails.Add(xMLDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetXMLDetails", new { id = xMLDetails.XmlId }, xMLDetails);
        }

        // DELETE: api/XMLDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteXMLDetails(int id)
        {
            var xMLDetails = await _context.XMLDetails.FindAsync(id);
            if (xMLDetails == null)
            {
                return NotFound();
            }

            _context.XMLDetails.Remove(xMLDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool XMLDetailsExists(int id)
        {
            return _context.XMLDetails.Any(e => e.XmlId == id);
        }
    }
}
