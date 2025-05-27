using Mapster;
using Taskly_Api.Response.Card;
using Taskly_Api.Response.CardComments;
using Taskly_Domain.Entities;

namespace Taskly_Api.MapsterConfigs;

public class CommentsMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CardCommentEntity, CardCommentResponse>()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Text, desp => desp.Text)
            .Map(src => src.UserId, desp => desp.UserId)
            .Map(src => src.UserName, desp => desp.User!.UserName)
            .Map(src => src.UserAvatar, desp => desp.User!.Avatar!.ImagePath)
            .Map(src => src.CreatedAt, desp => desp.CreatedAt);
    }
}
