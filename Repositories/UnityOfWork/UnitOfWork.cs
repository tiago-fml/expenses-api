using expenses_api.Data;
using expenses_api.Repositories.Transactions;
using expenses_api.Repositories.Users;

namespace expenses_api.Repositories.UnityOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IUserRepository UserRepository { get; private set; }
    public ITransactionRepository TransactionRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        UserRepository = new UserRepository(_context);
        TransactionRepository = new TransactionRepository(_context);
    }
    
    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}