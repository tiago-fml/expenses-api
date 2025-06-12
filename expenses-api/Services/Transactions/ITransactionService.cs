using expenses_api.DTOs.Transaction;

namespace expenses_api.Services.Transactions;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDTO>> GetUserTransactionsAsync(DateTimeOffset startDate,
        DateTimeOffset endDate);
    
    Task<TransactionDTO> AddTransactionAsync(TransactionCreateDTO transaction);
    
    Task<TransactionDTO?> UpdateTransactionAsync(Guid id, TransactionUpdateDTO transaction);
    
    Task<TransactionDTO?> GetTransactionAsync(Guid id);
    
    Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync();
    
    Task<double> GetTotalSpentAsync(DateTimeOffset startDate, DateTimeOffset endDate);
    
    Task<double> GetTotalEarnedAsync(DateTimeOffset startDate, DateTimeOffset endDate);
    
    Task DeleteTransactionAsync(Guid id);
}