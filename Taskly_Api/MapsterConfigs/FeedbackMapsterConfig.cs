using Mapster;
using Taskly_Api.Request.Feedback;
using Taskly_Api.Response.Feedback;
using Taskly_Application.Requests.Feedback.Command.Create;
using Taskly_Domain.Entities;

namespace Taskly_Api.MapsterConfigs;

public class FeedbackMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateFeedbackRequest, CreateFeedbackCommand>()
            .Map(src => src.UserId, desp => desp.UserId)
            .Map(src => src.Rating, desp => desp.Rating)
            .Map(src => src.Review, desp => desp.Review);

        config.NewConfig<FeedbackEntity, FeedbackResponse>()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.UserId, desp => desp.UserId)
            .Map(src => src.Rating, desp => desp.Rating)
            .Map(src => src.Review, desp => desp.Review)
            .Map(src => src.CreatedAt, desp => desp.CreatedAt);
    }
}