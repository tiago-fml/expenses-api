using expenses_api.Repositories.Transactions;
using expenses_api.Repositories.Users;

namespace expenses_api.Repositories.UnityOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    ITransactionRepository TransactionRepository { get; }
    Task<int> SaveChangesAsync();
}