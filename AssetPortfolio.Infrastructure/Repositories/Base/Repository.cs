using AssetPortfolio.Core.IRepositories.Base;
using AssetPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AssetPortfolio.Infrastructure.Repositories.Base
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly AssetPortfolioDbContext _dbContext;
		private readonly DbSet<T> _dbSet;
		public Repository(AssetPortfolioDbContext assetPortfolioDbContext) 
		{
			_dbContext = assetPortfolioDbContext;
			_dbSet = _dbContext.Set<T>(); //table for T
		}

		public async Task<T> AddAsync(T Entity)
		{
			await _dbSet.AddAsync(Entity);
			await _dbContext.SaveChangesAsync();
			return Entity;
		}

		public async Task DeleteAsync(int Id)
		{
			var entity = await _dbSet.FindAsync(Id);
			if(entity != null)
			{
				_dbSet.Remove(entity);
				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<T>> GetAllAsync() 
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T> GetByIdAsync(int Id)
		{
			var entity = await _dbSet.FindAsync(Id);
			if(entity == null) 
			{
				throw new NotImplementedException();
			}
			return entity;
		}

		public async Task UpdateAsync(T Entity)
		{
			_dbSet.Attach(Entity);                               
			_dbContext.Entry(Entity).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();
		}
		public IQueryable<T> GetQueryable()
		{
			return _dbSet.AsQueryable();
		}
	}
}
