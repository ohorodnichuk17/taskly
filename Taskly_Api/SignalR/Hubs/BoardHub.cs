using MediatR;
using Microsoft.AspNetCore.SignalR;
using Taskly_Api.Request.Card;
using Taskly_Api.SignalR.Models;
using Taskly_Application.Requests.Card.Command.ChangeCard;
using Taskly_Application.Requests.Card.Command.LeaveCard;
using Taskly_Application.Requests.Card.Command.RemoveCard;
using Taskly_Application.Requests.Card.Command.TakeCard;
using Taskly_Application.Requests.Card.Command.TransferCardToAnotherCardList;

namespace Taskly_Api.SignalR.Hubs;

public class BoardHub(ISender sender) : Hub
{
    public async Task ConnectToTeamBoard(ConnectToTeamBoardModel model)
    {
        await Groups.
            AddToGroupAsync(Context.ConnectionId, model.BoardId.ToString());
        await Clients.
            Group(model.BoardId.ToString()).
            SendAsync("ConnectToTeamBoard", $"User with ID: {model.UserId} has been connect to board with ID: {model.BoardId}");
    }
    public async Task DisconnectFromTeamBoard(DisconnectFromTeamBoardModel model)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId,model.BoardId.ToString());
        await Clients.
            Group(model.BoardId.ToString()).
            SendAsync("DisconnectFromTeamBoard", $"User with ID: {model.UserId} has been disconnect from board with ID: {model.BoardId}");
    }
    public async Task TransferCardToAnotherCardList(TransferCardToAnotherCardListModel model)
    {
        await Clients
            .Groups(model.BoardId.ToString())
            .SendAsync("TransferCardToAnotherCardList",
                new {
                    UserId = model.UserId,
                    CardId = model.CardId,
                    FromCardListId = model.FromCardListId,
                    ToCardListId = model.ToCardListId,
                    IsCompleated = model.IsCompleated});

        await sender.Send(new TransferCardToAnotherCardListCommand(
            ToCardListId: model.ToCardListId,
            CardId: model.CardId
            ));
    }
    public async Task RemoveCardFromCardList(RemoveCardFromCardList model)
    {
        await Clients
                .Groups(model.BoardId.ToString())
                .SendAsync("RemoveCardFromCardList", new {
                    CardListId = model.CardListId,
                    CardId = model.CardId,
                    UserId = model.UserId
                });
        await sender.Send(new RemoveCardCommand(CardId: model.CardId));
    }
    public async Task ChangeCardInformation(ChangeCardInformationModel model)
    {
        await Clients
            .Groups(model.BoardId.ToString())
            .SendAsync("ChangeCardInformation", new
            {
                CardListId = model.CardListId,
                CardId = model.CardId,
                UserId = model.UserId,
                ChangeProps = model.ChangeProps
            });

        await sender.Send(new ChangeCardCommand(CardId: model.CardId, ChangeCardProps: model.ChangeProps));
    }

    public async Task LeaveCard(LeaveCardModel model)
    {
        await Clients
            .Groups(model.BoardId.ToString())
            .SendAsync("LeaveCard", new
            {
                CardListId = model.CardListId,
                CardId = model.CardId,
                UserId = model.UserId
            });

        await sender.Send(new LeaveCardCommand(model.CardId));
    }
    public async Task TakeCard(LeaveCardModel model)
    {
        await Clients
            .Groups(model.BoardId.ToString())
            .SendAsync("TakeCard", new
            {
                CardListId = model.CardListId,
                CardId = model.CardId,
                UserId = model.UserId,
                UserName = model.UserName,
                UserAvatar = model.UserAvatar
            });

        await sender.Send(new TakeCardCommand(
            CardId: model.CardId,
            UserId: model.UserId));
    }
    public async Task AddNewCard(AddNewCardModel model)
    {
        await Clients
            .Groups(model.BoardId.ToString())
            .SendAsync("AddNewCard", new
            {
                CardListId = model.CardModel.CardListId,
                CardId = model.CardModel.CardId,
                Task = model.CardModel.Task,
                Deadline = model.CardModel.Deadline,
                UserId = model.CardModel.UserId,
                UserAvatar = model.CardModel.UserAvatar,
                UserName = model.CardModel.UserName
            });
    }
    public async Task AddNewCards(AddNewCardsModel model)
    {
        await Clients
            .Groups(model.BoardId.ToString())
            .SendAsync("AddNewCards", new
            {
                Cards = model.CardModels,               
            });
    }
    public async Task UserHasLeftBoard(UserHasLeftBoardModel model)
    {
        await Clients
            .Groups(model.BoardId.ToString())
            .SendAsync("UserHasLeftBoard", new
            {
                CardsId = model.CardsId
            });
    }
    public async Task UserHasBeenAddToBoard(UserHasBeenAddToBoardModel model)
    {
        Console.WriteLine($"{model.UserEmailWhoAdd} - {model.AddedUserEmail}");
        await Clients
            .Groups(model.BoardId.ToString())
            .SendAsync("UserHasBeenAddToBoard", new
            {
                AddedUserId = model.AddedUserId,
                AddedUserEmail = model.AddedUserEmail,
                AddedUserAvatarName = model.AddedUserAvatarName,
                UserEmailWhoAdd = model.UserEmailWhoAdd
            });
    }
    public async Task UserHasBeenRemovedFromBoard(UserHasBeenRemovedFromBoardModel model)
    {
        await Clients
            .Groups(model.BoardId.ToString())
            .SendAsync("UserHasBeenRemovedFromBoard", new
            {
                RemovedUserId = model.RemovedUserId,
                RemovedUserEmail = model.RemovedUserEmail,
                UserEmailWhoRemoved = model.UserEmailWhoRemoved,
                CardsId = model.CardsId
            });
    }
}
