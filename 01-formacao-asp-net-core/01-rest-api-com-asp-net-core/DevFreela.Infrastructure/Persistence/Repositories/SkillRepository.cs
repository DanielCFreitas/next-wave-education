using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public SkillRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Skill>> GetAll()
    {
        return await _dbContext.Skills
            .Include(skill => skill.UserSkills)
                .ThenInclude(user => user.User)
            .ToListAsync();
    }

    public Task Add(Skill skill)
    {
        _dbContext.Skills.Add(skill);
        return _dbContext.SaveChangesAsync();
    }
}