using AutoMapper;
using expenses_api.DTOs.Transaction;
using expenses_api.Repositories.Transactions;
using expenses_api.Repositories.UnityOfWork;

namespace expenses_api.Services.Transaction;

public class TransactionService(IMapper mapper, IUnitOfWork unitOfWork)
    : ITransactionService
{
    
    public Task<IEnumerable<TransactionDto>> GetTransactionsByUserIdAsync(Guid userId, DateTimeOffset? startDate = null,
        DateTimeOffset? endDate = null)
    {
        throw new NotImplementedException();
    }

    public async Task<TransactionDto> AddTransactionAsync(TransactionCreateDto transactionCreateDto)
    {
        throw new NotImplementedException();
        
        /*
        var transaction = mapper.Map<Models.Transaction>(transactionCreateDto);

        transactionRepository.AddTransaction(transaction);

        await transactionRepository.SaveChangesAsync();

        return mapper.Map<TransactionDto>(transaction);
        */
    }

    public async Task<TransactionDto?> UpdateTransactionAsync(Guid id, TransactionUpdateDto transaction)
    {
        //var transaction = transactionRepository.
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
        throw new NotImplementedException();
        //var list = await transactionRepository.GetAllTransactionsAsync();
        //return mapper.Map<List<TransactionDto>>(list);
    }
    
}