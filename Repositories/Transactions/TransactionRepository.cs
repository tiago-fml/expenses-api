using expenses_api.Data;
using expenses_api.Models;
using Microsoft.EntityFrameworkCore;

namespace expenses_api.Repositories.Transactions;

public class TransactionRepository(ApplicationDbContext context) : ITransactionRepository
{
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public IQueryable<Transaction> GetAllTransactionsByUserIdAsync(Guid userId)
    {
        return context.Transactions.Where(x => x.UserId == userId);
    }

    public async Task<List<Transaction>> GetAllTransactionsAsync()
    {
        return await context.Transactions.ToListAsync();
    }

    public void AddTransaction(Transaction transaction)
    {
        context.Transactions.Add(transaction);
    }

    public void DeleteTransaction(Transaction transaction)
    {
        throw new NotImplementedException();
    }
}