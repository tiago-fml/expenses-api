namespace expenses_api.DTOs.Transaction;

public class TransactionDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string Description { get; set; }
    public decimal Value { get; set; }
    public DateTimeOffset ExecutedAt { get; set; }
    public Guid CategoryId { get; set; }
}