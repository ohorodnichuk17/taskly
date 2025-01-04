// using ErrorOr;
// using MediatR;
// using Taskly_Application.Interfaces;
// using Taskly_Application.Interfaces.IService;
// using Taskly_Domain.Entities;
//
// namespace Taskly_Application.Requests.Board.Command.Create;
//
// public class CreateBoardCommandHandler(IUnitOfWork unitOfWork, 
//     ICurrentUserService currentUserService) 
//     : IRequestHandler<CreateBoardCommand, ErrorOr<BoardEntity>>
// {
//     public async Task<ErrorOr<BoardEntity>> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
//     {
//         var currentUserId = currentUserService.GetCurrentUserId();
//         if (!Guid.TryParse(currentUserId, out var currentUserGuid))
//             return Error.Validation("Invalid user ID format.");
//         // var user = await currentUserService.GetUserById(currentUserGuid);
//         
//         
//     }
// }