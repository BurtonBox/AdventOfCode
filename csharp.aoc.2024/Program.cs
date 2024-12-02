using csharp.aoc._2024.day1;

namespace csharp.aoc._2024;

class Program
{
    static void Main(string[] args)
    {
        
        var day1 = new Distance();
        var lists = day1.LoadSignificantLocations("./day1/part1.data.txt");

        day1.Part1(lists.Item1, lists.Item2);
        day1.Part2(lists.Item1, lists.Item2);

        Console.WriteLine("done");
        
    }
}