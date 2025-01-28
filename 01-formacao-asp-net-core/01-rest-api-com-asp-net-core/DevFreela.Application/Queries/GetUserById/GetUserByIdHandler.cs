using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetUserById;

public class GetUserByIdHandler :
    IRequestHandler<GetUserByIdQuery, ResultViewModel<UserViewModel>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ResultViewModel<UserViewModel>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdWithSkills(request.Id);

        if (user is null) return ResultViewModel<UserViewModel>.Error("Usuario nao encontrado");

        var model = UserViewModel.FromEntity(user);

        return ResultViewModel<UserViewModel>.Success(model);
    }
}