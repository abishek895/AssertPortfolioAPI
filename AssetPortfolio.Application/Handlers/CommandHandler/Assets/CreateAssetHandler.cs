using AssetPortfolio.Application.Commands.Assets;
using AssetPortfolio.Application.HelperModel;
using AssetPortfolio.Core.IRepositories.Asset;
using AssetPortfolio.Core.Models;
using MediatR;

namespace AssetPortfolio.Application.Handlers.CommandHandler.Assets
{
	public class CreateAssetHandler : IRequestHandler<CreateAssetCommand,AssetHelperModel>
	{
		private readonly IAssetRepository _assetRepository;
		public CreateAssetHandler(IAssetRepository assetRepository) 
		{ 
			_assetRepository = assetRepository;
		}
		public async Task<AssetHelperModel> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
		{
			var entity = new Asset
			{
				AssetTypeId = request.Asset.AssetTypeId,
				AssetName = request.Asset.AssetName,
				PriceUsd = request.Asset.PriceUsd,
				Quantity = request.Asset.Quantity,
				CreatedOn = DateTime.Now,
				Createby = 1 
			};
			var newAsset = await _assetRepository.AddAsync(entity);
			return new AssetHelperModel
			{
				Id = newAsset.Id,
				AssetTypeId = newAsset.AssetTypeId,
				AssetName = newAsset.AssetName,
				PriceUsd = newAsset.PriceUsd,
				CreatedOn = newAsset.CreatedOn,
				Createby = newAsset.Createby
			};
		}
	}
}
