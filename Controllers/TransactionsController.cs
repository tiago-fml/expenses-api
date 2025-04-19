using expenses_api.DTOs.Transaction;
using expenses_api.Services.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace expenses_api.Controllers;

[Authorize]
[ApiController]
[Route("api/transactions")]
public class TransactionsController(ITransactionService _transactionsService) : ControllerBase
{
    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetUserTransactions(DateTimeOffset startDate,
        DateTimeOffset endDate)
    {
        try
        {
            var list = await _transactionsService.GetUserTransactionsAsync(
                startDate, endDate);
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(GetUserTransactions)}" + e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionDTO>> GetTransaction(Guid id)
    {
        try
        {
            var transaction = await _transactionsService.GetTransactionAsync(id);
            return Ok(transaction);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(GetTransaction)}" + e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<TransactionDTO>> AddTransaction(TransactionCreateDTO transactionCreateDto)
    {
        try
        {
            var transaction = await _transactionsService.AddTransactionAsync(transactionCreateDto);
            return Ok(transaction);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(AddTransaction)}" + e.Message);
        }
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<TransactionDTO>> UpdateTransaction(Guid id, TransactionUpdateDTO transactionUpdateDto)
    {
        try
        {
            var transaction = await _transactionsService.UpdateTransactionAsync(id, transactionUpdateDto);
            return Ok(transaction);
        }
        catch (Exception e)
        {
            return BadRequest($"Error in method {nameof(UpdateTransaction)}" + e.Message);
        }
    }
}