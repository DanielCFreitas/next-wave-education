using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.InsertComment;

public class InsertCommentCommand(string content, int idProject, int idUser) : IRequest<ResultViewModel>
{
    public string Content { get; set; } = content;
    public int IdProject { get; set; } = idProject;
    public int IdUser { get; set; } = idUser;
}