using AssetPortfolio.Application.HelperModel;
using AssetPortfolio.Application.Queries.AssetTypes;
using AssetPortfolio.Core.IRepositories.AssetType;
using MediatR;

namespace AssetPortfolio.Application.Handlers.QueryHandler.AssetTypes
{
	public class GetAllAssetTypesHandler : IRequestHandler<GetAllAssetTypeQuery,List<AssetTypeHelperModel>>
	{
		private readonly IAssetTypeRepository _assetTypeRepository;
		public GetAllAssetTypesHandler(IAssetTypeRepository assetTypeRepository) 
		{
			_assetTypeRepository = assetTypeRepository;
		}
		public async Task<List<AssetTypeHelperModel>> Handle(GetAllAssetTypeQuery request, CancellationToken cancellationToken)
		{
			var result = await _assetTypeRepository.GetAllAsync();
			return result.Select(assetType => new AssetTypeHelperModel
			{
				Id = assetType.Id,
				AssetTypeName = assetType.AssetTypeName,
				CreatedBy = assetType.CreatedBy,
				CreatedOn = assetType.CreatedOn,
				UpdatedBy = assetType.UpdatedBy,
				UpdatedOn = assetType.UpdatedOn,
			}).ToList();
		}
	}
}
