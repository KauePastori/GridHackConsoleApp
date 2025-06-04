using System;

namespace GridHackConsoleApp.Models
{
    public class GameRecord
    {
        public DateTime Date { get; }
        public int FailedIsolated { get; }
        public int TimeSeconds { get; }
        public string Rank { get; }

        public GameRecord(DateTime date, int failedIsolated, int timeSeconds, string rank)
        {
            Date = date;
            FailedIsolated = failedIsolated;
            TimeSeconds = timeSeconds;
            Rank = rank ?? "";
        }
    }
}
