using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taskly_Api.Request.Table;
using Taskly_Api.Response.Table;
using Taskly_Application.Requests.Table.Command.CreateToDoTableItem;
using Taskly_Application.Requests.Table.Query.GetAllToDoTableItemsByTableId;
using Taskly_Domain.Entities;

namespace Taskly_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController(ISender sender, IMapper mapper) : ApiController
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        [HttpGet("get-all-table-items")]
        public async Task<IActionResult> GetAllToDoTableItems([FromQuery] Guid ToDoTableId)
        {
            var result = await _sender.Send(new GetAllToDoTableItemsByTableIdQuery(ToDoTableId));
            return result.Match(result => Ok(_mapper.Map<ICollection<TableItemResponse>>(result)),
                errors => Problem(errors)); 
        }
        [HttpPost("create-table-item")]
        public async Task<IActionResult> CreateToDoTableItem([FromBody] CreateToDoTableItemRequest createTableItemRequest)
        {
            var result = await _sender.Send(_mapper.Map<CreateToDoTableItemCommand>(createTableItemRequest));
            return result.Match(result => Ok(result),
                errors => Problem(errors));
        }
    }
}
