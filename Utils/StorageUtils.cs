using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using GridHackConsoleApp.Models;

namespace GridHackConsoleApp.Utils
{
    public static class StorageUtils
    {
        private static readonly string _filePath = "history.json";

        public static void SaveHistory(List<GameRecord> history)
        {
            var json = JsonSerializer.Serialize(history);
            File.WriteAllText(_filePath, json);
        }

        public static List<GameRecord>? LoadHistory()
        {
            if (!File.Exists(_filePath))
                return null;

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<GameRecord>>(json);
        }

        public static void ClearHistory()
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);
        }
    }
}
