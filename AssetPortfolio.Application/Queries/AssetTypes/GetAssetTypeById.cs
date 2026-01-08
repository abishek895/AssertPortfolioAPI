using AssetPortfolio.Application.HelperModel;
using MediatR;

namespace AssetPortfolio.Application.Queries.AssetTypes
{
	public record GetAssetTypeByIdQuery(int Id) : IRequest<AssetTypeHelperModel>;
}
