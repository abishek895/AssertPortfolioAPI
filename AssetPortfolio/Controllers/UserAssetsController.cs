using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetPortfolio.Core.Models;
using AssetPortfolio.Infrastructure.Data;

namespace AssetPortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAssetsController : ControllerBase
    {
        private readonly AssetPortfolioDbContext _context;

        public UserAssetsController(AssetPortfolioDbContext context)
        {
            _context = context;
        }

        // GET: api/UserAssets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAsset>>> GetUserAssets()
        {
            return await _context.UserAssets.ToListAsync();
        }

        // GET: api/UserAssets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAsset>> GetUserAsset(int id)
        {
            var userAsset = await _context.UserAssets.FindAsync(id);

            if (userAsset == null)
            {
                return NotFound();
            }

            return userAsset;
        }

        // PUT: api/UserAssets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAsset(int id, UserAsset userAsset)
        {
            if (id != userAsset.Id)
            {
                return BadRequest();
            }

            _context.Entry(userAsset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAssetExists(id))
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

        // POST: api/UserAssets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAsset>> PostUserAsset(UserAsset userAsset)
        {
            _context.UserAssets.Add(userAsset);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAsset", new { id = userAsset.Id }, userAsset);
        }

        // DELETE: api/UserAssets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsset(int id)
        {
            var userAsset = await _context.UserAssets.FindAsync(id);
            if (userAsset == null)
            {
                return NotFound();
            }

            _context.UserAssets.Remove(userAsset);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAssetExists(int id)
        {
            return _context.UserAssets.Any(e => e.Id == id);
        }
    }
}
