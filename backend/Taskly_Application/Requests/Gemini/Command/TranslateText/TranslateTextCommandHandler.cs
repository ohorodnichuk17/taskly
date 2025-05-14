using Taskly_Application.Interfaces.IService;
using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Gemini.Command.TranslateText;

public class TranslateTextCommandHandler(IGeminiApiClient apiClient)
    : IRequestHandler<TranslateTextCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(TranslateTextCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return await apiClient.TranslateTextAsync(
                request.SourceLanguage, request.TargetLanguage, request.Text);
        }
        catch (Exception ex)
        {
            return Error.Failure(ex.Message);
        }
    }
}