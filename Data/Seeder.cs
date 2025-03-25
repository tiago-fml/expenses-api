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
            var categoriesList = new List<Category>()
            {
                //EXPENSES
                new("Food"),
                new("Home"),
                new("Medical"),
                new("Fuel"),
                new("Investments"),
                new("Dinners"),
                new("Gifts"),
                new("Personal"),
                new("Nights Out"),
                new("Carne"),
                new("Utilities"),
                
                //INCOMES
                new("Savings"),
                new("Paycheck"),
                new("Interest"),
                new("Bonus"),
                new("Sales"),
            };
            
            context.Categories.AddRange(categoriesList);
        }
            
        context.SaveChanges();
    }
}