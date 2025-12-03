var lines = File.ReadAllLines("input.txt");
var (part1, part2) = (0, 0L);

foreach (var line in lines)
{
    var digits = line.ToArray().Select(x => int.Parse(x.ToString())).ToList();
    part1 += int.Parse(GetBest(line, 2));
    part2 += long.Parse(GetBest(line, 12));
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

string GetBest(string bank, int slots)
{
    var bankarray = bank.ToArray();
    var bestindex = -1;
    List<char> best = [];
    while (slots-- > 0)
    {
        var length = bankarray.Length - slots;
        var subBank = bankarray[(bestindex + 1) .. length];
        var maxValue = subBank.Max();
        bestindex = subBank.IndexOf(maxValue) + bestindex + 1;
        best.Add(maxValue);
    }

    return string.Join("", best);
}