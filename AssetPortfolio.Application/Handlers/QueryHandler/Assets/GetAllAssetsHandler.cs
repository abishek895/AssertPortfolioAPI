using AssetPortfolio.Application.HelperModel;
using AssetPortfolio.Application.Queries.Assets;
using AssetPortfolio.Core.IRepositories.Asset;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssetPortfolio.Application.Handlers.QueryHandler.Assets
{
	public class GetAllAssetsHandler : IRequestHandler<GetAllAssetsQuery, List<AssetHelperModel>>
	{
		private readonly IAssetRepository _assetRepository;

		public GetAllAssetsHandler(IAssetRepository assetRepository)
		{
			_assetRepository = assetRepository;
		}

		public async Task<List<AssetHelperModel>> Handle(GetAllAssetsQuery request, CancellationToken cancellationToken)
		{
			var assets = await _assetRepository
				.GetQueryable()
				.Include(a => a.AssetType)
				.ToListAsync();
			var result = assets.Select(a => new AssetHelperModel
			{
				Id = a.Id,
				AssetTypeId = a.AssetTypeId,
				AssetName = a.AssetName,
				PriceUsd = a.PriceUsd,
				CreatedOn = a.CreatedOn,
				Createby = a.Createby,
				UpdatedOn = a.UpdatedOn,
				Quantity = a.Quantity,
				UpdatedBy = a.UpdatedBy,

				AssetTypeHelperModels = a.AssetType != null ? new List<AssetTypeHelperModel>
					{
						new AssetTypeHelperModel
						{
							Id = a.AssetType.Id,
							AssetTypeName = a.AssetType.AssetTypeName,
							CreatedOn = a.AssetType.CreatedOn,
							CreatedBy = a.AssetType.CreatedBy,
							UpdatedOn = a.AssetType.UpdatedOn,
							UpdatedBy = a.AssetType.UpdatedBy
						}
					} : null
			}).ToList();

			return result;
		}
	}
}
