using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetUserById;

public class GetUserByIdHandler(DevFreelaDbContext dbContext) :
    IRequestHandler<GetUserByIdQuery, ResultViewModel<UserViewModel>>
{
    public async Task<ResultViewModel<UserViewModel>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Include(user => user.Skills)
            .ThenInclude(skill => skill.Skill)
            .SingleOrDefaultAsync(u => u.Id == request.Id);

        if (user is null) return ResultViewModel<UserViewModel>.Error("Usuario nao encontrado");

        var model = UserViewModel.FromEntity(user);

        return ResultViewModel<UserViewModel>.Success(model);
    }
}