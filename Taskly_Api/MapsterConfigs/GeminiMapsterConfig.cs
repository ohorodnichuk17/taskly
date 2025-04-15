using Mapster;
using Taskly_Api.Request.Gemini;
using Taskly_Application.Requests.Gemini.Command.CreateCardsForTask;

namespace Taskly_Api.MapsterConfigs;

public class GeminiMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateCardsForTaskRequest request, Guid userId), CreateCardsForTaskCommand>()
            .Map(src => src.BoardId, desp => desp.request.BoardId)
            .Map(src => src.Task, desp => desp.request.Task)
            .Map(src => src.UserId, desp => desp.userId);
    }
}
