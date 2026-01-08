using AssetPortfolio.Core.Models;
using MediatR;

namespace AssetPortfolio.Application.Commands.Assets
{
	public record DeleteAssetCommand(int Id) : IRequest<Unit>;
}
