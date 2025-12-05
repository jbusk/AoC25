var file = File.ReadAllText("input.txt").Split(Environment.NewLine + Environment.NewLine);
List<Range> fresh = [];
foreach (var line in file[0].Split(Environment.NewLine))
{
    var current = line.Split('-');
    fresh.Add(new Range(long.Parse(current[0]), long.Parse(current[1])));
}
Console.WriteLine($"Part 1: " + file[1]
    .Split(Environment.NewLine)
    .Count(ingredients => 
        fresh.Any(range => range.IsInRange(long.Parse(ingredients)))
    ));
var stack = new Stack<Range>();
foreach (var range in fresh.OrderBy(range => range.Start))
{
    if (!stack.TryPeek(out var current) || !current.Overlaps(range))
    {
        stack.Push(range);
    }
    else
    {
        var last  = stack.Pop();
        stack.Push(last.Merge(range));
    }
}

Console.WriteLine("Part 2: " + stack.Sum(r => r.Length));
class Range(long start, long end)
{
    public long Start { get; init; } = start;
    public long End { get; init; } = end;
    
    public bool IsInRange(long value)
        => value >= Start && value <= End;

    public bool Overlaps(Range range) 
        => !(range.End < Start || range.Start > End);

    public Range Merge(Range range) 
        => new(Math.Min(Start, range.Start), Math.Max(End, range.End));

    public long Length
        => End - Start + 1;
    
}