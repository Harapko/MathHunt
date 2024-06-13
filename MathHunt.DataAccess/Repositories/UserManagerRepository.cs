using MathHunt.Core.Abstraction.IRepositories;
using MathHunt.Core.Models;
using MathHunt.DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MathHunt.DataAccess.Repositories;

public class UserManagerRepository(
    AppDbContext context,
    UserManager<AppUserEntity> userManager,
    IWebHostEnvironment environment) : IUserManagerRepository
{
    public async Task<List<string>> GetUserSkills(string userName)
    {
        var userEntity = await context.Users
            .AsNoTracking()
            .Where(u => u.UserName == userName)
            .Include(u => u.UserSkillsEntities)
            .FirstOrDefaultAsync();

        var userSkill = userEntity.UserSkillsEntities
            .Select(s => s.SkillName)
            .ToList();
    
        return userSkill;
    }

    public async Task<string> AddToUser(string userName, string skillName)
    {
        var skillEntity = await context.UserSkill
            .AsNoTracking()
            .Where(s => s.SkillName == skillName)
            .FirstOrDefaultAsync();
        var user = await userManager.FindByNameAsync(userName);
        if (user != null && skillEntity != null)
        {
            user.UserSkillsEntities?.Add(skillEntity);
        }

        await context.SaveChangesAsync();


        return user.Email;
    }

    public async Task<bool> DeleteSkill(string userName, string skillName)
    {
        var skill = await context.UserSkill
            .Include(s=>s.AppUserEntities)
            .FirstOrDefaultAsync(s => s.SkillName == skillName);

        var user = await userManager.FindByNameAsync(userName);

        var resp = skill.AppUserEntities.Remove(user);
        await context.SaveChangesAsync();
        return resp;
    }
    
    public async Task<PhotoUser> CreatePhoto(IFormFile titlePhoto, string appUserId)
    {
        try
        {
            string path = environment.WebRootPath + "\\PhotoUser\\";
            var fileName = Path.GetFileName(titlePhoto.FileName);
            var filePath = Path.Combine(path, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await titlePhoto.CopyToAsync(stream);
            }

            var photo = PhotoUser.Create(Guid.NewGuid(),fileName, appUserId).photoUser;
            var photoUserEntity = new PhotoUserEntity
            {
                Id = photo.Id,
                Path = photo.Path,
                AppUserEntityId = userManager.FindByIdAsync(appUserId).Result.Id
                
            };
            await context.PhotoUser.AddAsync(photoUserEntity);
            await context.SaveChangesAsync();
            return photo;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Guid> UpdatePhoto(Guid id, IFormFile path, string appUserId)
    {
        await DeletePhoto(id);
        await CreatePhoto(path, appUserId);
        return id;

    }

    public async Task<Guid> DeletePhoto(Guid id)
    {
        string path = environment.WebRootPath + "\\PhotoUser\\";
        
        var photoFromDb = await context.PhotoUser
            .FirstOrDefaultAsync(p => p.Id == id);
        
        var fileName = photoFromDb.Path;
        var filePath = Path.Combine(path, fileName);

        File.Delete(filePath);
        
        await context.PhotoUser
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}