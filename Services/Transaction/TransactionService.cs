using AutoMapper;
using expenses_api.DTOs.Transaction;
using expenses_api.Repositories.Transactions;

namespace expenses_api.Services.Transaction;

public class TransactionService(IMapper mapper, ITransactionRepository transactionRepository)
    : ITransactionService
{
    public Task<IEnumerable<TransactionDto>> GetTransactionsByUserIdAsync(Guid userId, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
    {
        throw new NotImplementedException();
    }

    public async Task<TransactionDto> AddTransactionAsync(TransactionCreateDto transactionCreateDto)
    {
        var transaction = mapper.Map<Models.Transaction>(transactionCreateDto);

        transactionRepository.AddTransaction(transaction);

        await transactionRepository.SaveChangesAsync();
        
        return mapper.Map<TransactionDto>(transaction);
    }

    public Task<TransactionDto?> UpdateTransactionAsync(Guid id, TransactionUpdateDto transaction)
    {
        throw new NotImplementedException();
    }

    public async Task<TransactionDto?> GetTransactionAsync(Guid id)
    {
        /*var list = await transactionRepository.GetAllTransactionsAsync();
        return mapper.Map<TransactionDto>(list);*/
        throw new NotImplementedException();

    }

    public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
    {
        var list = await transactionRepository.GetAllTransactionsAsync();
        return mapper.Map<List<TransactionDto>>(list);
    }
}