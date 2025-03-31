using expenses_api.DTOs.Transaction;

namespace expenses_api.Services.Transactions;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetTransactionsByUserIdAsync(Guid userId, DateTimeOffset startDate,
        DateTimeOffset endDate);
    
    Task<TransactionDto> AddTransactionAsync(TransactionCreateDto transaction);
    
    Task<TransactionDto?> UpdateTransactionAsync(Guid id, TransactionUpdateDto transaction);
    
    Task<TransactionDto?> GetTransactionAsync(Guid id);
    
    Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync();
}