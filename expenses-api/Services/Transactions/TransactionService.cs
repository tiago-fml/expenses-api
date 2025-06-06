using AutoMapper;
using expenses_api.DTOs.Transaction;
using expenses_api.Enums;
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
    
    public async Task<IEnumerable<TransactionDTO>> GetUserTransactionsAsync(DateTimeOffset startDate,
        DateTimeOffset endDate)
    {
        var userId = _jwtService.GetUserId();
        
        var list = await _unitOfWork.TransactionRepository
            .FindByCondition(x => x.UserId == userId &&
                x.ExecutedAt >= startDate && x.ExecutedAt <= endDate) ?? [];
        
        return _mapper.Map<IEnumerable<TransactionDTO>>(list);
    }

    public async Task<TransactionDTO> AddTransactionAsync(TransactionCreateDTO transactionCreateDto)
    {
        var userId = _jwtService.GetUserId();
        
        var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(transactionCreateDto.CategoryId);
        if (category == null)
        {
            throw new Exception("Category not found");
        }
        
        var transaction = _mapper.Map<Transaction>(transactionCreateDto);
        transaction.UserId = userId;
        transaction.Type = category.Type;
        
        _unitOfWork.TransactionRepository.Add(transaction);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TransactionDTO>(transaction);
    }

    public async Task<TransactionDTO?> UpdateTransactionAsync(Guid id, TransactionUpdateDTO transactionUpdateDto)
    {
        var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);
        if(transaction is null){ throw new Exception("Transaction not found"); }
        
        _mapper.Map(transactionUpdateDto, transaction);
        
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<TransactionDTO>(transaction);
    }

    public async Task<TransactionDTO?> GetTransactionAsync(Guid id)
    {
        var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);

        if (transaction.UserId != _jwtService.GetUserId())
        {
            throw new UnauthorizedAccessException();
        }
        
        return transaction == null ? null : _mapper.Map<TransactionDTO>(transaction);
    }

    public async Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync()
    {
        var list = await _unitOfWork.TransactionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TransactionDTO>>(list);
    }

    public async Task<double> GetTotalSpentAsync(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        var userId = _jwtService.GetUserId();
        
        var list = await _unitOfWork.TransactionRepository
            .FindByCondition(x=> x.UserId == userId && x.Type == TransactionType.EXPENSE 
                && x.ExecutedAt >= startDate && x.ExecutedAt <= endDate);
        
        var expense = list.Sum(x=>x.Value);

        return (double)expense;
    }
    
    public async Task<double> GetTotalEarnedAsync(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        var userId = _jwtService.GetUserId();
        
        var list = await _unitOfWork.TransactionRepository
            .FindByCondition(x=>x.UserId == userId && x.Type == TransactionType.INCOME 
                && x.ExecutedAt >= startDate && x.ExecutedAt <= endDate);

        var income = list.Sum(x=>x.Value);
        
        return (double)income;
    }
}