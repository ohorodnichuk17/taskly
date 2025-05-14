namespace Taskly_Api.Request.Table;

public record MemberTableRequest(
    Guid TableId,
    string MemberEmail);