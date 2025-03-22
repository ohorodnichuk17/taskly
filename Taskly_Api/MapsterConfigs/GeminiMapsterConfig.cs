using Mapster;
using Taskly_Api.Request.Gemini;
using Taskly_Application.Requests.Gemini.Command.CreateCardsForTask;

namespace Taskly_Api.MapsterConfigs;

public class GeminiMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCardsForTaskRequest, CreateCardsForTaskCommand>()
            .Map(src => src.BoardId, desp => desp.BoardId)
            .Map(src => src.Task, desp => desp.Task);
    }
}
