using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Taskly_Infrastructure")]

namespace Taskly_Application.Gemini.ContentResponse;

internal sealed class ContentResponse
{
    public Candidate[] Candidates { get; set; }
    public PromptFeedback PromptFeedback { get; set; }
}