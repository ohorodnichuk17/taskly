using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Query.GetTemplatesOfBoard;

public record GetTemplatesOfBoardQuery: IRequest<BoardTemplateEntity[]>;
