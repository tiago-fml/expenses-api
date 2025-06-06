using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using expenses_api.Enums;

namespace expenses_api.Models;

public class Category : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] 
    public Guid Id { get; set; }
    public string Description { get; set; }
    
    public TransactionType Type { get; set; }
    
    public Guid? UserId { get; set; }
    
    public bool IsDefault { get; set; }
    
    public User? User { get; set; }
    
    public Category(string description, TransactionType type)
    {
        Id = Guid.NewGuid();
        Description = description;
        IsDefault = true;
        Type = type;    
        UserId = null;
    }
    
    public Category(string description, TransactionType type, Guid userId)
    {
        Id = Guid.NewGuid();
        Description = description;
        IsDefault = false;
        Type = type;
        UserId = userId;    
    }
}