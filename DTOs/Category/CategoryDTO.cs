using expenses_api.Enums;

namespace expenses_api.DTOs.Category;

public class CategoryDTO
{
    public string Description { get; set; }
    
    public TransactionType Type { get; set; }
}