using MathHunt.Core.Models;
using MathHunt.DataAccess;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.Test;

public class GetDatabaseContext
{
    private static async Task<AppDbContext> CreateDatabase()
    {
        var option = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;


        var databaseContext = new AppDbContext(option, null);
        await databaseContext.Database
            .EnsureCreatedAsync();

        return databaseContext;
    }

    protected static async Task<AppDbContext> GetSkill()
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


    protected static async Task<List<AppUser>> GetUser()
    {
        var listUser = new List<AppUser>();
        if (listUser.Count != 0) return null;

        var user = AppUser.Create(
            Guid.NewGuid().ToString(),
            "Geralt",
            "Rivia",
            "geraltfromrivia@gmail.com",
            "3413123124",
            "B2",
            "",
            "",
            "admin",
            DateTime.Now,
            false,
            [],
            [],
            []
        ).appUser;

        listUser.Add(user);

        return await Task.FromResult(listUser);
    }
}

