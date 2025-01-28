using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public UserRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserByIdWithSkills(int id)
    {
        return await _dbContext.Users
            .Include(user => user.Skills)
            .ThenInclude(skill => skill.Skill)
            .SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task Add(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddSkills(IEnumerable<UserSkill> userSkills)
    {
        _dbContext.UsersSkills.AddRange(userSkills);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _dbContext.Users.AnyAsync(user => user.Id == id);
    }
}