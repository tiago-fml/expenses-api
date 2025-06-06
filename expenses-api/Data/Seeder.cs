using expenses_api.Enums;
using expenses_api.Models;
using expenses_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace expenses_api.Data;

public static class Seeder
{
    public static void SeedData(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        if (!context.Categories.Any())
        {
            var categoriesList = new List<Category>
            {
                //EXPENSES
                new("Food", TransactionType.EXPENSE),
                new("Home", TransactionType.EXPENSE),
                new("Medical", TransactionType.EXPENSE),
                new("Fuel", TransactionType.EXPENSE),
                new("Investments", TransactionType.EXPENSE),
                new("Dinners", TransactionType.EXPENSE),
                new("Gifts", TransactionType.EXPENSE),
                new("Personal", TransactionType.EXPENSE),
                new("Nights Out", TransactionType.EXPENSE),
                new("Utilities", TransactionType.EXPENSE),
                
                //INCOMES
                new("Savings", TransactionType.INCOME),
                new("Paycheck", TransactionType.INCOME),
                new("Interest", TransactionType.INCOME),
                new("Bonus", TransactionType.INCOME),
                new("Sales", TransactionType.INCOME),
            };
            
            context.Categories.AddRange(categoriesList);
        }
            
        context.SaveChanges();
    }
}