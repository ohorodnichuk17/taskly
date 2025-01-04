using Mapster;
using Taskly_Api.Request.Authenticate;
using Taskly_Api.Request.Table;
using Taskly_Api.Response.Table;
using Taskly_Application.Requests.Table.Command.CreateToDoTableItem;
using Taskly_Domain.Entities;

namespace Taskly_Api.MapsterConfigs;

public static class TableMapsterConfig
{
    public static void Config()
    {
        TypeAdapterConfig<ToDoItemEntity,TableItemResponse>.NewConfig()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Task, desp => desp.Text)
            .Map(src => src.Status, desp => desp.Status)
            .Map(src => src.Label, desp => desp.Label)
            .Map(src => src.Members, desp => desp.Members.Adapt<UserForTableItemResponse>())
            .Map(src => src.StartTime, desp => desp.TimeRange!.StartTime)
            .Map(src => src.EndTime, desp => desp.TimeRange!.EndTime);

        TypeAdapterConfig<CreateToDoTableItemRequest, CreateToDoTableItemCommand>.NewConfig()
            .Map(src => src.Task, desp => desp.Task)
            .Map(src => src.Status, desp => desp.Status)
            .Map(src => src.Label, desp => desp.Label)
            .Map(src => src.Members, desp => desp.Members)
            .Map(src => src.EndTime, desp => desp.EndTime)
            .Map(src => src.Members, desp => desp.Members);

    }
}
