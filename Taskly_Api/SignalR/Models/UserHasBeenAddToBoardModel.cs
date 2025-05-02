namespace Taskly_Api.SignalR.Models;

public record UserHasBeenAddToBoardModel(Guid BoardId, string AddedUserEmail, string UserEmailWhoAdd);
