using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Command.Create;

public record CreateBoardCommand(
    string Name,
    string Tag,
    bool IsTeamBoard,
    Guid BoardTemplateId,
    List<CardListEntity> CardLists) : IRequest<ErrorOr<Guid>>;