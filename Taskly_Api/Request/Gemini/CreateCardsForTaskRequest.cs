namespace Taskly_Api.Request.Gemini;

public record CreateCardsForTaskRequest(Guid BoardId,string Task);
