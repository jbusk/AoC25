using Position = (int x, int y, int z);

var boxes = new List<Position>();
bool sample = false;
(string filename, int iterationsPt1) = sample switch
    {
        true => ("sample.txt", 10),
        _ => ("input.txt", 1000)
    }
    ;
var lines = File.ReadAllLines(filename);
foreach (var l in lines)
{
    var xyz = l.Split(',');
    boxes.Add((int.Parse(xyz[0]), int.Parse(xyz[1]), int.Parse(xyz[2])));
}

var distances = new Dictionary<(Position, Position), double>();

foreach (var b1 in boxes)
{
    foreach (var b2 in boxes)
    {
        if (b1 == b2)
            continue;

        if (distances.ContainsKey((b1, b2)) || distances.ContainsKey((b2, b1)))
            continue;

        distances.Add((b1, b2), Math.Sqrt(
            Math.Pow(b1.x - b2.x, 2) +
            Math.Pow(b1.y - b2.y, 2) +
            Math.Pow(b1.z - b2.z, 2)
        ));
    }
}

var connections = distances
    .OrderBy(kv => kv.Value)
    .Select(kv => (kv.Key.Item1, kv.Key.Item2))
    .ToList();

Console.WriteLine("Part 1: " + Part1(connections.Take(iterationsPt1)));
Console.WriteLine("Part 2: " + Part2(connections, lines.Length));

int Part1(IEnumerable<(Position, Position)> pairs)
{
    var circuits = new Dictionary<Position, HashSet<Position>>();

    foreach (var pair in pairs)
    {
        var circuit1 = circuits.GetValueOrDefault(pair.Item1);
        var circuit2 = circuits.GetValueOrDefault(pair.Item2);

        var circuit = (circuit1, circuit2) switch
        {
            (null, null) => new HashSet<Position> { pair.Item1, pair.Item2 },
            ({ } c1, null) => c1,
            (null, { } c2) => c2,
            ({ } c1, { } c2) => new HashSet<Position>([.. c1, .. c2])
        };

        circuit.UnionWith([pair.Item1, pair.Item2]);
        foreach (var b in circuit)
            circuits[b] = circuit;
    }

    var sizes = circuits.Values
        .Select(v => v.Count)
        .OrderByDescending(c => c)
        .Distinct()
        .Take(3);

    return sizes.Aggregate(1, (a, b) => a * b);
}


int Part2(IEnumerable<(Position, Position)> pairs, int iterations)
{
    var circuits = new Dictionary<Position, HashSet<Position>>();

    foreach (var pair in pairs)
    {
        var circuit1 = circuits.GetValueOrDefault(pair.Item1);
        var circuit2 = circuits.GetValueOrDefault(pair.Item2);
        var circuit = (circuit1, circuit2) switch
        {
            (null, null) => new HashSet<Position> { pair.Item1, pair.Item2 },
            ({ } c1, null) => c1,
            (null, { } c2) => c2,
            ({ } c1, { } c2) => new HashSet<Position>([.. c1, .. c2])
        };
        circuit.UnionWith([pair.Item1, pair.Item2]);
        foreach (var b in circuit)
            circuits[b] = circuit;

        if (circuits.Count == iterations)
        {
            return pair.Item1.x * pair.Item2.x;
        }
    }

    return int.MinValue;
}