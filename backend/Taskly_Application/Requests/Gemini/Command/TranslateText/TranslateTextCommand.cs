using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Gemini.Command.TranslateText;

public record TranslateTextCommand(
    string SourceLanguage,
    string TargetLanguage,
    string Text) : IRequest<ErrorOr<string>>;