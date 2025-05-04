using expenses_api.Enums;

namespace expenses_api.DTOs.Transaction;

public class TransactionCreateDTO
{
    public required string Description { get; set; }
    public decimal Value { get; set; }
    public DateTimeOffset ExecutedAt { get; set; }
    public Guid CategoryId { get; set; }
}