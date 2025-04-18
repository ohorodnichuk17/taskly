using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Table;
using Taskly_Api.Response.Table;
using Taskly_Application.Requests.Table.Command.CreateTable;
using Taskly_Application.Requests.Table.Command.CreateTableItem;
using Taskly_Application.Requests.Table.Command.DeleteTable;
using Taskly_Application.Requests.Table.Command.EditTable;
using Taskly_Application.Requests.Table.Query.GetAllTableItemsByTableId;
using Taskly_Application.Requests.Table.Query.GetAllTables;
using Taskly_Application.Requests.Table.Query.GetTableById;
using Taskly_Application.Requests.Table.Query.GetTablesByUserId;

namespace Taskly_Api.Controllers
{
    [Route("api/table")]
    [ApiController]
    public class TableController(ISender sender, IMapper mapper) : ApiController
    {
        [HttpPost("create-table")]
        public async Task<IActionResult> CreateTable([FromBody] CreateTableRuquest createTableRequest)
        {
            var result = await sender.Send(mapper.Map<CreateTableCommand>(createTableRequest));
            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }
        [HttpGet("get-all-table-items")]
        public async Task<IActionResult> GetAllTableItems([FromQuery] Guid toDoTableId)
        {
            var result = await sender.Send(new GetAllTableItemsByTableIdQuery(toDoTableId));
            return result.Match(result => Ok(mapper.Map<ICollection<TableItemResponse>>(result)),
                errors => Problem(errors)); 
        }
        [HttpGet("get-all-tables")]
        public async Task<IActionResult> GetAllTables()
        {
            var result = await sender.Send(new GetAllTablesQuery());
            return result.Match(result => Ok(mapper.Map<ICollection<TableResponse>>(result)),
                errors => Problem(errors));
        }
        [HttpGet("get-tables-by-user-id")]
        public async Task<IActionResult> GetTablesByUserId([FromQuery] Guid userId)
        {
            var result = await sender.Send(new GetTablesByUserIdQuery(userId));
            return result.Match(result => Ok(mapper.Map<ICollection<TableResponse>>(result)),
                errors => Problem(errors));
        }
        [HttpGet("get-table-by-id")]
        public async Task<IActionResult> GetTableById([FromQuery] Guid tableId)
        {
            var result = await sender.Send(new GetTableByIdQuery(tableId));
            return result.Match(result => Ok(mapper.Map<TableResponse>(result)),
                errors => Problem(errors));
        }
        [HttpPost("create-table-item")]
        public async Task<IActionResult> CreateTableItem([FromBody] CreateTableItemRequest createTableItemRequest)
        {
            var result = await sender.Send(mapper.Map<CreateTableItemCommand>(createTableItemRequest));
            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }

        [HttpDelete("delete-table")]
        public async Task<IActionResult> DeleteTable([FromQuery] Guid tableId)
        {
            var result = await sender.Send(new DeleteTableCommand(tableId));
            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPut("edit-table")]
        public async Task<IActionResult> EditTable([FromBody] EditTableRequest editTableRequest)
        {
            var result = await sender.Send(mapper.Map<EditTableCommand>(editTableRequest));
            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }
    }
}
