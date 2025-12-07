using Position = (int x, int y);

var lines = File.ReadAllLines("input.txt");
Dictionary<Position, long> cache = [];
Position start = (0, lines[0].IndexOf('S'));
HashSet<Position> beams = [start];
HashSet<Position> splitters = [];

for (int row = 1; row < lines.Length; row++)
{
    var line = lines[row];
    for (int col = 0; col < line.Length; col++)
        if (beams.Contains(new Position(row - 1, col)))
        {
            if (line[col] == '^')
            {
                splitters.Add((row, col));
                Position[] newpositions = [(row, col - 1), (row, col + 1)];
                foreach (var newpos in newpositions)
                    beams.Add(newpos);
            }
            else
                beams.Add(new Position(row, col));
        }
}

Console.WriteLine("Part 1: " + splitters.Count);
Console.WriteLine("Part 2: " + RecurseManifold(start));

long RecurseManifold(Position pos)
{
    if (cache.TryGetValue(pos, out var value))
        return value;
    if (pos.x == lines.Length)
        return 1L;
    var retval = 0L;
    if (splitters.Contains(pos))
        retval = RecurseManifold((pos.x, pos.y - 1)) + RecurseManifold((pos.x, pos.y + 1));
    else
        retval = RecurseManifold((pos.x + 1, pos.y));
    cache.Add(pos, retval);
    return retval;
}