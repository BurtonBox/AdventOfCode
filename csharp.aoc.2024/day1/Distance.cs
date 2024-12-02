using System.Text;

namespace csharp.aoc._2024.day1;

public class Distance
{

    public void Part1(List<int>? list1, List<int>? list2)
    {
        if (list1 == null || list2 == null ||  list1.Count() != list2.Count)
        {
            throw new Exception("Input lists must have the same length.");
        }
        
        list1.Sort();
        list2.Sort();
        int dist = 0;
        for (int index = 0; index < list1.Count; index++)
        {
            dist += Math.Abs(list1[index] - list2[index]);
        }

        Console.WriteLine($"Part1 Distance: {dist}");
    }

    public void Part2(List<int> list1, List<int> list2)
    {
        if (list1 == null || list2 == null ||  list1.Count() != list2.Count)
        {
            throw new Exception("Input lists must have the same length.");
        }
        
        var group = list2
            .OrderBy(number => number)
            .GroupBy(number => number)
            .ToDictionary(group => group.Key, group => group.Count());

        int dist = 0;
        foreach (var item in list1)
        {
            if (group.TryGetValue(item, out var value))
            {
                dist += item * value;
            }
        }

        Console.WriteLine($"Part2 Distance: {dist}");

    }

    public Tuple<List<int>,List<int>> LoadSignificantLocations(string filename)
    {
        using var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(stream, Encoding.UTF8);

        var list1 = new List<int>();
        var list2 = new List<int>();
        while (reader.ReadLine() is { } line)
        {
            var lists = line.Split("   ");
            if (lists.Length == 2)
            {
                list1.Add(int.Parse(lists[0]));
                list2.Add(int.Parse(lists[1]));
            }
            else
            {
                throw new Exception("File is corrupted.");
            }
        }

        return new Tuple<List<int>, List<int>>(list1, list2);
    }
}