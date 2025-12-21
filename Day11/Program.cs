var lines = File.ReadAllLines("input.txt").Select(x => x.Split(": "));
var nodes = new Dictionary<string, string[]>();
var cache = new Dictionary<(string current, string goal, bool fft, bool dac), long>();
foreach (var line in lines)
    nodes[line[0]] = line[1].Split(' ');

Console.WriteLine("Part 1: " + RecursePt1("you", "out"));
Console.WriteLine("Part 2: " + RecursePt2("svr", "out"));

int RecursePt1(string current, string goal)
{
    if (current == goal)
        return 1;
    int paths = 0;
    foreach (var node in nodes[current])
        paths += RecursePt1(node, goal);

    return paths;
}

long RecursePt2(string current, string goal, bool fft = false, bool dac = false)
{
    if (cache.TryGetValue((current, goal, fft, dac), out var value))
        return value;
    if (current == goal && fft && dac)
        return 1;
    if (current == goal && (!fft || !dac))
        return 0;
    long paths = 0;
    if (current == "fft")
        fft = true;
    if (current == "dac")
        dac = true;
    foreach (var node in nodes[current])
        if (cache.TryGetValue((node, goal, fft, dac), out var curr))
            paths += curr;
        else
            paths += RecursePt2(node, goal, fft, dac);

    cache.TryAdd((current, goal, fft, dac), paths);
    return paths;
}