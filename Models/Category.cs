using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expenses_api.Models;

public class Category : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] 
    public Guid Id { get; set; }
    public string Description { get; set; }
    
    public Category(string description)
    {
        Id = Guid.NewGuid();
        Description = description;
    }
}