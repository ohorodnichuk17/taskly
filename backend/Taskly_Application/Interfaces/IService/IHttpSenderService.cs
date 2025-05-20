using ErrorOr;

namespace Taskly_Application.Interfaces.IService;

public interface IHttpSenderService
{
    Task<ErrorOr<string>> SendRequestAsync(string TypeOfHTML, string To, Dictionary<string, string> Props);
}
