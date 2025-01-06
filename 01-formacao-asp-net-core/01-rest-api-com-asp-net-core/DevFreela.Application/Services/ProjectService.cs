using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.Application.Services;

public class ProjectService : IProjectService
{
    private readonly DevFreelaDbContext _context;

    public ProjectService(DevFreelaDbContext context)
    {
        _context = context;
    }

    public ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "")
    {
        var projects =
            _context.Projects
                .Include(project => project.Client)
                .Include(project => project.Freelancer)
                .Where(project => !project.IsDeleted && (search.IsNullOrEmpty() || project.Title.Contains(search) ||
                                                         project.Description.Contains(search)))
                .ToList();

        var model = projects
            .Select(ProjectItemViewModel.FromEntity)
            .ToList();

        return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }

    public ResultViewModel<ProjectItemViewModel> GetById(int id)
    {
        var project = _context.Projects
            .Include(project => project.Client)
            .Include(project => project.Freelancer)
            .Include(project => project.Comments)
            .SingleOrDefault(project => project.Id == id);

        if (project is null)
            return ResultViewModel<ProjectItemViewModel>.Error("Projeto não existe");

        var model = ProjectItemViewModel.FromEntity(project);

        return ResultViewModel<ProjectItemViewModel>.Success(model);
    }

    public ResultViewModel<int> Insert(CreateProjectInputModel model)
    {
        var project = model.ToEntity();

        _context.Projects.Add(project);
        _context.SaveChanges();

        return ResultViewModel<int>.Success(project.Id);
    }

    public ResultViewModel Update(UpdateProjectInputModel model)
    {
        var project = _context.Projects.SingleOrDefault(project => project.Id == model.IdProject);

        if (project is null) return ResultViewModel.Error("Projeto não encontrado");

        project.Update(model.Title, model.Description, model.TotalCost);

        _context.Projects.Update(project);
        _context.SaveChanges();
        
        return ResultViewModel.Success();
    }

    public ResultViewModel Delete(int id)
    {
        var project = _context.Projects.SingleOrDefault(project => project.Id == id);

        if (project is null) return ResultViewModel.Error("Projeto não encontrado");

        project.SetAsDeleted();
        _context.Projects.Update(project);
        _context.SaveChanges();
        
        return ResultViewModel.Success();
    }

    public ResultViewModel Start(int id)
    {
        var project = _context.Projects.SingleOrDefault(project => project.Id == id);

        if (project is null) return ResultViewModel.Error("Projeto não encontrado");

        project.Start();
        _context.Projects.Update(project);
        _context.SaveChanges();
        
        return ResultViewModel.Success();
    }

    public ResultViewModel Complete(int id)
    {
        var project = _context.Projects.SingleOrDefault(project => project.Id == id);

        if (project is null) return ResultViewModel.Error("Projeto não encontrado");

        project.Complete();
        _context.Projects.Update(project);
        _context.SaveChanges();
        
        return ResultViewModel.Success();
    }

    public ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model)
    {
        var project = _context.Projects.SingleOrDefault(project => project.Id == id);

        if (project is null) return ResultViewModel.Error("Projeto não encontrado");

        var comment = new ProjectComment(model.Content, model.IdProject, model.IdUser);

        _context.ProjectComments.Add(comment);
        _context.SaveChanges();
        
        return ResultViewModel.Success();
    }
}