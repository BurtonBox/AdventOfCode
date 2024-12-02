using csharp.aoc._2024.day1;
using csharp.aoc._2024.day2;

namespace csharp.aoc._2024;

class Program
{
    static void Main(string[] args)
    {
        
        var day1 = new Distance();
        var parts = day1.LoadSignificantLocations("./day1/part1.sample.txt");
        day1.Part1(parts.Item1, parts.Item2);
        day1.Part2(parts.Item1, parts.Item2);

        var day2 = new Report();
        day2.Part1("./day2/part1.sample.txt");
        day2.Part2("./day2/part1.sample.txt");

        Console.WriteLine("done");
        
    }
}