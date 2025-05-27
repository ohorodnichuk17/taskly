namespace Taskly_Api.SignalR.Models.BoardHub;

public record UserHasBeenAddToBoardModel(Guid BoardId, Guid AddedUserId, string AddedUserEmail, string AddedUserAvatarName, string UserEmailWhoAdd);
