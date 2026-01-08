using AssetPortfolio.Application.HelperModel;
using MediatR;

namespace AssetPortfolio.Application.Queries.Assets
{
	public record GetAssetByIdQuery(int Id) : IRequest<AssetHelperModel>;
	
}
