using FluentAssertions;
using MathHunt.Core.Models;
using MathHunt.DataAccess;
using MathHunt.DataAccess.Entities;
using MathHunt.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.Test;

public class GetDatabaseContext
{

    private async Task<AppDbContext> CreateDatabase()
    {
        var option = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        

        var databaseContext = new AppDbContext(option, null);
        await databaseContext.Database
            .EnsureCreatedAsync();

        return databaseContext;
    }
    protected async Task<AppDbContext> GetSkill()
    {
        var databaseContext = await CreateDatabase(); 

        if (await databaseContext.Skill.CountAsync() <= 0)
        {
                await databaseContext.Skill.AddAsync(
                    new SkillEntity()
                    {
                        Id = Guid.Parse("E3CBFA4E-604E-4E3B-BE6E-5010CE48530B"),
                        SkillName = "Js"
                    }
                );
                await databaseContext.SaveChangesAsync();
        }
        
        return databaseContext;
    }

    
    protected async Task<AppDbContext> GetUser()
    {
        var databaseContext = await CreateDatabase(); 
        
        if (await databaseContext.AppUsers.CountAsync() <= 0)
        {
            for (int i = 1; i < 10; i++)
            {
                await databaseContext.AppUsers.AddAsync(
                    new AppUserEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "Geralt",
                        UserSurname = "Rivia",
                        Email = "geraltfromrivia@gmail.com",
                        PhoneNumber = "3413123124",
                        EnglishLevel = "A0",
                        Role = "admin"
                    }
                );
            }
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }
    
    
}


