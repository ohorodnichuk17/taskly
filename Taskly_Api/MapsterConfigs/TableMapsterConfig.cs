using Mapster;
using Taskly_Api.Request.Authenticate;
using Taskly_Api.Request.Table;
using Taskly_Api.Response.Table;
using Taskly_Application.Requests.Table.Command.CreateTable;
using Taskly_Application.Requests.Table.Command.CreateTableItem;
using Taskly_Domain.Entities;

namespace Taskly_Api.MapsterConfigs;

public class TableMapsterConfig : IRegister
{
    /*public static void Config()
    {
        TypeAdapterConfig<ItemEntity,TableItemResponse>.NewConfig()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Task, desp => desp.Text)
            .Map(src => src.Status, desp => desp.Status)
            .Map(src => src.Label, desp => desp.Label)
            .Map(src => src.Members, desp => desp.Members.Adapt<UserForTableItemResponse>())
            .Map(src => src.StartTime, desp => desp.TimeRange!.StartTime)
            .Map(src => src.EndTime, desp => desp.TimeRange!.EndTime);

        TypeAdapterConfig<CreateTableItemRequest, CreateTableItemCommand>.NewConfig()
            .Map(src => src.Task, desp => desp.Task)
            .Map(src => src.Status, desp => desp.Status)
            .Map(src => src.Label, desp => desp.Label)
            .Map(src => src.Members, desp => desp.Members)
            .Map(src => src.EndTime, desp => desp.EndTime)
            .Map(src => src.Members, desp => desp.Members)
            .Map(src => src.TableId, desp => desp.TableId);

        TypeAdapterConfig<CreateTableRuquest, CreateTableCommand>.NewConfig()
            .Map(src => src.UserId, desp => desp.UserId);
}*/

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TableItemEntity, TableItemResponse>()
           .Map(src => src.Id, desp => desp.Id)
           .Map(src => src.Task, desp => desp.Text)
           .Map(src => src.Status, desp => desp.Status)
           .Map(src => src.Label, desp => desp.Label)
           .Map(src => src.Members, desp => desp.Members.Adapt<UserForTableItemResponse>())
           .Map(src => src.StartTime, desp => desp.TimeRange!.StartTime)
           .Map(src => src.EndTime, desp => desp.TimeRange!.EndTime);

        config.NewConfig<CreateTableItemRequest, CreateTableItemCommand>()
           .Map(src => src.Task, desp => desp.Task)
            .Map(src => src.Status, desp => desp.Status)
            .Map(src => src.Label, desp => desp.Label)
            .Map(src => src.Members, desp => desp.Members)
            .Map(src => src.EndTime, desp => desp.EndTime)
            .Map(src => src.Members, desp => desp.Members)
            .Map(src => src.TableId, desp => desp.TableId);

        config.NewConfig<CreateTableRuquest, CreateTableCommand>()
           .Map(src => src.UserId, desp => desp.UserId);

    }

}
