using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Gemini.Command.SummarizeText;

public class SummarizeTextCommandHandler(IGeminiApiClient apiClient)
    : IRequestHandler<SummarizeTextCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(SummarizeTextCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await apiClient.SummarizeTextAsync(request.Text);
        }
        catch (Exception ex)
        {
            return Error.Failure(ex.Message);
        }
    }
}