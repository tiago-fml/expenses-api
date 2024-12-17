using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using expenses_api.Enums;

namespace expenses_api.Models;

public class Transaction  : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] 
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public required string Description { get; set; }
    public decimal Value { get; set; }
    public DateTimeOffset ExecutedAt { get; set; }
    public TransactionType Type { get; set; }
    
    [ForeignKey(nameof(Category))]
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    
    public Transaction()
    {
        Id = Guid.NewGuid();
    }
}