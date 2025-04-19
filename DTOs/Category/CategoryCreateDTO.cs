using expenses_api.Enums;

namespace expenses_api.DTOs.Category;

public class CategoryCreateDTO
{
    public string Description { get; set; }
    
    public TransactionType Type { get; set; }
}