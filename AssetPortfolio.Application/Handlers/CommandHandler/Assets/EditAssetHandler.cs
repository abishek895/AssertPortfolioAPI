using AssetPortfolio.Application.Commands.Assets;
using AssetPortfolio.Application.HelperModel;
using AssetPortfolio.Core.IRepositories.Asset;
using MediatR;

namespace AssetPortfolio.Application.Handlers.CommandHandler.Assets
{
	public class EditAssetHandler: IRequestHandler<EditAssetCommand,AssetHelperModel>
	{
		private readonly IAssetRepository _assetRepository;
		public EditAssetHandler(IAssetRepository assetRepository) 
		{ 
			_assetRepository = assetRepository;
		}
		public async Task<AssetHelperModel> Handle(EditAssetCommand request, CancellationToken cancellationToken)
		{
			var asset = await _assetRepository.GetByIdAsync(request.model.Id);
			if(asset == null)
			{
				throw new Exception("Asset Not Found");
			}
			asset.AssetTypeId = request.model.AssetTypeId;
			asset.AssetName = request.model.AssetName;
			asset.PriceUsd = request.model.PriceUsd;
			asset.Quantity = request.model.Quantity;
			asset.UpdatedOn = DateTime.Now;
			asset.UpdatedBy = request.model.UpdatedBy;
			await _assetRepository.UpdateAsync(asset);
			return new AssetHelperModel
			{
				Id = asset.Id,
				AssetTypeId = asset.AssetTypeId,
				AssetName = asset.AssetName,
				PriceUsd = asset.PriceUsd,
				UpdatedOn = asset.UpdatedOn,
				UpdatedBy = asset.UpdatedBy,
				Quantity = asset.Quantity
			};
		}
	}
}
