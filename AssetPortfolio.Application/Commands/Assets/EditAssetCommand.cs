using AssetPortfolio.Application.HelperModel;
using MediatR;

namespace AssetPortfolio.Application.Commands.Assets
{
	public record EditAssetCommand(AssetHelperModel model) : IRequest<AssetHelperModel>;
}
