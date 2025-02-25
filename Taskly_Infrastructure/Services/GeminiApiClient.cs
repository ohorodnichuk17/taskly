using System.Text;
using Newtonsoft.Json;
using Taskly_Application.Gemini.ContentRequest;
using Taskly_Application.Gemini.ContentResponse;
using Content = Taskly_Application.Gemini.ContentRequest.Content;
using Part = Taskly_Application.Gemini.ContentRequest.Part;

namespace Taskly_Infrastructure.Services;

public class GeminiApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _url;
    
    public GeminiApiClient(string apiKey, string url)
    {
        _httpClient = new HttpClient();
        _apiKey = apiKey;
        _url = url;
    }
    
    public async Task<string> GenerateContentAsync(string prompt)
    {
        string url = $"{_url}?key={_apiKey}";
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

        HttpResponseMessage response = await _httpClient.PostAsync(url, content);

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
}