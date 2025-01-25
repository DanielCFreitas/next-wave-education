using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.SetUserProfilePicture;

public class SetUserProfilePictureHandler()
    : IRequestHandler<SetUserProfilePictureCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(SetUserProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var fileName = request.FileName;
        var fileLength = request.FileLength;

        var description = $"File: {fileName}, Size: {fileLength}";

        Console.WriteLine(description);

        return ResultViewModel.Success();
    }
}