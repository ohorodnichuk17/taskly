using ErrorOr;

namespace Taskly_Domain.TypeExtensions;

public static class ErrorOrExtensions
{
    public static bool IsSuccess<T>(this ErrorOr<T> errorOr) => !errorOr.IsError;
}