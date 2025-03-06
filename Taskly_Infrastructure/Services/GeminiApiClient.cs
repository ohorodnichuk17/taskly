using System.Text;
using Newtonsoft.Json;
using Taskly_Application.Gemini.ContentRequest;
using Taskly_Application.Gemini.ContentResponse;
using Taskly_Application.Interfaces.IService;
using Content = Taskly_Application.Gemini.ContentRequest.Content;
using Part = Taskly_Application.Gemini.ContentRequest.Part;

namespace Taskly_Infrastructure.Services;

public class GeminiApiClient(string apiKey, string url) : IGeminiApiClient
{
    private readonly HttpClient _httpClient = new();

    public async Task<string> GenerateContentAsync(string prompt)
    {
        string url1 = $"{url}?key={apiKey}";
        var request = new ContentRequest
        {
            contents = new[]
            {
                new Content
                {
                    parts = new[]
                    {
                        new Part
                        {
                            text = prompt
                        }
                    }
                }
            }
        };
        string jsonRequest = JsonConvert.SerializeObject(request);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(url1, content);

        if (response.IsSuccessStatusCode)
        {
            string jsonResponse = await response.Content.ReadAsStringAsync();
            var geminiResponse = JsonConvert.DeserializeObject<ContentResponse>(jsonResponse);
            return geminiResponse.Candidates[0].Content.Parts[0].Text;
        }
        else
        {
            string errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Gemini API Error: {response.StatusCode} - {errorMessage}");
        }
    }
    
    public async Task<string> GenerateTaskImprovementSuggestionsAsync(string taskDescription)
    {
        string prompt = $"Here's a description of the task: {taskDescription}. How can you improve this task by adding more details or hints for completion?";
        return await GenerateContentAsync(prompt);
    }

    public async Task<string> GenerateDeadlineSuggestionsAsync(string taskDescription)
    {
        string prompt = $"Here's a description of the task: {taskDescription}. What deadline would you suggest for this task?";
        return await GenerateContentAsync(prompt);
    }

    public async Task<string> TranslateTextAsync(string sourceLanguage, string targetLanguage, string text)
    {
        string prompt = $"Translate the following text from {sourceLanguage} to {targetLanguage}:\n{text}";
        return await GenerateContentAsync(prompt);
    }

    public async Task<string> SummarizeTextAsync(string text)
    {
        string prompt = $"Summarize the following text in a concise manner:\n{text}";
        return await GenerateContentAsync(prompt);
    }

    public async Task<List<string>> CreateCardsForTask(string Task)
    {
        string prompt = $"Divide tasks into the most important stages of development in the form of a list, for a task board for other users (briefly, without further ado, just a list) : . {Task}";
        var result = await GenerateContentAsync(prompt);
        List<string> cards = result.Split(new[] { '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                  .Select(item => item.Replace("*", "").Trim())
                  .ToList();
        return cards;
    }
}