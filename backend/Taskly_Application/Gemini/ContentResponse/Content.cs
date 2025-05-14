namespace Taskly_Application.Gemini.ContentResponse;

internal sealed class Content
{
    public Part[] Parts { get; set; }
    public string Role { get; set; }
}