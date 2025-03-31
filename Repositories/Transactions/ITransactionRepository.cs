using expenses_api.Models;
using expenses_api.Repositories.GenericRepository;

namespace expenses_api.Repositories.Transactions;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    IQueryable<Transaction> GetAllTransactionsByUserIdAsync(Guid userId);
    Task<List<Transaction>> GetAllTransactionsAsync();
}