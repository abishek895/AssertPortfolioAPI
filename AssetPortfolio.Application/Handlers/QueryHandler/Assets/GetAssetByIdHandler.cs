using AssetPortfolio.Application.HelperModel;
using AssetPortfolio.Application.Queries.Assets;
using AssetPortfolio.Core.IRepositories.Asset;
using AssetPortfolio.Core.Models;
using MediatR;

namespace AssetPortfolio.Application.Handlers.QueryHandler.Assets
{
	public class GetAssetByIdHandler: IRequestHandler<GetAssetByIdQuery,AssetHelperModel>
	{
		private readonly IAssetRepository _assetRepository;
		public GetAssetByIdHandler(IAssetRepository assetRepository) 
		{
			_assetRepository = assetRepository;
		}
		public async Task<AssetHelperModel> Handle(GetAssetByIdQuery request,CancellationToken cancellationToken)
		{
			var result = await _assetRepository.GetByIdAsync(request.Id);
			return new AssetHelperModel
			{
				Id = result.Id,
				AssetTypeId = result.AssetTypeId,
				AssetName = result.AssetName,
				PriceUsd = result.PriceUsd,
				CreatedOn = result.CreatedOn,
				Createby = result.Createby,
				UpdatedOn = result.UpdatedOn,
				UpdatedBy = result.UpdatedBy,
				Quantity = result.Quantity
			};
		}
	}
}
