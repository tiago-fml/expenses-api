using expenses_api.Data;
using expenses_api.Models;
using expenses_api.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace expenses_api.Repositories.Transactions;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    private readonly ApplicationDbContext _context;
    
    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public IQueryable<Transaction> GetAllTransactionsByUserIdAsync(Guid userId)
    {
        return _context.Transactions.Where(x => x.UserId == userId);
    }

    public async Task<List<Transaction>> GetAllTransactionsAsync()
    {
        return await _context.Transactions.ToListAsync();
    }
}