using FluentAssertions;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Repositories;

namespace MathHunt.Test.Repository;

public class SkillRepositoryTests : GetDatabaseContext
{
    [Fact]
    public async Task SkillRepository_GetSkill_ReturnSkills()
    {
        //Arrange
        var skillRepository = await CreateSkill();
        
        //Act
        var result = await skillRepository.Get();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<Skill>>();

    }

    [Fact]
    public async Task SkillRepository_CreateSkill_ReturnId()
    {
        //Arrange
        var skillRepository = await CreateSkill();

        var skill = Skill.Create(
            Guid.Parse("837D8524-B236-4194-A5F6-D4813ACC7D43"),
            "C#",
            []
        ).userSkill;
        
        //Act
        var result = await skillRepository.Create(skill);
        
        //Assert
        result.Should().NotBeEmpty();
    }

    // [Fact]
    // public async Task SkillRepository_UpdateSkill_ReturnId()
    // {
    //     //Arrange
    //     var skillRepository = await CreateSkill();
    //     
    //     
    //     
    //     //Act
    //     var result = await skillRepository.Update(
    //         Guid.Parse("E3CBFA4E-604E-4E3B-BE6E-5010CE48530B"),
    //         "Angular"
    //     );
    //     
    //     //Assert
    //     result.Should().NotBeEmpty();
    // }
    
    

    private async Task<SkillRepository> CreateSkill()
    {
        var dbContext = await GetSkill();
        var skillRepository = new SkillRepository(dbContext);
        return skillRepository;
    }
    
}