using expenses_api.Models;

namespace expenses_api.Repositories.Transactions;

public interface ITransactionRepository
{
    IQueryable<Transaction> GetAllTransactionsByUserIdAsync(Guid userId);
    Task<List<Transaction>> GetAllTransactionsAsync();
    void AddTransaction(Transaction transaction);
    void DeleteTransaction(Transaction transaction); 
}