﻿using expenses_api.Enums;
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
        
        // Look for any users already in the database.
        if (!context.Users.Any())
        {
                
            var adminUsername = configuration["AdminUser:UserName"];
            var adminPassword = configuration["AdminUser:Password"];

            if (adminUsername is null || adminPassword is null)
            {
                return;
            }

            context.Users.AddRange(
                new User
                {
                    Username = adminUsername,
                    HashedPassword = PasswordHasher.HashPassword(adminPassword),
                    Role = Roles.Admin,
                    Email = "",
                    FirstName = "Admin",
                    LastName = "Admin"
                }
            );
        }
            
        context.SaveChanges();
    }
}