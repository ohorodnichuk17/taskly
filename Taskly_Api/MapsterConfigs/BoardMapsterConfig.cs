using Mapster;
using Taskly_Api.Response.Board;
using Taskly_Domain.Entities;

namespace Taskly_Api.MapsterConfigs;

public class BoardMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BoardEntity, UsersBoardResponse>()
            .Map(src => src.Id, desp => desp.Id)
            .Map(src => src.Name, desp => desp.Name)
            .Map(src => src.CountOfMembers, desp => desp.Members == null ? 0 : desp.Members.Count)
            .Map(src => src.BoardTemplateName, desp => desp.BoardTemplate!.Name)
            .Map(src => src.BoardTemplateColor, desp => desp.BoardTemplate!.ImagePath);
    }
}
