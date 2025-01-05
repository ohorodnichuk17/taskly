using Taskly_Domain.Entities;

namespace Taskly_Api.Request.Table;

public record CreateToDoTableItemRequest(string Task, 
                                     string Status, 
                                     string Label, 
                                     List<Guid> Members,
                                     DateTime EndTime, 
                                     Guid TableId);
