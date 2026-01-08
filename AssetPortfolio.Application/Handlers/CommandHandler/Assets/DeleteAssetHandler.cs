using AssetPortfolio.Application.Commands.Assets;
using AssetPortfolio.Core.IRepositories.Asset;
using MediatR;

namespace AssetPortfolio.Application.Handlers.CommandHandler.Assets
{
	public class DeleteAssetHandler : IRequestHandler<DeleteAssetCommand,Unit>
	{
		private readonly IAssetRepository _assetRepository;
		public DeleteAssetHandler(IAssetRepository assetRepository)
		{
			_assetRepository = assetRepository;
		}
		public async Task<Unit> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
		{
			await _assetRepository.DeleteAsync(request.Id);
			return Unit.Value;
		}
	}
}
