using AssetPortfolio.Application.HelperModel;
using AssetPortfolio.Application.Queries.AssetTypes;
using AssetPortfolio.Core.Models;
using AssetPortfolio.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Serilog;

namespace AssetPortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetTypesController : ControllerBase
    {
        private readonly AssetPortfolioDbContext _context;
        private readonly IMediator _mediator;

        public AssetTypesController(AssetPortfolioDbContext context,IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // GET: api/AssetTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetTypeHelperModel>>> GetAssetTypes()
        {
			Log.Information("GET /api/AssetTypes called at {Time}", DateTime.UtcNow);
            var assetTypes = await _mediator.Send(new GetAllAssetTypeQuery());
			Log.Information("{Count} assettypes retrieved successfully", assetTypes.Count());
			return Ok(assetTypes);
        }

        // GET: api/AssetTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetTypeHelperModel>> GetAssetType(int id)
        {
			Log.Information("GET /api/AssetTypes/{Id} called at {Time}", id, DateTime.UtcNow);
            var assetType = await _mediator.Send(new GetAssetTypeByIdQuery(id));
			if (assetType != null)
				Log.Information("AssetType with ID {Id} retrieved successfully", id);
			else
				Log.Warning("AssetType with ID {Id} not found", id);
			return Ok(assetType);
		}

        // PUT: api/AssetTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssetType(int id, AssetType assetType)
        {
            if (id != assetType.Id)
            {
                return BadRequest();
            }

            _context.Entry(assetType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetTypeExists(id))
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

        // POST: api/AssetTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AssetType>> PostAssetType(AssetType assetType)
        {
            _context.AssetTypes.Add(assetType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssetType", new { id = assetType.Id }, assetType);
        }

        // DELETE: api/AssetTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetType(int id)
        {
            var assetType = await _context.AssetTypes.FindAsync(id);
            if (assetType == null)
            {
                return NotFound();
            }

            _context.AssetTypes.Remove(assetType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssetTypeExists(int id)
        {
            return _context.AssetTypes.Any(e => e.Id == id);
        }
    }
}
