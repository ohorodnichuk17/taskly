using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Gemini.Command.GenerateBase;

public class GenerateBaseCommandHandler(IGeminiApiClient apiClient) : IRequestHandler<GenerateBaseCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(GenerateBaseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await apiClient.GenerateContentAsync(request.prompt);
        }
        catch (Exception ex)
        {
            return Error.Failure(ex.Message);
        }
    }
}