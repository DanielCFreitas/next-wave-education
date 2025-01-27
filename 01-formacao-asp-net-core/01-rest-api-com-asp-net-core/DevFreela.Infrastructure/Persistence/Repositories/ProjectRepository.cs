using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Project>> GetAll()
    {
        return await _dbContext.Projects
            .Include(project => project.Client)
            .Include(project => project.Freelancer)
            .Where(project => !project.IsDeleted)
            .ToListAsync();
    }

    public async Task<Project?> GetById(int id)
    {
        return await _dbContext.Projects.FindAsync(id);
    }

    public async Task<Project?> GetDetailsById(int id)
    {
        return await _dbContext.Projects
            .Include(project => project.Client)
            .Include(project => project.Freelancer)
            .Include(project => project.Comments)
            .SingleOrDefaultAsync(project => project.Id == id);
    }

    public async Task<int> Add(Project project)
    {
        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();

        return project.Id;
    }

    public async Task Update(Project project)
    {
        _dbContext.Projects.Update(project);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddComment(ProjectComment comment)
    {
        await _dbContext.ProjectComments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _dbContext.Projects.AnyAsync(project => project.Id == id);
    }
}