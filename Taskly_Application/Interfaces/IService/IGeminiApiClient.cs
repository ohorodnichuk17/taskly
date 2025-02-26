namespace Taskly_Application.Interfaces.IService;

public interface IGeminiApiClient
{
    Task<string> GenerateContentAsync(string prompt);
    Task<string> GenerateTaskImprovementSuggestionsAsync(string taskDescription);
    Task<string> GenerateDeadlineSuggestionsAsync(string taskDescription);
    Task<string> TranslateTextAsync(string sourceLanguage, string targetLanguage, string text);
    Task<string> SummarizeTextAsync(string text);
}