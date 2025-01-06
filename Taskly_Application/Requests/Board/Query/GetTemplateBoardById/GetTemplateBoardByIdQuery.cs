using ErrorOr;
using MediatR;
using Taskly_Application.DTO;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Query.GetTemplateBoardById;

public record GetTemplateBoardByIdQuery(Guid Id) 
    : IRequest<ErrorOr<TemplateBoardDto>>;