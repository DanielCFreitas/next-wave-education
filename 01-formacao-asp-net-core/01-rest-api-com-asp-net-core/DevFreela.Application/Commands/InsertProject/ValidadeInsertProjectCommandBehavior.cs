using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertProject;

public class ValidadeInsertProjectCommandBehavior :
    IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
{
    private readonly DevFreelaDbContext _dbContext;

    public ValidadeInsertProjectCommandBehavior(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request,
        RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
    {
        var clientExists = await _dbContext.Users.AnyAsync(user => user.Id == request.IdClient);
        var freelancerExists = await _dbContext.Users.AnyAsync(user => user.Id == request.IdFreelancer);

        if (!clientExists || !freelancerExists) 
            return ResultViewModel<int>.Error("Cliente ou Freelancer inválidos");

        return await next();
    }
}