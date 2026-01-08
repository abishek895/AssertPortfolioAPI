using AssetPortfolio.Application.HelperModel;
using MediatR;

namespace AssetPortfolio.Application.Queries.Assets
{
	public record GetAllAssetsQuery() : IRequest<List<AssetHelperModel>>;
}
