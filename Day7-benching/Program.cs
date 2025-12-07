using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Day07;

public class LinqBenchmarks
{
    private string[] _lines = [];

    [GlobalSetup]
    public void Setup()
    {
        _lines = File.ReadAllLines("input.txt");
    }

    [Benchmark]
    public long Pascal()
    {
        Dictionary<(int x, int y), long> dict = [];
        dict.Add((0, _lines[0].IndexOf('S')), 1);

        for (int x = 1; x < _lines.Length; x++)
        {
            for (int y = 0; y < _lines[x].Length; y++)
            {
                if (dict.TryGetValue((x, y), out long above))
                {
                    if (_lines[x][y] is '^')
                    {
                        // right
                        if (dict.TryGetValue((x, y-1), out long right))
                            right += above + 1;
                        // left
                        if (dict.TryGetValue((x, y-1), out long left))
                            left += above + 1;
                    }
                    else
                    {
                        dict.Add((x, y), above);
                    }
                }
            }
        }

        return dict.Where(kvp => kvp.Key.x == _lines.Length).Sum(kvp => kvp.Value);
    }
    
    [Benchmark]
    public long OriginalQuery()
    {
        Dictionary<(int x, int y), long> cache = [];
        return RecurseManifold(2, _lines[0].IndexOf('S'));

        long RecurseManifold(int x, int y)
        {
            if (cache.TryGetValue((x, y), out var value))
                return value;
            if (x == _lines.Length)
                return 1L;
            long retval;
            if (_lines[x][y] is '^') // splitter
            {
                retval = RecurseManifold(x, y - 1) + RecurseManifold(x, y + 1);
            }
            else
                retval = RecurseManifold(x + 1, y);

            cache.Add((x, y), retval);
            return retval;
        }
    }

    [Benchmark]
    public long ImprovedQuery()
    {
        Dictionary<(int x, int y), long> cache = [];
        return RecurseManifold(2, _lines[0].IndexOf('S'));

        long RecurseManifold(int x, int y)
        {
            if (cache.TryGetValue((x, y), out var value))
                return value;
            if (x == _lines.Length)
                return 1L;
            long retval;
            if (_lines[x][y] is '^') // splitter
            {
                retval = RecurseManifold(x + 2, y - 1) + RecurseManifold(x + 2, y + 1);
            }
            else
                retval = RecurseManifold(x + 2, y);

            cache.Add((x, y), retval);
            return retval;
        }
    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        var bm = new LinqBenchmarks();
        Console.WriteLine(bm.Pascal());
        Console.WriteLine(bm.ImprovedQuery());
        // BenchmarkRunner.Run<LinqBenchmarks>();
    }
}