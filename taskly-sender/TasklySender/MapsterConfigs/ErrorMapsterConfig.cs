using ErrorOr;
using Mapster;
using TasklySender_Domain.Common;

namespace TasklySender.MapsterConfigs;

public class ErrorMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Error, CustomError>()
            .Map(src => src.Code, desp => desp.Code)
            .Map(src => src.Description, desp => desp.Description);
    }
}
