using AssetPortfolio.Core.IRepositories.AssetType;
using AssetPortfolio.Infrastructure.Data;
using AssetPortfolio.Infrastructure.Repositories.Base;
using AssetTypeModel = AssetPortfolio.Core.Models.AssetType;

namespace AssetPortfolio.Infrastructure.Repositories.AssetType
{
	public class AssetTypeRepository : Repository<AssetTypeModel>,IAssetTypeRepository
	{
		private readonly AssetPortfolioDbContext _dbContext;
		public AssetTypeRepository(AssetPortfolioDbContext assetPortfolioDbContext) : base(assetPortfolioDbContext) 
		{
			_dbContext = assetPortfolioDbContext;
		}
	}
}
