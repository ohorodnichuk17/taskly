using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Table;
using Taskly_Api.Response.Table;
using Taskly_Application.Requests.Table.Command.CreateToDoTable;
using Taskly_Application.Requests.Table.Command.CreateToDoTableItem;
using Taskly_Application.Requests.Table.Query.GetAllToDoTableItemsByTableId;
using Taskly_Application.Requests.Table.Query.GetAllToDoTables;
using Taskly_Application.Requests.Table.Query.GetToDoTablesByUserId;

namespace Taskly_Api.Controllers
{
    [Route("api/table")]
    [ApiController]
    public class TableController(ISender sender, IMapper mapper) : ApiController
    {
        [HttpPost("create-table")]
        public async Task<IActionResult> CreateToDoTable([FromBody] CreateToDoTableRuquest createTableRequest)
        {
            var result = await sender.Send(mapper.Map<CreateToDoTableCommand>(createTableRequest));
            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }
        [HttpGet("get-all-table-items")]
        public async Task<IActionResult> GetAllToDoTableItems([FromQuery] Guid toDoTableId)
        {
            var result = await sender.Send(new GetAllToDoTableItemsByTableIdQuery(toDoTableId));
            return result.Match(result => Ok(mapper.Map<ICollection<TableItemResponse>>(result)),
                errors => Problem(errors)); 
        }
        [HttpGet("get-all-tables")]
        public async Task<IActionResult> GetAllToDoTables()
        {
            var result = await sender.Send(new GetAllToDoTablesQuery());
            return result.Match(result => Ok(mapper.Map<ICollection<TableResponse>>(result)),
                errors => Problem(errors));
        }
        [HttpGet("get-tables-by-user-id")]
        public async Task<IActionResult> GetToDoTablesByUserId([FromQuery] Guid userId)
        {
            var result = await sender.Send(new GetToDoTablesByUserIdQuery(userId));
            return result.Match(result => Ok(mapper.Map<ICollection<TableResponse>>(result)),
                errors => Problem(errors));
        }
        [HttpPost("create-table-item")]
        public async Task<IActionResult> CreateToDoTableItem([FromBody] CreateToDoTableItemRequest createTableItemRequest)
        {
            var result = await sender.Send(mapper.Map<CreateToDoTableItemCommand>(createTableItemRequest));
            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }
    }
}
