using AutoMapper;
using expenses_api.DTOs.Transaction;
using expenses_api.Models;
using expenses_api.Repositories.UnityOfWork;
using expenses_api.Services.Jwt;

namespace expenses_api.Services.Transactions;

public class TransactionService : ITransactionService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    
    public TransactionService(IMapper mapper, IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }
    
    public async Task<IEnumerable<TransactionDto>> GetTransactionsByUserIdAsync(Guid userId, DateTimeOffset startDate,
        DateTimeOffset endDate)
    {
        var list = _unitOfWork.TransactionRepository
            .FindByCondition(x => x.UserId == userId &&
                x.ExecutedAt >= startDate && x.ExecutedAt <= endDate);
        
        return _mapper.Map<IEnumerable<TransactionDto>>(list);
    }

    public async Task<TransactionDto> AddTransactionAsync(TransactionCreateDto transactionCreateDto)
    {
        var userId = GetUserId();
        
        var user = _unitOfWork.UserRepository.GetUserByIdAsync(userId);
        if(user is null){ throw new Exception("User not found"); }
        
        var transaction = _mapper.Map<Transaction>(transactionCreateDto);

        _unitOfWork.TransactionRepository.Add(transaction);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TransactionDto>(transaction);
    }

    public async Task<TransactionDto?> UpdateTransactionAsync(Guid id, TransactionUpdateDto transactionUpdateDto)
    {
        var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);
        if(transaction is null){ throw new Exception("Transaction not found"); }
        
        _mapper.Map(transactionUpdateDto, transaction);
        
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<TransactionDto>(transaction);
    }

    public async Task<TransactionDto?> GetTransactionAsync(Guid id)
    {
        var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);

        if (transaction.UserId != GetUserId())
        {
            throw new UnauthorizedAccessException();
        }
        
        return transaction == null ? null : _mapper.Map<TransactionDto>(transaction);
    }

    public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
    {
        var list = await _unitOfWork.TransactionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TransactionDto>>(list);
    }

    private Guid GetUserId()
    {
        var userId = _jwtService.GetUserId();
        if (userId is null)
        {
            throw new Exception("User id is null");
        }
        
        return userId.Value;
    }
}