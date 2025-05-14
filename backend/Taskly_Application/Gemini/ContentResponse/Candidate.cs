namespace Taskly_Application.Gemini.ContentResponse;

internal sealed class Candidate
{
    public Content Content { get; set; }
    public string FinishSession { get; set; }
    public int Index { get; set; }
    public SafetyRating[] SafetyRatings { get; set; }
}