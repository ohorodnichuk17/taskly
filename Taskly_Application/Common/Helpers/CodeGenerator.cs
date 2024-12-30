namespace Taskly_Application.Common.Helpers;

public static class CodeGenerator
{
    private static Random random = new Random();
    public static string GenerateCode()
    {
        return CodeGenerator.ConvertToString(
            random.GetItems<int>([0, 1, 2, 3, 4, 5, 6, 7, 8, 9], 7));
    }
    private static string ConvertToString(int[] codeArr) {
        string code = String.Empty;
        foreach (int i in codeArr) code += i.ToString();
        return code;
    }
}
