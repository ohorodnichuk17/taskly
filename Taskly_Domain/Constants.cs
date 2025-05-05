namespace Taskly_Domain;

public static class Constants
{
    public static readonly string Todo = "To Do";
    public static readonly string Inprogress = "In Progress";
    public static readonly string Done = "Done";

    public static readonly string ChangePassword = "ChangePassword";
    public static readonly string VerificateEmail = "VerificateEmail";

    public static readonly Guid DefaultAvatarId = Guid.Parse("B50E9545-44CE-4E9B-A056-5B8580AA9017");
    
    public static readonly string AdminRole = "Admin";
    public static readonly string UserRole = "User";
    public static readonly string[] AllRoles = [AdminRole, UserRole];
    
    public static readonly string BegginerLevel = "Beginner";
    public static readonly string AdvancedLevel = "Advanced";
    public static readonly string MasteryLevel = "Mastery";
}
