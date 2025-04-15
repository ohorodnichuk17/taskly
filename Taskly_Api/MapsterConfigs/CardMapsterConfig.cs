using Mapster;
using System.Collections.Generic;
using Taskly_Api.Request.Card;
using Taskly_Api.Response.Card;
using Taskly_Application.Requests.Card.Command.TransferCardToAnotherCardList;
using Taskly_Domain.Entities;

namespace Taskly_Api.MapsterConfigs;

public class CardMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CommentEntity, CommentResponse>()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Text, desp => desp.Text)
            .Map(src => src.CreatedAt, desp => desp.CreatedAt);

        config.NewConfig<CardEntity, CardResponse>()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Description, desp => desp.Description)
            .Map(src => src.AttachmentUrl, desp => desp.AttachmentUrl)
            .Map(src => src.Status, desp => desp.Status)
            .Map(src => src.UserId, desp => desp.UserId)
            .Map(src => src.UserAvatar, desp => desp.User == null ? null : desp.User.Avatar!.ImagePath)
            .Map(src => src.UserName, desp => desp.User == null ? null : desp.User.UserName)
            .Map(src => src.IsCompleated, desp => desp.IsCompleated)
            .Map(src => src.StartTime, desp => desp.TimeRangeEntity!.StartTime)
            .Map(src => src.EndTime, desp => desp.TimeRangeEntity!.EndTime)
            .Map(src => src.Comments, desp => desp.Comments!.ToArray().Adapt<CommentResponse[]>());

        config.NewConfig<CardListEntity, CardListResponse>()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Cards, desp => desp.Cards.ToArray().Adapt<CardResponse[]>())
            .Map(src => src.BoardId, desp => desp.BoardId);

        config.NewConfig<TransferCardToAnotherCardListRequest, TransferCardToAnotherCardListCommand>()
            .Map(src => src.CardId, desp => desp.CardId)
            .Map(src => src.ToCardListId, desp => desp.AnotherCardListId);
    }
}
