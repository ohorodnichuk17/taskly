namespace Taskly_Api.SignalR.Models;

public record RemoveInformationModel(Guid CardListId, Guid CardId, Guid UserId);