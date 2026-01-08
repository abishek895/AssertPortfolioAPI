using MediatR;
using AssetPortfolio.Application.HelperModel;
using System.Collections.Generic;

namespace AssetPortfolio.Application.Commands.Assets
{
	public record BulkUploadAssetsCommand(List<AssetHelperModel> Assets) : IRequest<List<AssetHelperModel>>;
}
