namespace adventofcode2024;

internal static class Helpers
{
    public static string GetFileAsString(string fileName)
    {
        var path = $"./data/{fileName}";
        string workingDirectory = Environment.CurrentDirectory;

        string projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName ?? throw new NullReferenceException("Can't find project dir.");
        string listPath = Path.Combine(projectDirectory, path);

        return File.ReadAllText(listPath);
    }
}
