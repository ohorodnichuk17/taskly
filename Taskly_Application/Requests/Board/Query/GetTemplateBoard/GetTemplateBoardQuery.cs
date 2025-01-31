using ErrorOr;
using MediatR;
using Taskly_Application.DTO;
using Taskly_Application.DTO.TemplateBoardDTOs;

namespace Taskly_Application.Requests.Board.Query.GetTemplateBoard;

public record GetTemplateBoardQuery() 
    : IRequest<ErrorOr<TemplateBoardDto>>;