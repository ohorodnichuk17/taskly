using Microsoft.AspNetCore.SignalR;
using Taskly_Api.SignalR.Models;

namespace Taskly_Api.SignalR.Hubs;

public class BoardHub : Hub
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
                new TransferInformationModel(
                                         UserId : model.UserId,
                                         CardId : model.CardId,
                                         FromCardListId : model.FromCardListId,
                                         ToCardListId : model.ToCardListId));
        /*await Clients
            .Groups(model.BoardId.ToString())
            .SendAsync("TransferCardToAnotherCardList",
                "Test message");*/
    }
}
