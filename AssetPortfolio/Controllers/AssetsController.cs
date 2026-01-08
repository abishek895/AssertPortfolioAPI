using AssetPortfolio.Application.Commands.Assets;
using AssetPortfolio.Application.HelperModel;
using AssetPortfolio.Application.Queries.Assets;
using AssetPortfolio.Core.Models;
using AssetPortfolio.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetPortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly AssetPortfolioDbContext _context;
        private readonly IMediator _mediator;

        public AssetsController(AssetPortfolioDbContext context,IMediator mediator)
        {
            _context = context;
			_mediator = mediator;
        }

        // GET: api/Assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetHelperModel>>> GetAssets()
        {
				Log.Information("GET /api/Assets called at {Time}", DateTime.UtcNow);
				var assets = await _mediator.Send(new GetAllAssetsQuery());
				Log.Information("{Count} assets retrieved successfully", assets.Count());
				return Ok(assets);
		}

        // GET: api/Assets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetHelperModel>> GetAsset(int id)
        {
			Log.Information("GET /api/Assets/{Id} called at {Time}", id, DateTime.UtcNow);
			var asset = await _mediator.Send(new GetAssetByIdQuery(id));
			if (asset != null)
				Log.Information("Asset with ID {Id} retrieved successfully", id);
			else
				Log.Warning("Asset with ID {Id} not found", id);
			return Ok(asset);
		}

        // PUT: api/Assets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult<AssetHelperModel>> PutAsset(AssetHelperModel asset)
        {
			Log.Information("PUT /api/Assets called at {Time} with Asset ID {Id}", DateTime.UtcNow, asset.Id);
			var result = await _mediator.Send(new EditAssetCommand(asset));
			Log.Information("Asset with ID {Id} updated successfully", result.Id);
			return Ok(result);
		}

        // POST: api/Assets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AssetHelperModel>> PostAsset(AssetHelperModel asset)
        {
			Log.Information("POST /api/Assets called at {Time} for Asset '{Name}'", DateTime.UtcNow, asset.AssetName);
			var result = await _mediator.Send(new CreateAssetCommand(asset));
			Log.Information("Asset created successfully with ID {Id}", result.Id);
			return Ok(result);
		}

        // DELETE: api/Assets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
			Log.Information("DELETE /api/Assets/{Id} called at {Time}", id, DateTime.UtcNow);
			await _mediator.Send(new DeleteAssetCommand(id));
			Log.Information("Asset with ID {Id} deleted successfully", id);
			return NoContent();
		}

		[HttpPost("BulkUpload")]
		public async Task<ActionResult<List<AssetHelperModel>>> BulkUploadAssets([FromBody] List<AssetHelperModel> assets)
		{
			Log.Information("POST /api/Assets/BulkUpload called at {Time} with {Count} assets", DateTime.UtcNow, assets?.Count ?? 0);
			if (assets == null || assets.Count == 0)
			{
				Log.Warning("Bulk upload request received with no assets");
				return BadRequest("No assets provided for bulk upload.");
			}
			var result = await _mediator.Send(new BulkUploadAssetsCommand(assets));
			Log.Information("{Count} assets uploaded successfully via bulk upload", result.Count);
			return Ok(result);
		}


	}
}
