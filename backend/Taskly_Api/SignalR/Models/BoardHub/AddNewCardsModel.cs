namespace Taskly_Api.SignalR.Models.BoardHub;

public record AddNewCardsModel(Guid BoardId, ICollection<CardModel> CardModels);
