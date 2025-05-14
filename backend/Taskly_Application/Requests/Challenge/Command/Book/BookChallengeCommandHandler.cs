using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Challenge.Command.Book;

public class BookChallengeCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<BookChallengeCommand, ErrorOr<bool>>
{
    public async Task<ErrorOr<bool>> Handle(BookChallengeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.Challenges.BookChallengeAsync(request.ChallengeId, request.UserId);
            return true;
        }
        catch (Exception ex)
        {
            return Error.Failure("BookChallengeError", ex.Message);
        }
    }
}