namespace Taskly_Api.Request.Card;

public record CreateCardRequest(Guid CardListId, string Task, DateTime Deadline, Guid? UserId);
