using AssetPortfolio.Core.IRepositories.Asset;
using AssetPortfolio.Infrastructure.Data;
using AssetPortfolio.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using AssetModel = AssetPortfolio.Core.Models.Asset;

namespace AssetPortfolio.Infrastructure.Repositories.Asset
{
	public class AssetRepository : Repository<AssetModel>,IAssetRepository
	{
		private readonly AssetPortfolioDbContext _dbContext;
		public AssetRepository(AssetPortfolioDbContext assetPortfolioDbContext) : base(assetPortfolioDbContext)
		{
			_dbContext = assetPortfolioDbContext;
		}
	}
}
