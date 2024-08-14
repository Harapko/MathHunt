using FluentAssertions;
using MathHunt.Application;
using MathHunt.Controllers;
using MathHunt.Core.Abstraction.IServices;
using MathHunt.Core.Models;
using MathHunt.DataAccess;
using MathHunt.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace MathHunt.Test.Repository;

public sealed class AppUserRepositoryTests : GetDatabaseContext
{
    [Fact]
    public async Task AppUserRepository_GetUsers_ReturnsUsers()
    {
        // Arrange
        var appUserRepository = await GetRepository();
        

        // Act
        var result = await appUserRepository.GetAllUser();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<AppUser>>();
    }

    private async Task<AppUserService> GetRepository()
    {
        var mockUserService = new Mock<IAppUserRepository>();
        
        mockUserService.Setup(service => service.Get())
            .ReturnsAsync(await GetUser());
        var userService = new AppUserService(mockUserService.Object);
        return userService;
    }
}




