namespace Taskly_Application.Gemini.PromptRequest;

public record TranslateTextRequest(
    string SourceLanguage,
    string TargetLanguage,
    string Text);