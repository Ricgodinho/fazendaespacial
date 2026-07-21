using System;
using System.IO;
using UnityEngine;

// Log de eventos de jogo em CSV, para depuracao ponta-a-ponta ("cliquei
// aqui, o que aconteceu"). Fica em unity/Logs/gameplay/ (irmao de Assets,
// ja coberto por [Ll]ogs/ no .gitignore) - nao e salvo, nao e arte final,
// so uma ferramenta de diagnostico durante o playtest.
public static class GameLog
{
    private static string _filePath;
    private static readonly object WriteLock = new();

    private static void EnsureInitialized()
    {
        if (_filePath != null)
        {
            return;
        }

        string dir = Path.Combine(Application.dataPath, "..", "Logs", "gameplay");
        Directory.CreateDirectory(dir);

        string fileName = $"session_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        _filePath = Path.Combine(dir, fileName);

        File.WriteAllText(_filePath, "Timestamp,Category,Event,Details\n");
    }

    public static void Log(string category, string eventName, string details = "")
    {
        EnsureInitialized();

        string line = $"{DateTime.Now:HH:mm:ss.fff},{ToCsvField(category)},{ToCsvField(eventName)},{ToCsvField(details)}\n";

        lock (WriteLock)
        {
            File.AppendAllText(_filePath, line);
        }
    }

    private static string ToCsvField(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        if (value.IndexOfAny(new[] { ',', '"', '\n' }) >= 0)
        {
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        return value;
    }
}
