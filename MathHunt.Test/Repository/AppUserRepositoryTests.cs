// using FluentAssertions;
// using MathHunt.Core.Abstraction.IServices;
// using MathHunt.DataAccess;
// using MathHunt.DataAccess.Entities;
// using MathHunt.DataAccess.Repositories;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Moq;
//
// namespace MathHunt.Test.Repository;
//
// public class AppUserRepositoryTests : GetDatabaseContext
// {
//
//     
//
//     [Fact]
//     public async Task AppUserRepository_GetUsers_ReturnsUsers()
//     {
//         // Arrange
//         var test = await GetUser();
//         
//         // Act
//         var result = await userRepository.Get();
//
//         // Assert
//         result.Should().NotBeNull();
//         result.Should().BeOfType<List<AppUserEntity>>();
//     }
// }
//
//
//
//
