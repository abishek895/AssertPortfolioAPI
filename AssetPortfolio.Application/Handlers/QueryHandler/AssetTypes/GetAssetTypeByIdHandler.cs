using AssetPortfolio.Application.HelperModel;
using AssetPortfolio.Application.Queries.AssetTypes;
using AssetPortfolio.Core.IRepositories.AssetType;
using MediatR;

namespace AssetPortfolio.Application.Handlers.QueryHandler.AssetTypes
{
	public class GetAssetTypeByIdHandler : IRequestHandler<GetAssetTypeByIdQuery,AssetTypeHelperModel>
	{
		private readonly IAssetTypeRepository _assetTypeRepository;
		public GetAssetTypeByIdHandler(IAssetTypeRepository assetTypeRepository) 
		{ 
			_assetTypeRepository = assetTypeRepository;
		}
		public async Task<AssetTypeHelperModel> Handle(GetAssetTypeByIdQuery request, CancellationToken cancellationToken)
		{
			var result = await _assetTypeRepository.GetByIdAsync(request.Id);
			return new AssetTypeHelperModel
			{
				Id = result.Id,
				AssetTypeName = result.AssetTypeName,
				CreatedBy = result.CreatedBy,
				CreatedOn = result.CreatedOn,
				UpdatedBy = result.UpdatedBy,
				UpdatedOn = result.UpdatedOn,
			};
		}
	}
}
