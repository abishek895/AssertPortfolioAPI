namespace AssetPortfolio.Core.IRepositories.Base
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync();
		IQueryable<T> GetQueryable();
		Task<T> GetByIdAsync(int Id);
		Task<T> AddAsync(T Entity);
		Task UpdateAsync(T Entity);
		Task DeleteAsync(int Id);
	}
}
