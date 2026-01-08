using MediatR;
using AssetPortfolio.Application.HelperModel;

namespace AssetPortfolio.Application.Commands.Assets
{
	public record CreateAssetCommand(AssetHelperModel Asset) : IRequest<AssetHelperModel>;
}
