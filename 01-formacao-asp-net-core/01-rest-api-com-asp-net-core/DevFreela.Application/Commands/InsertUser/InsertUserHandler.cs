using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertUser;

public class InsertUserHandler(DevFreelaDbContext dbContext) :
    IRequestHandler<InsertUserCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.FullName, request.Email, request.BirthDate);

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        
        return ResultViewModel.Success();
    }
}