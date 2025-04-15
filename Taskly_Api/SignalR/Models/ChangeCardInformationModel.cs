using Taskly_Domain.Other;
namespace Taskly_Api.SignalR.Models;

public record ChangeCardInformationModel(ChangeCardProps ChangeProps, Guid BoardId, Guid CardListId, Guid CardId, Guid UserId);

