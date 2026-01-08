using AssetPortfolio.Application.Commands.Assets;
using AssetPortfolio.Application.HelperModel;
using AssetPortfolio.Core.IRepositories.Asset;
using AssetPortfolio.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AssetPortfolio.Application.Handlers.CommandHandler.Assets
{
	public class BulkUploadAssetHandler : IRequestHandler<BulkUploadAssetsCommand, List<AssetHelperModel>>
	{
		private readonly IAssetRepository _assetRepository;

		public BulkUploadAssetHandler(IAssetRepository assetRepository)
		{
			_assetRepository = assetRepository;
		}

		public async Task<List<AssetHelperModel>> Handle(BulkUploadAssetsCommand request, CancellationToken cancellationToken)
		{
			var results = new List<AssetHelperModel>();

			foreach (var assetModel in request.Assets)
			{
				if (string.IsNullOrWhiteSpace(assetModel.AssetName) ||
					assetModel.AssetTypeId <= 0 ||
					assetModel.PriceUsd <= 0 ||
					assetModel.Quantity <= 0)
				{
					continue;
				}

				var entity = new Asset
				{
					AssetName = assetModel.AssetName,
					AssetTypeId = assetModel.AssetTypeId,
					PriceUsd = assetModel.PriceUsd,
					Quantity = assetModel.Quantity,
					CreatedOn = DateTime.Now,
					Createby = 1
				};

				var newAsset = await _assetRepository.AddAsync(entity);

				results.Add(new AssetHelperModel
				{
					Id = newAsset.Id,
					AssetName = newAsset.AssetName,
					AssetTypeId = newAsset.AssetTypeId,
					PriceUsd = newAsset.PriceUsd,
					Quantity = newAsset.Quantity,
					CreatedOn = newAsset.CreatedOn,
					Createby = newAsset.Createby
				});
			}

			return results;
		}
	}
}
