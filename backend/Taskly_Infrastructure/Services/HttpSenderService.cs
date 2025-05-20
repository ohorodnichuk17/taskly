using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Text;
using System.Text.Json;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.ValueObjects;

namespace Taskly_Infrastructure.Services;

public class HttpSenderService(HttpClient httpClient, IOptions<SenderOptions> options) : IHttpSenderService
{
    private HttpClient HttpClient { get; set; } = httpClient;

    public async Task<ErrorOr<string>> SendRequestAsync(string TypeOfHTML, string To, Dictionary<string,string> Props)
    {
        var values = new 
        {
            TypeOfHTML= TypeOfHTML,
            To= To,
            Props = Props
        };
        var json = JsonSerializer.Serialize(values);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await HttpClient.PostAsync(options.Value.Url, content);
        var stringResult = await result.Content.ReadAsStringAsync();
        
        try
        {
            var jsonResult = JsonSerializer.Deserialize<SenderError>(stringResult);
            return Error.Conflict(jsonResult != null && jsonResult.Errors.Length > 0 ? jsonResult.Errors[0].Code! : "Something went wrong");
        }
        catch (Exception)
        {
            return await result.Content.ReadAsStringAsync();
        }
    }
}
