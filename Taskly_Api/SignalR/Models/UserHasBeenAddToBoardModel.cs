namespace Taskly_Api.SignalR.Models;

public record UserHasBeenAddToBoardModel(Guid BoardId, Guid AddedUserId,string AddedUserEmail, string AddedUserAvatarName, string UserEmailWhoAdd);
