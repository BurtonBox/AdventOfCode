using System.Text;

namespace csharp.aoc._2024.day2;

public class Report
{
    public void Part1(string filename)
    {
        if (!File.Exists(filename)) return;

        using var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(stream, Encoding.UTF8);

        int validLineCount = 0;

        while (reader.ReadLine() is { } line)
        {
            var reports = line
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.TryParse(s, out var num) ? num : (int?)null)
                .Where(num => num.HasValue)
                .Select(num => num.Value)
                .ToList();

            if (reports.Count < 2) {continue;}

            int? previousDirection = null;
            bool isInvalid = false;

            for (int i = 0; i < reports.Count - 1; i++)
            {
                int current = reports[i];
                int next = reports[i + 1];
                int direction = current > next ? 1 : current < next ? -1 : 0;

                if (previousDirection == null) previousDirection = direction;
                if (direction != previousDirection || Math.Abs(current - next) > 3)
                {
                    isInvalid = true;
                    break;
                }
            }

            if (!isInvalid) validLineCount++;
        }

        Console.WriteLine($"Day2 Part1: {validLineCount}");
    }
    
    public void Part2(string filename)
    {
        if (!File.Exists(filename)) return;

        using var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(stream, Encoding.UTF8);

        int validLineCount = 0;

        while (reader.ReadLine() is { } line)
        {
            var report = line
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.TryParse(s, out var num) ? num : (int?)null)
                .Where(num => num.HasValue)
                .Select(num => num.Value)
                .ToList();

            if (report.Count < 2) {continue;}
            if (IsValidReport(report))
            {
                validLineCount++;
                continue;
            }

            bool canFix = false;
            for (int i = 0; i < report.Count; i++)
            {
                var modifiedReports = new List<int>(report);
                modifiedReports.RemoveAt(i);

                if (!IsValidReport(modifiedReports)) {continue;}
                
                canFix = true;
                break;
            }

            if (canFix)
            {
                validLineCount++;
            }
        }

        Console.WriteLine($"Day2 Part1: {validLineCount}");
    }
    
    private bool IsValidReport(List<int> report)
    {
        int? previousDirection = null;
        for (int i = 0; i < report.Count - 1; i++)
        {
            int current = report[i];
            int next = report[i + 1];
            int direction = current > next ? 1 : current < next ? -1 : 0;
            previousDirection ??= direction;

            if (direction != previousDirection || Math.Abs(current - next) > 3)
            {
                return false;
            }
        }
        return true;
    }
}