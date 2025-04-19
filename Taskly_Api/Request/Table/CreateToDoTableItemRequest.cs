using Taskly_Domain.Entities;

namespace Taskly_Api.Request.Table;

public record CreateTableItemRequest(string Task, 
                                     string Status, 
                                     string Label, 
                                     List<Guid> Members,
                                     DateTime EndTime,
                                     bool IsCompleted,
                                     Guid TableId);
