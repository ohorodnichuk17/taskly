namespace Taskly_Api.Response.Card;

public record CreatedCardResponse(Guid CardId, Guid CardListId, string Task, DateTime Deadline, Guid UserId, string UserAvatar, string UserName);
