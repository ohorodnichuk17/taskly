using MediatR;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using Taskly_Api.SignalR.Models.CardCommentsHub;
using Taskly_Application.Requests.Authentication.Query.GetUserInformationById;
using Taskly_Application.Requests.CardComment.Command.LeaveCardComment;

namespace Taskly_Api.SignalR.Hubs;

public class CardCommentsHub(ISender sender) : Hub
{
    public async Task ConnectToCardComments(ConnectToCardCommentsModel model)
    {
        await Groups
            .AddToGroupAsync(Context.ConnectionId,model.CardId.ToString());
        await Clients
            .Group(model.CardId.ToString())
            .SendAsync("ConnectToCardComments", $"User with id ({model.UserId.ToString()}) has been connect to card comments with id ({model.CardId.ToString()})");
    }
    public async Task DisconnectFromCardComments(ConnectToCardCommentsModel model)
    {
        await Groups
            .RemoveFromGroupAsync(Context.ConnectionId, model.CardId.ToString());

        await Clients
            .Group(model.CardId.ToString())
            .SendAsync("DisconnectFromCardComments", $"User with id ({model.UserId.ToString()}) has been disconnect from card comments with id ({model.CardId.ToString()})");
    }
    public async Task LeaveComment(LeaveCommentModel model)
    {
        var user = await sender.Send(new GetUserInformationByIdQuery(model.UserId));

        if (user.IsError == false)
        {
            await Clients
           .Group(model.CardId.ToString())
           .SendAsync("DisconnectFromCardComments", new
           {
               CardId = model.CardId,
               UserId = model.UserId,
               Text = model.Text,
               UserName = user.Value.UserName,
               Avatar = user.Value.Avatar!.ImagePath
           });

            await sender.Send(new LeaveCardCommentCommand(
                model.CardId,
                model.UserId,
                model.Text
            ));
        }
        /*user.MatchAsync(async user =>
        {
            await Clients
           .Group(model.CardId.ToString())
           .SendAsync("DisconnectFromCardComments", new
           {
               CardId = model.CardId,
               UserId = model.UserId,
               Text = model.Text,
               UserName = user.UserName,
               Avatar = user.Avatar!.ImagePath
           });
            return Task.CompletedTask;
        }, async error => {
            Log.Error(error.ToString());
            return Task.CompletedTask;
        });*/
        
    }
}
