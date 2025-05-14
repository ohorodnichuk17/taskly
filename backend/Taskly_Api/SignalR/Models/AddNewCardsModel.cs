namespace Taskly_Api.SignalR.Models;

public record AddNewCardsModel(Guid BoardId, ICollection<CardModel> CardModels);
