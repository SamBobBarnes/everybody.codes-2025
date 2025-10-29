using System.Diagnostics;

namespace everybody.codes_2025;

public class Helpers
{
    public static string LoadInputFile(string fileName)
    {
        string workingDir = Path.Combine(Directory.GetCurrentDirectory(), "../../../Inputs");
        string path = Path.Combine(workingDir, fileName);
        if (!File.Exists(path))
        {
            File.Create(path).Close();
            File.WriteAllText(path, "Replace me with your input");
            var processStartInfo = new ProcessStartInfo
            {
                Arguments = $"add \"{fileName}\"",
                FileName = "git.exe",
                WorkingDirectory = workingDir,
            };
            Process.Start(processStartInfo);
        }
        return File.ReadAllText(path);
    }
}