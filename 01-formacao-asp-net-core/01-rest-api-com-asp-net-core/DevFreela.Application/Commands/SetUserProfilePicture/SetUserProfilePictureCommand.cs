using DevFreela.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DevFreela.Application.Commands.SetUserProfilePicture;

public class SetUserProfilePictureCommand(string fileName, long fileLength) : IRequest<ResultViewModel>
{
    public string FileName { get; set; } = fileName;
    public long FileLength { get; set; } = fileLength;
}