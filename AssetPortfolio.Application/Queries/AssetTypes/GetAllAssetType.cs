using AssetPortfolio.Application.HelperModel;
using MediatR;

namespace AssetPortfolio.Application.Queries.AssetTypes
{
	public record GetAllAssetTypeQuery : IRequest<List<AssetTypeHelperModel>>;
}
